using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class attached to a collider intends to activate a series of pulling forces from a point in space towards it,
/// adding the colliding object to a list of objects (or transforms or rigidbodies) or adding a component
/// </summary>
/// 
public class TractorForce : MonoBehaviour
{
    Rigidbody[] rigBodObjs;

    int foundAtIndex;//OnTriggerEnter

    [SerializeField]//Array to store the GameObjects under the traction force
    public Collider[] awareItems { get; private set; }
    [SerializeField]//Array to be paired with AwareItems, telling whether they are within traction range or not
    public bool[] currentlyInRange { get; set; }
    [SerializeField]
    float maxForceScale;
    [SerializeField]
    float minForceScale;
    [SerializeField]
    float radiusOfBlackHole;
    [SerializeField]
    float distanceBetweenBodies;

    void Awake()
    {
        // foundAtIndex = -1;//Was not found
        awareItems = new Collider[12];
        currentlyInRange = new bool[12];
        for (int i = 0; i < currentlyInRange.Length; i++)
        {
            currentlyInRange[i] = false;
        }
        radiusOfBlackHole = GetComponent<SphereCollider>().radius;
        distanceBetweenBodies = radiusOfBlackHole + 1;//just enough to keep it away from exerting any force
        //maxForceScale = 15;
        //minForceScale = 1;
    }

    // Use this for initialization
    void Start()
    {

        rigBodObjs = FindObjectsOfType<Rigidbody>();
        awareItems = new Collider[rigBodObjs.Length];
        currentlyInRange = new bool[rigBodObjs.Length];
        for (int i = 0; i < rigBodObjs.Length; i++)
        {
            if (!(rigBodObjs[i].gameObject.GetComponentInChildren<Collider>().GetType() == new WheelCollider().GetType()))
            {
                awareItems[i] = rigBodObjs[i].gameObject.GetComponentInChildren<Collider>();
            }     
            currentlyInRange[i] = false;
        }
        GetComponent<SphereCollider>().enabled = true;//Enabling so it triggers Enter
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < awareItems.Length; i++)
        {
            if (currentlyInRange[i])
            {
                Vector3 force = transform.position - rigBodObjs[i].gameObject.transform.position;
                force.Normalize();//Direction Vector
                force = Vector3.Lerp(new Vector3(force.x* maxForceScale, force.y * maxForceScale, force.z * maxForceScale),
                    new Vector3(force.x * minForceScale, force.y * minForceScale, force.z * minForceScale),
                    1 - (radiusOfBlackHole - Vector3.Distance(rigBodObjs[i].gameObject.transform.position, transform.position)));

                //Debug.Log("Lerp Val : " + (1 - (radiusOfBlackHole - Vector3.Distance(transform.position, rigBodObjs[i].gameObject.transform.position))));
                //Debug.Log("Force X " + force.x);
                //Debug.Log("Force Y " + force.y);
                //Debug.Log("Force Z " + force.z);

                rigBodObjs[i].AddForce(force * Time.deltaTime, ForceMode.VelocityChange);
            }
        }
    }

    void OnTriggerEnter(Collider other)//Comparing rigidbodies
    {
        if (!other.gameObject.isStatic)//If the triggering collider belongs to a non-static object, procede further, otherwise, ignore it
        {
            Debug.Log("Triggered");
            foundAtIndex = -1;

            for (int i = 0; i < awareItems.Length; i++)
            {
                if (awareItems[i].gameObject == other.gameObject)
                {
                    foundAtIndex = i;
                    currentlyInRange[i] = true;
                }
            }

            if (foundAtIndex == -1)
            {
                for (int i = 0; i < awareItems.Length; i++)
                {
                    if (!currentlyInRange[i])
                    {
                        awareItems[i] = other;
                        currentlyInRange[i] = true;
                    }
                }
            }
        }
    }
}

