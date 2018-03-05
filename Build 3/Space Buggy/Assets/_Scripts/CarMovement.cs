using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CarMovement : MonoBehaviour
{
    [SerializeField]
    int playerNumber;
    [SerializeField]
    List<AxleInfo> axleInfos; // the information about each individual axle
    [SerializeField]
    float uphillMotorTorque;
    [SerializeField]
    float downhillMotorTorque;
    [SerializeField]
    float normalMotorTorque;
    [SerializeField]
    float maxSteeringAngle; // maximum steer angle the wheel can have
    [SerializeField]
    float maximumSpeed;
    [SerializeField]
    float jumpPower;
    [SerializeField]
    float boost;
    [SerializeField]
    Transform centreOfMass;
    [SerializeField]
    Rigidbody mainRigidBody;
    [SerializeField]
    Text speedDisplay;
    [SerializeField]
    Text coinDisplay;

    float breakTorque;
    public string wheelType = "Generic";
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
        float motor = 0;

        if (transform.rotation.x <= -5)
        {
            Debug.Log("uphill");
            motor = uphillMotorTorque * Input.GetAxis("Vertical");
            breakTorque = uphillMotorTorque * 2;
        }
        if (transform.rotation.x >= 1)
        {
            Debug.Log("downhill");
            motor = downhillMotorTorque * Input.GetAxis("Vertical");
            breakTorque = downhillMotorTorque * 2;
        }
        if (transform.rotation.x < 1 && transform.rotation.x > -5)
        {
            Debug.Log("normal");
            motor = normalMotorTorque * Input.GetAxis("Vertical");
            breakTorque = normalMotorTorque * 2;
        }

        //main movement script
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
                //Debug.Log(velocity.ToString());

            }

            if (axleInfo.breaks && Input.GetKey(KeyCode.LeftShift))
            {
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
                //jump script - on com
                mainRigidBody.AddForce(transform.up * jumpPower);
                Debug.Log("jumping");
                jumpReady = false;
            }

            ApplyLocalPositionToVisuals(axleInfo.leftWheel);
            ApplyLocalPositionToVisuals(axleInfo.rightWheel);
        }

        coinDisplay.text = "Coins: " + coinsCollected.ToString();
        speedDisplay.text = "Speed: " + velocity.ToString();
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
        if (other.gameObject.CompareTag("SpeedBoost"))
        {
            Debug.Log("speed boosted");
            StartCoroutine(speedBoost());
        }
        if (other.gameObject.CompareTag("Lava"))
        {

        }
    }

    IEnumerator speedBoost()
    {
        Vector3 boostForce = mainRigidBody.velocity.normalized * boost * Time.deltaTime;
        Debug.Log(boostForce.ToString());
        mainRigidBody.AddForce(boostForce, ForceMode.Impulse);
        yield return new WaitForSeconds(5f);
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