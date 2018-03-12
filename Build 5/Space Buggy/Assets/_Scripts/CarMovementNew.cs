//using UnityEngine;
//using UnityEngine.UI;
//using System.Collections;
//using System.Collections.Generic;

//public class CarMovementNew : MonoBehaviour
//{
//    [SerializeField]
//    int playerNumber;
//    [SerializeField]
//    List<AxleInfo> axleInfos; // the information about each individual axle
//    [SerializeField]
//    float uphillMotorTorque;
//    [SerializeField]
//    float downhillMotorTorque;
//    [SerializeField]
//    float normalMotorTorque;
//    [SerializeField]
//    float maxSteeringAngle; // maximum steer angle the wheel can have
//    [SerializeField]
//    float maximumSpeed;
//    [SerializeField]
//    float jumpPower;
//    [SerializeField]
//    float boost;
//    [SerializeField]
//    Transform centreOfMass;
//    [SerializeField]
//    Rigidbody mainRigidBody;
//    [SerializeField]
//    Text speedDisplay;
//    [SerializeField]
//    Text coinDisplay;

//    float breakTorque;
//    public string wheelType = "Generic";
//    bool jumpReady = true;
//    bool boostReady = true;
//    float velocity = 0;
//    int coinsCollected = 0;

//    int carNumber;
//    string getNumber;


//    public void Start()
//    {
//        getNumber = gameObject.name.Substring(gameObject.name.Length - 1);
//        int.TryParse(getNumber, out carNumber);

//        mainRigidBody.centerOfMass = centreOfMass.localPosition;
//    }

//    public void FixedUpdate()
//    {
//        float motor = GetMotorInput();

//        //main movement script
//        float steering = GetSteeringInput();

//        foreach (AxleInfo axleInfo in axleInfos)
//        {
//            if (axleInfo.steering)
//            {
//                axleInfo.leftWheel.steerAngle = steering;
//                axleInfo.rightWheel.steerAngle = steering;
//            }
//            if (axleInfo.motor)
//            {

//                axleInfo.leftWheel.motorTorque = motor;
//                axleInfo.rightWheel.motorTorque = motor;

//                velocity = axleInfo.leftWheel.radius * axleInfo.leftWheel.rpm * 0.10472f;
//                //Debug.Log(velocity.ToString());

//            }

//            if (axleInfo.breaks && Input.GetKey(KeyCode.LeftShift) && playerNumber == 1)
//            {
//                print(axleInfo.leftWheel.suspensionDistance);
//                axleInfo.leftWheel.brakeTorque = breakTorque;
//                axleInfo.rightWheel.brakeTorque = breakTorque;
//            }
//            else if (axleInfo.breaks && !Input.GetKey(KeyCode.LeftShift) && playerNumber == 1)
//            {
//                axleInfo.leftWheel.brakeTorque = 0;
//                axleInfo.rightWheel.brakeTorque = 0;
//            }

//            if (axleInfo.breaks && Input.GetAxis("LeftTrigger" + carNumber) != 0)
//            {
//                print("breaking" + Input.GetAxis("LeftTrigger" + carNumber));
//                print(axleInfo.leftWheel.suspensionDistance);
//                axleInfo.leftWheel.brakeTorque = breakTorque * Input.GetAxis("LeftTrigger" + carNumber);
//                axleInfo.rightWheel.brakeTorque = breakTorque * Input.GetAxis("LeftTrigger" + carNumber);
//            }

//            else if (axleInfo.breaks && !Input.GetKey(KeyCode.LeftShift) && playerNumber == 1)
//            {
//                axleInfo.leftWheel.brakeTorque = 0;
//                axleInfo.rightWheel.brakeTorque = 0;
//            }

//            if (jumpReady == true && Input.GetKey(KeyCode.Space) && playerNumber == 1)
//            {
//                //jump script - on com
//                mainRigidBody.AddForce(transform.up * jumpPower);
//                Debug.Log("jumping");
//                jumpReady = false;
//            }

//            if (axleInfo.breaks && Input.GetKey(KeyCode.RightShift) && playerNumber == 2)
//            {
//                print(axleInfo.leftWheel.suspensionDistance);
//                axleInfo.leftWheel.brakeTorque = breakTorque;
//                axleInfo.rightWheel.brakeTorque = breakTorque;
//            }

//            if (Input.GetAxis("AButton" + carNumber) != 0)
//            {
//                Debug.Log(Input.GetAxis("AButton" + carNumber) + "woo" + carNumber);
//                mainRigidBody.AddForce(transform.up * jumpPower);
//                jumpReady = false;
//            }

//            if (Input.GetAxis("XButton" + carNumber) != 0)
//            {
//                //Do something
//            }

//            if (Input.GetAxis("YButton" + carNumber) != 0)
//            {
//                //Do something
//            }

//            else if (axleInfo.breaks && !Input.GetKey(KeyCode.RightShift) && playerNumber == 2)
//            {
//                axleInfo.leftWheel.brakeTorque = 0;
//                axleInfo.rightWheel.brakeTorque = 0;
//            }

//            /*if (jumpReady == true && Input.GetKey(KeyCode.Keypad0) && playerNumber == 2)
//            {
//                //jump script - on com
//                mainRigidBody.AddForce(transform.up * jumpPower);
//                Debug.Log("jumping");
//                jumpReady = false;
//            }*/

//            ApplyLocalPositionToVisuals(axleInfo.leftWheel);
//            ApplyLocalPositionToVisuals(axleInfo.rightWheel);
//        }

//        coinDisplay.text = "Coins: " + coinsCollected.ToString();
//        speedDisplay.text = "Speed: " + velocity.ToString();
//    }

//    public void ApplyLocalPositionToVisuals(WheelCollider collider)
//    {
//        if (collider.transform.childCount == 0)
//        {
//            return;
//        }

//        Transform visualWheel = collider.transform.GetChild(0);



//        visualWheel.localEulerAngles = new Vector3(visualWheel.localEulerAngles.x, collider.steerAngle - visualWheel.localEulerAngles.z, visualWheel.localEulerAngles.z);
//        visualWheel.Rotate(collider.rpm / 60 * 360 * Time.deltaTime, 0, 0);


//    }


//    void OnTriggerEnter(Collider other)
//    {
//        if (other.gameObject.CompareTag("Coin"))
//        {
//            other.gameObject.SetActive(false);
//            coinsCollected = coinsCollected + 1;
//            Debug.Log(coinsCollected.ToString());
//        }
//        if (other.gameObject.CompareTag("Floor"))
//        {
//            jumpReady = true;
//        }
//        if (other.gameObject.CompareTag("SpeedBoost"))
//        {
//            Debug.Log("speed boosted");
//            StartCoroutine(speedBoost());
//        }
//        if (other.gameObject.CompareTag("Lava"))
//        {

//        }
//    }

//    IEnumerator speedBoost()
//    {
//        Vector3 boostForce = mainRigidBody.velocity.normalized * boost * Time.deltaTime;
//        Debug.Log(boostForce.ToString());
//        mainRigidBody.AddForce(boostForce, ForceMode.Impulse);
//        yield return new WaitForSeconds(5f);
//    }

//    float GetMotorInput()
//    {
//        float motor = 0;
//        /*if (playerNumber == 1 && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)))
//         {
//            if (transform.rotation.x <= -5)
//            {
//                Debug.Log("uphill");
//				motor = normalMotorTorque * -Input.GetAxis("LeftJoystickY");
//				//motor = uphillMotorTorque * Input.GetAxis("Vertical");
//                breakTorque = uphillMotorTorque * 2;
//            }
//            if (transform.rotation.x >= 1)
//            {
//                Debug.Log("downhill");
//				motor = normalMotorTorque * -Input.GetAxis("LeftJoystickY");
//				//motor = downhillMotorTorque * Input.GetAxis("Vertical");
//                breakTorque = downhillMotorTorque * 2;
//            }
//            if (transform.rotation.x < 1 && transform.rotation.x > -5)
//            {
//                Debug.Log("normal");
//				motor = normalMotorTorque * -Input.GetAxis("LeftJoystickY");
//				//motor = normalMotorTorque * Input.GetAxis("Vertical");
//				breakTorque = normalMotorTorque * 2;
//            }
//        }
//        else if ( playerNumber == 2 && (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow)))
//        {
//            if (transform.rotation.x <= -5)
//            {
//                Debug.Log("uphill");
//                motor = uphillMotorTorque * Input.GetAxis("Vertical");
//                breakTorque = uphillMotorTorque * 2;
//            }
//            if (transform.rotation.x >= 1)
//            {
//                Debug.Log("downhill");
//                motor = downhillMotorTorque * Input.GetAxis("Vertical");
//                breakTorque = downhillMotorTorque * 2;
//            }
//            if (transform.rotation.x < 1 && transform.rotation.x > -5)
//            {
//                Debug.Log("normal");
//                motor = normalMotorTorque * Input.GetAxis("Vertical");
//                breakTorque = normalMotorTorque * 2;
//            }
//        }*/
//        if (Input.GetAxis("RightTrigger" + carNumber) != 0)
//        {
//            if (transform.rotation.x <= -5)
//            {
//                Debug.Log("uphill");
//                motor = normalMotorTorque * Input.GetAxis("RightTrigger" + carNumber);
//                //motor = uphillMotorTorque * Input.GetAxis("Vertical");
//                breakTorque = uphillMotorTorque * 2;
//            }
//            if (transform.rotation.x >= 1)
//            {
//                Debug.Log("downhill");
//                motor = normalMotorTorque * Input.GetAxis("RightTrigger" + carNumber);
//                //motor = downhillMotorTorque * Input.GetAxis("Vertical");
//                breakTorque = downhillMotorTorque * 2;
//            }
//            if (transform.rotation.x < 1 && transform.rotation.x > -5)
//            {
//                Debug.Log("normal");
//                motor = normalMotorTorque * Input.GetAxis("RightTrigger" + carNumber);
//                //motor = normalMotorTorque * Input.GetAxis("Vertical");
//                breakTorque = normalMotorTorque * 2;
//                print(Input.GetAxis("RightTrigger" + carNumber));
//            }
//        }
//        return motor;
//    }

//    float GetSteeringInput()
//    {
//        float steering = 0;
//        if (playerNumber == 1 && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
//        {
//            steering = maxSteeringAngle * Input.GetAxis("Horizontal");
//        }
//        else if (playerNumber == 2 && (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)))
//        {
//            steering = maxSteeringAngle * Input.GetAxis("Horizontal");
//        }

//        if (Input.GetAxis("LeftJoystickY" + carNumber) != 0)
//        {
//            steering = maxSteeringAngle * Input.GetAxis("LeftJoystickX" + carNumber);
//        }
//        return steering;
//    }
//}





//[System.Serializable]
//public class AxleInfo
//{
//    public WheelCollider leftWheel;
//    public WheelCollider rightWheel;
//    public bool motor; // is this wheel attached to motor?
//    public bool steering; // does this wheel apply steer angle?
//    public bool breaks;
//}