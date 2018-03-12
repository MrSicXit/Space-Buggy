using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Energy : MonoBehaviour {
    private int energyTotal;
    private int energy;
    private int energyPerTick;
    private float secondsPerTick;
    private Text energyBar;

	// Use this for initialization
	void Start () {
        energyTotal = 100;
        energy = 100;
        energyPerTick = 5;
        secondsPerTick = 2f;
        StartCoroutine(EnergyRegen());
        energyBar = GameObject.Find("Canvas/Energy").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (energy >= 10)
            {
                energy -= 10;
            }
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            StartCoroutine(EnergyBoost(5, 10));
        }

        energyBar.text = "Energy: " + energy + "%";
	}

    bool CheckEnergy(int energyRequired)
    {
        if (energy >= energyRequired)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void UseEnergy(int energyUsed)
    {
        energy -= energyUsed;
    }

    IEnumerator EnergyRegen()
    {
        while (true)
        {
            if (energy < energyTotal)
            {
                if (energy + energyPerTick > energyTotal)
                {
                    yield return new WaitForSeconds(secondsPerTick);
                    energy = energyTotal;
                }
                else
                {
                    yield return new WaitForSeconds(secondsPerTick);
                    energy += energyPerTick;
                }
            } else
            {
                yield return new WaitForSeconds(secondsPerTick);
            }
        }
    }

    void AddEnergy(int energyAmmount)
    {
        if(energy + energyAmmount > energyTotal)
        {
            energy = energyTotal;
        } else
        {
            energy += energyAmmount;
        }
    }

    IEnumerator EnergyBoost(float Time, float boostAmmount)
    {
        secondsPerTick = secondsPerTick / boostAmmount;
        yield return new WaitForSeconds(Time);
        secondsPerTick = secondsPerTick * boostAmmount;
    }
}
