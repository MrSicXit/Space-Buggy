//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;

//public class SimpleCarController : MonoBehaviour
//{
//	public List<AxleInfo> axleInfos; // the information about each individual axle
//	public float maxMotorTorque; // maximum torque the motor can apply to wheel
//	public float maxSteeringAngle; // maximum steer angle the wheel can have
//	public float breakTorque;
//	public float jumpHeight;
//	public bool readyToJump;
//	public bool readyToBoost;

//	public void FixedUpdate()
//	{
//		float motor = maxMotorTorque * Input.GetAxis("Vertical");
//		float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

//		foreach (AxleInfo axleInfo in axleInfos)
//		{
//			if (axleInfo.steering)
//			{
//				axleInfo.leftWheel.steerAngle = steering;
//				axleInfo.rightWheel.steerAngle = steering;
//			}
//			if (axleInfo.motor)
//			{
//				axleInfo.leftWheel.motorTorque = motor;
//				axleInfo.rightWheel.motorTorque = motor;
//			}
//			if (axleInfo.breaks && Input.GetKey(KeyCode.C))
//			{
//				axleInfo.leftWheel.brakeTorque = breakTorque;
//				axleInfo.rightWheel.brakeTorque = breakTorque;
//			} else if (axleInfo.breaks && !Input.GetKey(KeyCode.C))
//			{
//				axleInfo.leftWheel.brakeTorque = 0;
//				axleInfo.rightWheel.brakeTorque = 0;
//			}

//            ApplyLocalPositionToVisuals(axleInfo.leftWheel);
//            ApplyLocalPositionToVisuals(axleInfo.rightWheel);

//		}
//		if (Input.GetKey(KeyCode.Space) && readyToJump)
//		{
//			GetComponent<Rigidbody>().AddForce(new Vector3(0, 1, 0) * jumpHeight);
//			readyToJump = false;
//		}
//		if (Input.GetKeyDown(KeyCode.LeftShift))
//		{
//			StartCoroutine(Boost(2));
//		}
        

//	}

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
//	{
//		readyToBoost = false;
//		maxMotorTorque = maxMotorTorque * 2;
//		yield return new WaitForSeconds(BoostDuration);
//		maxMotorTorque = maxMotorTorque / 2;
//		readyToBoost = true;
//	}
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
//}

//[System.Serializable]
//public class AxleInfo
//{
//	public WheelCollider leftWheel;
//	public WheelCollider rightWheel;
//	public bool motor; // is this wheel attached to motor?
//	public bool steering; // does this wheel apply steer angle?
//	public bool breaks;
//}