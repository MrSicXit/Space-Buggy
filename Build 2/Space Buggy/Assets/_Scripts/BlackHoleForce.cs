using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The black hole will attract bodies in range of collision. Will exert a force in a range of InnerMinForce to InnerMaxForce,
/// unless inside a distance from the center of InnerRadius or less, in that case the force will range between ExternalMinForce and ExternalMaxForce.
/// 
/// Needs to be attached to a GameObject with a SphereCollider, and this need to be set as trigger.
/// The object or parents need to be scaled to 1,1,1 
/// so the real radius of the collider matches the value on its Radius property. Objects subject to scale, for visuals, need to be children or at the same hierarchy level.
/// 
/// The BlackHole will keep a reference of every rigidbody on scene at the moment it runs Start(). Static objects and WheelColliders will be ignored for distance calculation.
/// Kinematic rigidbodies will be ignored on Update(). **A Rigidbody on an object containing no collider other than wheel colliders in its hierarchy WILL CAUSE AN ERROR.** (like a loose, rolling wheel)
/// </summary>
/// 
public class BlackHoleForce : MonoBehaviour
{
    [SerializeField]
    Vector3 force;//Vector to manage the Velocity to be added on a frame.

    //Values to be used in the Linear Interpolation:
    Vector3 minForce;//Would be applied when the distance is equal to the radius of the blackHole sphereCollider
    Vector3 maxForce;//Would be applied when the object its at the center of the blackHole

    [SerializeField]//Number of objects on scene subject to be pulled by the blackHole once inside range
    int numberOfRigidbodiesOnScene;

    [SerializeField]//Used to identify objects potentially subject to the blackhole, and to apply such force on them
    Rigidbody[] rigidbodyArray;

    Transform[] transformArray;//For distance calculation
    Collider[] colliderArray;//Array to store colliders subject to be under the traction force
    bool[] collidersInRange;//Array to be paired with colliderArray, telling whether they are within traction range or not

    [SerializeField]//Minimum force to be applied
    float ExternalRingMinForce;
    [SerializeField]//Maximum force to be applied on the weaker side of the black hole
    float ExternalRingMaxForce;
    [SerializeField]//Minimum force on the inner radius of the black hole
    float InnerRingMinForce;
    [SerializeField]//Maximum force, for the center of the black hole
    float InnerRingMaxForce;
    [SerializeField]//Radius of the inner, stronger part of the black hole
    float InnerRadius;

    float radiusOfBlackHole;//Total radius of Black hole  
    float distanceBetweenBodies;//Will be used to calculate amount of force used
    bool foundInArray;//variable to be used in search in arrays

    // Use this for initialization
    void Start()
    {
        //Finding the rigidbodies in scene, and keeping reference to them, their transforms, their first order, non-wheel colliders.
        rigidbodyArray = FindObjectsOfType<Rigidbody>();
        numberOfRigidbodiesOnScene = rigidbodyArray.Length;
        transformArray = new Transform[numberOfRigidbodiesOnScene];
        colliderArray = new Collider[numberOfRigidbodiesOnScene];
        collidersInRange = new bool[numberOfRigidbodiesOnScene];

        for (int i = 0; i != numberOfRigidbodiesOnScene; i++)//Initializing objects to not subject to force at start
        {
            if (!(rigidbodyArray[i].gameObject.GetComponentInChildren<Collider>().GetType() == new WheelCollider().GetType()))
            {
                transformArray[i] = rigidbodyArray[i].gameObject.transform;
                colliderArray[i] = rigidbodyArray[i].gameObject.GetComponentInChildren<Collider>();
            }
            collidersInRange[i] = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i != numberOfRigidbodiesOnScene; i++)//Looking through the rigidbodies:
        {
            if ((collidersInRange[i])&&(!rigidbodyArray[i].isKinematic))//If on range, and not kinematic
            {
                force = transform.position - transformArray[i].position;//Find the direction towards the center of the black hole from the object
                force.Normalize();//Get the direction Vector
                distanceBetweenBodies = Vector3.Distance(transformArray[i].position, transform.position);//Calculate Distance
                
                //Scale force vector depending on distance being lower or higher than the inner radius set for the script
                if (distanceBetweenBodies<=InnerRadius)
                {
                    minForce.x = force.x * InnerRingMinForce;
                    minForce.y = force.y * InnerRingMinForce;
                    minForce.z = force.z * InnerRingMinForce;
                    maxForce.x = force.x * InnerRingMaxForce;
                    maxForce.y = force.y * InnerRingMaxForce;
                    maxForce.z = force.z * InnerRingMaxForce;
                    force = Vector3.Lerp(minForce, maxForce, 1 - (radiusOfBlackHole - distanceBetweenBodies));
                }
                else
                {
                    minForce.x = force.x * ExternalRingMinForce;
                    minForce.y = force.y * ExternalRingMinForce;
                    minForce.z = force.z * ExternalRingMinForce;
                    maxForce.x = force.x * ExternalRingMaxForce;
                    maxForce.y = force.y * ExternalRingMaxForce;
                    maxForce.z = force.z * ExternalRingMaxForce;
                    force = Vector3.Lerp(minForce, maxForce, 1- (radiusOfBlackHole - distanceBetweenBodies));
                }
                //Apply force, proportional to deltaTime
                rigidbodyArray[i].AddForce(force * Time.deltaTime, ForceMode.VelocityChange);
            }
        }
    }

    void OnTriggerEnter(Collider other)//Comparing rigidbodies
    {
        if (!other.gameObject.isStatic)//If the triggering collider belongs to a non-static object, procede further, otherwise, ignore it
        {
            foundInArray = false;

            for (int i = 0; i != numberOfRigidbodiesOnScene; i++)
            {
                if (colliderArray[i].gameObject == other.gameObject)//The object is set as under the black hole force
                {
                    foundInArray = true;
                    collidersInRange[i] = true;
                }
            }
        }
    }
}

