using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelChanges : MonoBehaviour {

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

    WheelFrictionCurve[] fFrictionCurve;
    WheelFrictionCurve[] sFrictionCurve;
    WheelCollider[] wheelColliders;


    // Use this for initialization
    void Start () {
        wheelColliders = GetComponentsInChildren<WheelCollider>();
        fFrictionCurve = new WheelFrictionCurve[wheelColliders.Length];
        sFrictionCurve = new WheelFrictionCurve[wheelColliders.Length];
        for (int x = 0; x <= wheelColliders.Length; x++) {
            fFrictionCurve[x] = wheelColliders[x].forwardFriction;
            sFrictionCurve[x] = wheelColliders[x].sidewaysFriction;
        }
        WheelSwitch(1);

    }

    // Update is called once per frame
    void Update () {
		if (Input.GetKey(KeyCode.Alpha1))
        {
            wheelType = 1;
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            wheelType = 2;
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            wheelType = 3;
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
                break;
            default:
                print("Error: Wheel type not set");
                return;
        }

        for (int x = 0; x <= fFrictionCurve.Length; x++)
        {
            fFrictionCurve[x].extremumSlip = fExtremumSlip;
            fFrictionCurve[x].extremumValue = fExremumValue;
            fFrictionCurve[x].asymptoteSlip = fAsymptoteSlip;
            fFrictionCurve[x].asymptoteValue = fAsymptoteValue;
            fFrictionCurve[x].stiffness = fStiffness;
            sFrictionCurve[x].extremumSlip = sExtremumSlip;
            sFrictionCurve[x].extremumValue = sExtremumValue;
            sFrictionCurve[x].asymptoteSlip = sAsymptoteSlip;
            sFrictionCurve[x].asymptoteValue = sAsymptoteValue;
            sFrictionCurve[x].stiffness = sStiffness;

            //Debug.Log(sFrictionCurve.stiffness);
        }
    }
}
