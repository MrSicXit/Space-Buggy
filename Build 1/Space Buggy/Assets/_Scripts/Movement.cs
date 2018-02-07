using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    private GameObject[] wheels = new GameObject[4];
    private Rigidbody bodyRB;

	// Use this for initialization
	void Start () {
        for (int x = 0; x <= 3; x++)
        {
            wheels[x] = GameObject.Find(gameObject.name+("/Wheel" + x));
            Debug.Log(wheels[x]);
         }
        bodyRB = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.W))
        {
            for (int x = 0; x <= 3; x++)
            {
                if (wheels[x].GetComponent<Collider>().enabled == true)
                {
                    Vector3 worldForcePosition = transform.TransformPoint(wheels[x].transform.position);
                    Vector3 speed = new Vector3(-5f, 0f, 0f);
                    bodyRB.AddForceAtPosition(speed, worldForcePosition);
                }
            }
        }
	}

    void Accelerate()
    {

    }
}
