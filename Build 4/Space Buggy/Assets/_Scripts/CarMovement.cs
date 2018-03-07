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
        float motor = GetMotorInput();

        //main movement script
        float steering = GetSteeringInput();

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

            if (axleInfo.breaks && Input.GetKey(KeyCode.LeftShift) && playerNumber == 1)
            {
                print(axleInfo.leftWheel.suspensionDistance);
                axleInfo.leftWheel.brakeTorque = breakTorque;
                axleInfo.rightWheel.brakeTorque = breakTorque;
            }
            else if (axleInfo.breaks && !Input.GetKey(KeyCode.LeftShift) && playerNumber == 1)
            {
                axleInfo.leftWheel.brakeTorque = 0;
                axleInfo.rightWheel.brakeTorque = 0;
            }

            if (jumpReady == true && Input.GetKey(KeyCode.Space) && playerNumber == 1)
            {
                //jump script - on com
                mainRigidBody.AddForce(transform.up * jumpPower);
                Debug.Log("jumping");
                jumpReady = false;
            }

            if (axleInfo.breaks && Input.GetKey(KeyCode.RightShift) && playerNumber == 2)
            {
                print(axleInfo.leftWheel.suspensionDistance);
                axleInfo.leftWheel.brakeTorque = breakTorque;
                axleInfo.rightWheel.brakeTorque = breakTorque;
            }
            else if (axleInfo.breaks && !Input.GetKey(KeyCode.RightShift) && playerNumber == 2)
            {
                axleInfo.leftWheel.brakeTorque = 0;
                axleInfo.rightWheel.brakeTorque = 0;
            }

            if (jumpReady == true && Input.GetKey(KeyCode.Keypad0) && playerNumber == 2)
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

    float GetMotorInput()
    {
        float motor = 0;
        if (playerNumber == 1)
         {
            if (transform.eulerAngles.x >= 300 && transform.eulerAngles.x < 358)
            {
                Debug.Log("uphill");
                motor = uphillMotorTorque * Input.GetAxis("P1_Vertical");
                breakTorque = uphillMotorTorque * 2;
            }
            if (transform.eulerAngles.x < 300 && transform.eulerAngles.x >= 1)
            {
                Debug.Log("downhill");
                motor = downhillMotorTorque * Input.GetAxis("P1_Vertical");
                breakTorque = downhillMotorTorque * 2;
            }
            if (transform.eulerAngles.x < 1 || transform.eulerAngles.x >= 358)
            {
                Debug.Log("normal");
                motor = normalMotorTorque * Input.GetAxis("P1_Vertical");
                breakTorque = normalMotorTorque * 2;
            }
        }
        else if (playerNumber == 2)
        {
            if (transform.eulerAngles.x <= -5)
            {
                Debug.Log("uphill");
                motor = uphillMotorTorque * Input.GetAxis("P2_Vertical");
                breakTorque = uphillMotorTorque * 2;
            }
            if (transform.eulerAngles.x >= 1)
            {
                Debug.Log("downhill");
                motor = downhillMotorTorque * Input.GetAxis("P2_Vertical");
                breakTorque = downhillMotorTorque * 2;
            }
            if (transform.eulerAngles.x < 1 && transform.eulerAngles.x > -5)
            {
                Debug.Log("normal");
                motor = normalMotorTorque * Input.GetAxis("P2_Vertical");
                breakTorque = normalMotorTorque * 2;
            }
        }
        return motor;
    }

    float GetSteeringInput()
    {
        float steering = 0;
        if (playerNumber == 1)
        {
            steering = maxSteeringAngle * Input.GetAxis("P1_Horizontal");
        }
        else if (playerNumber == 2)
        {
            steering = maxSteeringAngle * Input.GetAxis("P2_Horizontal");
        }

        return steering;
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