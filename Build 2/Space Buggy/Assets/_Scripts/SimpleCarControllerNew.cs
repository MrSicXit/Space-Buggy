//using UnityEngine;
//using UnityEngine.UI;
//using System.Collections;
//using System.Collections.Generic;

//public class SimpleCarControllerNew : MonoBehaviour
//{
//    [SerializeField]
//    List<AxleInfo> axleInfos; // the information about each individual axle
//    [SerializeField]
//    float maxMotorTorque; // maximum torque the motor can apply to wheel
//    [SerializeField]
//    float maxSteeringAngle; // maximum steer angle the wheel can have
//    [SerializeField]
//    float breakTorque;
//    [SerializeField]
//    float jumpHeight;
//    [SerializeField]
//    float antiRoll;
//    [SerializeField]
//    bool readyToJump;
//    [SerializeField]
//    bool readyToBoost;
//    [SerializeField]
//    Text speedValue;

//    Rigidbody bodyRigidBody;

//    public void Start()
//    {
//        bodyRigidBody.GetComponent<Rigidbody>();
//    }

//    public void FixedUpdate()
//    {
//        float motor = maxMotorTorque * Input.GetAxis("Vertical");
//        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");
//        float rearSteering = (maxSteeringAngle / 2) * -Input.GetAxis("Horizontal");
//        float currentSpeed;

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
//            }
//            if (axleInfo.rearSteering)
//            {
//                axleInfo.leftWheel.steerAngle = rearSteering;
//                axleInfo.rightWheel.steerAngle = rearSteering;
//            }
//            if (Input.GetKey(KeyCode.Space) && readyToJump)
//            {
//                //GetComponent<Rigidbody>().AddForce(new Vector3(0, 1, 0) * jumpHeight);
//                GetComponent<Rigidbody>().AddForceAtPosition(axleInfo.leftWheel.transform.up * jumpHeight, axleInfo.leftWheel.transform.position);
//                GetComponent<Rigidbody>().AddForceAtPosition(axleInfo.rightWheel.transform.up * jumpHeight, axleInfo.rightWheel.transform.position);
//                readyToJump = false;
//            }
//            //WheelHit hit;
//            //float travelL = 1.0f;
//            //float travelR = 1.0f;

//            //var groundedL = axleInfo.leftWheel.GetGroundHit(out hit);
//            //if (groundedL)
//            //{
//            //    travelL = (axleInfo.leftWheel.transform.InverseTransformPoint(hit.point).y - axleInfo.leftWheel.radius) / axleInfo.leftWheel.suspensionDistance;
//            //}
//            //var groundedR = axleInfo.rightWheel.GetGroundHit(out hit);
//            //if (groundedR)
//            //{
//            //    travelL = (axleInfo.rightWheel.transform.InverseTransformPoint(hit.point).y - axleInfo.rightWheel.radius) / axleInfo.rightWheel.suspensionDistance;
//            //}
//            //float antiRollForce = (travelL - travelR) * antiRoll;
//            //if (groundedL)
//            //{
//            //    GetComponent<Rigidbody>().AddForceAtPosition(axleInfo.leftWheel.transform.up * antiRollForce, axleInfo.leftWheel.transform.position);
//            //}
//            //if (groundedR)
//            //{
//            //    GetComponent<Rigidbody>().AddForceAtPosition(axleInfo.rightWheel.transform.up * antiRollForce, axleInfo.rightWheel.transform.position);
//            //}

//            if (axleInfo.breaks && Input.GetKey(KeyCode.C))
//            {
//                print(axleInfo.leftWheel.suspensionDistance);
//                axleInfo.leftWheel.brakeTorque = breakTorque;
//                axleInfo.rightWheel.brakeTorque = breakTorque;
//            }
//            else if (axleInfo.breaks && !Input.GetKey(KeyCode.C))
//            {
//                axleInfo.leftWheel.brakeTorque = 0;
//                axleInfo.rightWheel.brakeTorque = 0;
//            }

//            ApplyLocalPositionToVisuals(axleInfo.leftWheel);
//            ApplyLocalPositionToVisuals(axleInfo.rightWheel);

//            //currentSpeed = speedometer(axleInfo.leftWheel);
//            ////currentSpeed = speedometer(axleInfo.rightWheel) + currentSpeed;
//            //currentSpeed = currentSpeed / 2;
//            //speedValue.text = currentSpeed.ToString();
//        }

//        if (Input.GetKeyDown(KeyCode.LeftShift))
//        {
//            StartCoroutine(Boost(2));
//        }

//        speedValue.text = bodyRigidBody.velocity.magnitude.ToString();
//    }

//    // finds the corresponding visual wheel
//    // correctly applies the transform
//    public void ApplyLocalPositionToVisuals(WheelCollider collider)
//    {
//        if (collider.transform.childCount == 0)
//        {
//            return;
//        }

//        Transform visualWheel = collider.transform.GetChild(0);

//        //Vector3 position;
//        //Quaternion rotation;

//        visualWheel.localEulerAngles = new Vector3(visualWheel.localEulerAngles.x, collider.steerAngle - visualWheel.localEulerAngles.z, visualWheel.localEulerAngles.z);
//        visualWheel.Rotate(collider.rpm / 60 * 360 * Time.deltaTime, 0, 0);

//        //collider.GetWorldPose(out position, out rotation);

//        //visualWheel.transform.position = position;
//        //visualWheel.transform.rotation = rotation;
//    }

//    IEnumerator Boost(int BoostDuration)
//    {
//        readyToBoost = false;
//        GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, 10000000));
//        //maxMotorTorque = maxMotorTorque * 2;
//        yield return new WaitForSeconds(BoostDuration);
//        //maxMotorTorque = maxMotorTorque / 2;
//        readyToBoost = true;
//    }
//    //private void OnCollisionEnter(Collision collision)
//    //{
//    //    if (collision.gameObject.tag == "Holo")
//    //    {
//    //        readyToJump = true;
//    //    }
//    //}

//    void OnTriggerEnter(Collider other)
//    {
//        if (other.gameObject.CompareTag("Coin"))
//        {
//            other.gameObject.SetActive(false);
//        }
//        if (other.gameObject.CompareTag("Floor"))
//        {
//            readyToJump = true;
//        }
//    }

//    float speedometer(WheelCollider inputWheel)
//    {
//        float wheelRadius = inputWheel.radius; // put here your wheel radius
//        float wheelRpm = inputWheel.rpm;
//        float circumFerence; //here we will store the circumFerence
//        float speedOnKmh; // here the speed in kilometers in hour
//        float speedOnMph; // and here the speed in mhiles in hour

//        circumFerence = 2.0f * 3.14f * wheelRadius; // Finding circumFerence 2 Pi R
//        speedOnKmh = (circumFerence * wheelRpm) * 60; // finding kmh
//        speedOnMph = speedOnKmh * 0.62f; // converting kmh to mph

//        return speedOnMph;
//    }

//}

//[System.Serializable]
//public class AxleInfo
//{
//    public WheelCollider leftWheel;
//    public WheelCollider rightWheel;
//    public bool motor; // is this wheel attached to motor?
//    public bool steering; // does this wheel apply steer angle?
//    public bool rearSteering;
//    public bool breaks;
//}