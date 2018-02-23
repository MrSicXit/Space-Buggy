using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CarMovement : MonoBehaviour
{
    [SerializeField]
    List<AxleInfo> axleInfos; // the information about each individual axle
    [SerializeField]
    float maxMotorTorque; // maximum torque the motor can apply to wheel
    [SerializeField]
    float maxSteeringAngle; // maximum steer angle the wheel can have
    [SerializeField]
    float breakTorque;
    [SerializeField]
    float maximumSpeed;
    [SerializeField]
    float jumpPower;
    [SerializeField]
    Transform centreOfMass;
    [SerializeField]
    Rigidbody mainRigidBody;
    [SerializeField]
    Text speedDisplay;
    [SerializeField]
    Text coinDisplay;


    bool jumpReady = true;
    bool boostReady = true;
    float velocity = 0;
    int coinsCollected = 0;

    public void Start()
    {
        mainRigidBody.centerOfMass = centreOfMass.localPosition;
    }

    public void FixedUpdate()
    {
        //main movement script
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor)
            {

                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;

                velocity = axleInfo.leftWheel.radius * axleInfo.leftWheel.rpm * 0.10472f;
                Debug.Log(velocity.ToString());

            }

            if (axleInfo.breaks && Input.GetKey(KeyCode.LeftShift))
            {
                speedDisplay.text = "Speed: " + velocity.ToString();
                print(axleInfo.leftWheel.suspensionDistance);
                axleInfo.leftWheel.brakeTorque = breakTorque;
                axleInfo.rightWheel.brakeTorque = breakTorque;
            }
            else if (axleInfo.breaks && !Input.GetKey(KeyCode.LeftShift))
            {
                axleInfo.leftWheel.brakeTorque = 0;
                axleInfo.rightWheel.brakeTorque = 0;
            }

            if (jumpReady == true && Input.GetKey(KeyCode.Space))
            {
                //jump script
                mainRigidBody.AddForce(transform.up * jumpPower);
                Debug.Log("jumping");
                jumpReady = false;
            }

            ApplyLocalPositionToVisuals(axleInfo.leftWheel);
            ApplyLocalPositionToVisuals(axleInfo.rightWheel);
        }
        
        coinDisplay.text = "Coins: " + coinsCollected.ToString();

    }

    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0)
        {
            return;
        }

        Transform visualWheel = collider.transform.GetChild(0);



        visualWheel.localEulerAngles = new Vector3(visualWheel.localEulerAngles.x, collider.steerAngle - visualWheel.localEulerAngles.z, visualWheel.localEulerAngles.z);
        visualWheel.Rotate(collider.rpm / 60 * 360 * Time.deltaTime, 0, 0);


    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
            coinsCollected = coinsCollected + 1;
            Debug.Log(coinsCollected.ToString());
        }
        if (other.gameObject.CompareTag("Floor"))
        {
            jumpReady = true;
        }
    }
}





[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor; // is this wheel attached to motor?
    public bool steering; // does this wheel apply steer angle?
    public bool breaks;
}