﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour {

    //Declare Variables
    [SerializeField]
    int playerNumber;

	public int wheelType;
	private bool holoTyres;
    private float fExtremumSlip;
    private float fExremumValue;
    private float fAsymptoteSlip;
    private float fAsymptoteValue;
    private float fStiffness;
    private float sExtremumSlip;
    private float sExtremumValue;
    private float sAsymptoteSlip;
    private float sAsymptoteValue;
    private float sStiffness;
    private WheelFrictionCurve fFrictionCurve;
    private WheelFrictionCurve sFrictionCurve;

	// Use this for initialization
	void Start () {
        fFrictionCurve = GetComponent<WheelCollider>().forwardFriction;
        sFrictionCurve = GetComponent<WheelCollider>().sidewaysFriction;
        WheelSwitch(wheelType);

    }

    // Update is called once per frame
    void Update () {
        if (playerNumber == 1 && Input.GetKeyDown(KeyCode.E))
        {
            if (wheelType <= 2)
            {
                wheelType = wheelType + 1;
            }
            else
            {
                wheelType = 1;
            }
        }
        else if (playerNumber == 2 && Input.GetKeyDown(KeyCode.RightControl))
        {
            if (wheelType <= 2)
            {
                wheelType = wheelType + 1;
            }
            else
            {
                wheelType = 1;
            }
        }
        WheelSwitch(wheelType);
	}

    public void WheelSwitch(int wheelType)
    {
        switch (wheelType)
        {
            //Default
            case 1:
                holoTyres = true;
                fExtremumSlip = 0.5f;
                fExremumValue = 1f;
                fAsymptoteSlip = 0.8f;
                fAsymptoteValue = 0.5f;
                fStiffness = 0.5f;
                sExtremumSlip = 0.5f;
                sExtremumValue = 1f;
                sAsymptoteSlip = 0.5f;
                sAsymptoteValue = 0.75f;
                sStiffness = 0.5f;
                break;
            //Testing out different variables
            case 2:
                holoTyres = false;
                fExtremumSlip = 1.5f;
                fExremumValue = 2f;
                fAsymptoteSlip = 2f;
                fAsymptoteValue = 1f;
                fStiffness = 2.2f;
                sExtremumSlip = 1.5f;
                sExtremumValue = 2f;
                sAsymptoteSlip = 1.8f;
                sAsymptoteValue = 1.5f;
                sStiffness = 2.2f;
                break;
            case 3:
                //lava
                break;
            default:
                print("Error: Wheel type not set");
                return;
        }

        fFrictionCurve.extremumSlip = fExtremumSlip;
        fFrictionCurve.extremumValue = fExremumValue;
        fFrictionCurve.asymptoteSlip = fAsymptoteSlip;
        fFrictionCurve.asymptoteValue = fAsymptoteValue;
        fFrictionCurve.stiffness = fStiffness;
        sFrictionCurve.extremumSlip = sExtremumSlip;
        sFrictionCurve.extremumValue = sExtremumValue;
        sFrictionCurve.asymptoteSlip = sAsymptoteSlip;
        sFrictionCurve.asymptoteValue = sAsymptoteValue;
        sFrictionCurve.stiffness = sStiffness;

        //Debug.Log(sFrictionCurve.stiffness);
    }
}
