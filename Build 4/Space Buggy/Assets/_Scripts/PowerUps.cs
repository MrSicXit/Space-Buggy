using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUps : MonoBehaviour
{
    [SerializeField]
    int slotOne = -1;
    [SerializeField]
    int slotTwo = -1;
    [SerializeField]
    RectTransform panelOne;
    [SerializeField]
    RectTransform panelTwo;
    [SerializeField]
    Sprite spriteOnPanelOne;
    [SerializeField]
    Sprite spriteOnPanelTwo;
    [SerializeField]
    PlayerID playerID;
    [SerializeField]
    Image pOnePowerOneImage;
    [SerializeField]
    Image pOnePowerTwoImage;
    [SerializeField]
    Image pTwoPowerOneImage;
    [SerializeField]
    Image pTwoPowerTwoImage;
    [SerializeField]
    PowerUpGlobalManager powerUpGlobalInfo;
    [SerializeField]
    BoxCollider boxColliderOfThePlayer;
    [SerializeField]
    bool OnLowGravity=false;
    [SerializeField]
    Rigidbody playerRigidbody;
    [SerializeField]
    int lowGravityIndex;//SETTING HERE TO TEST THE TRIGGERING OF A POWER, needs to lookup at an array later when powers implemented
    [SerializeField]
    Vector3 lowGravityVector;

    void Awake()
    {
        lowGravityVector = new Vector3(0, 7, 0);
    }

    // Use this for initialization
    void Start()
    {
        playerID = gameObject.GetComponent<PlayerID>();
        pOnePowerOneImage = FindObjectOfType<CameraLoad>().playerOneHUDPowerUpOne.GetComponent<Image>();
        pOnePowerTwoImage = FindObjectOfType<CameraLoad>().playerOneHUDPowerUpTwo.GetComponent<Image>();
        pTwoPowerOneImage = FindObjectOfType<CameraLoad>().playerTwoHUDPowerUpOne.GetComponent<Image>();
        pTwoPowerTwoImage = FindObjectOfType<CameraLoad>().playerTwoHUDPowerUpTwo.GetComponent<Image>();
        powerUpGlobalInfo = FindObjectOfType<PowerUpGlobalManager>();
        boxColliderOfThePlayer = GetComponent<BoxCollider>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
            if (Input.GetKeyDown("1"))
            {
                if (playerID.getPlayerID == 0)
                    SpendPowerUp(true, 0);
            }
            if (Input.GetKeyDown("2"))
            {
                if (playerID.getPlayerID == 0)
                    SpendPowerUp(false, 0);
            }
            if (Input.GetKeyDown("8"))
            {
                if (playerID.getPlayerID == 1)
                    SpendPowerUp(true, 1);
            }
            if (Input.GetKeyDown("9"))
            {
                if (playerID.getPlayerID == 1)
                    SpendPowerUp(false, 1);
            }
    }

    bool AddPowerUp(int powerUpIdentifier, int playerID)
    {
        if (slotOne == -1)
        {
            slotOne = powerUpIdentifier;
            if(playerID==0)
            {
                pOnePowerOneImage.sprite = powerUpGlobalInfo.GetPowerUpSpriteWithIndex(powerUpIdentifier);
                pOnePowerOneImage.color = Color.white;
            }
            else
            {
                pTwoPowerOneImage.sprite = powerUpGlobalInfo.GetPowerUpSpriteWithIndex(powerUpIdentifier);
                pTwoPowerOneImage.color = Color.white;
            }
            return true;
        }
        else
        {
            if (slotTwo == -1)
            {
                slotTwo = powerUpIdentifier;
                if (playerID == 0)
                {
                    pOnePowerTwoImage.sprite = powerUpGlobalInfo.GetPowerUpSpriteWithIndex(powerUpIdentifier);
                    pOnePowerTwoImage.color = Color.white;
                }
                else
                {
                    pTwoPowerTwoImage.sprite = powerUpGlobalInfo.GetPowerUpSpriteWithIndex(powerUpIdentifier);
                    pTwoPowerTwoImage.color = Color.white;
                }
                return true;
            }
            else
            {
                //SLOTS ARE FULL
                return false;
            }
        }
    }

    bool OpenBox(int playerID)
    {
        if (AddPowerUp((int)Mathf.Floor(Random.Range(0, 5.4f)),playerID))
            return true;
        else
            return false;
    }

    void SpendPowerUp(bool usingSlotOne, int playerID)
    {
        int powerUpIndex = -1;//the power up to be executed will be NONE in this case
        if ((usingSlotOne) && (slotOne > -1))//if we use first slot and this one isn't empty
        {
            powerUpIndex = slotOne;//we store the power up at powerUpIndex for use below
            if (playerID == 0)
            {
                StartCoroutine(GreyAPowerUpHUDIcon(usingSlotOne, playerID,powerUpGlobalInfo.GetPowerUpDuration(powerUpIndex)));
            }
            else
            {
               StartCoroutine(GreyAPowerUpHUDIcon(usingSlotOne, playerID, powerUpGlobalInfo.GetPowerUpDuration(powerUpIndex)));
            }
        }
        else
        {
            if ((!usingSlotOne) && (slotTwo > -1))//if we use second slot and it isn't empty
            {
                powerUpIndex = slotTwo;//we store the power up at powerUpIndex for use below
                if (playerID == 0)
                {
                    StartCoroutine(GreyAPowerUpHUDIcon(usingSlotOne,playerID, powerUpGlobalInfo.GetPowerUpDuration(powerUpIndex)));
                }
                else
                {
                   StartCoroutine(GreyAPowerUpHUDIcon(usingSlotOne, playerID, powerUpGlobalInfo.GetPowerUpDuration(powerUpIndex)));
                }
            }
            //The user called for an empty power-up slot
        }

        //THIS COULD BE A SWITCH WIHEN REAL POWER UPS IMPLEMENTED

        if (powerUpIndex == lowGravityIndex)
        {
            StartCoroutine(LowGravityPlaceholder());
        }
        else
        {
            if (!(powerUpIndex == -1))
            {
                StartCoroutine(FakeCoroutine(powerUpGlobalInfo.GetPowerUpDuration(powerUpIndex), powerUpIndex));
            }
            else
            {
                //The slot the player is trying to use it's empty
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PowerBox")
        {
            if (OpenBox(playerID.getPlayerID))
                other.gameObject.SetActive(false);
        }
    }

    IEnumerator GreyAPowerUpHUDIcon(bool isSlotOne, int playerID, float time)
    {
        float elapsedTime = 0;
        if (playerID == 0)
        {
            if(isSlotOne)
            {
                pOnePowerOneImage.color = Color.grey;
                while (elapsedTime < time)
                {

                    elapsedTime += Time.deltaTime;
                    yield return null;
                }
                slotOne = -1;//and empty the slot
                pOnePowerOneImage.sprite = powerUpGlobalInfo.GetPowerUpSpriteWithIndex(-1);
                pOnePowerOneImage.color = Color.clear;
                boxColliderOfThePlayer.enabled = false;
                boxColliderOfThePlayer.enabled = true;
            }
            else
            {
                pOnePowerTwoImage.color = Color.grey;
                while (elapsedTime < time)
                {

                    elapsedTime += Time.deltaTime;
                    yield return null;
                }
                slotTwo = -1;
                //NEEDS TO CHECK IF ITS ON TOP OF ANOTHER BOX
                pOnePowerTwoImage.sprite = powerUpGlobalInfo.GetPowerUpSpriteWithIndex(-1);
                pOnePowerTwoImage.color = Color.clear;
                boxColliderOfThePlayer.enabled = false;
                boxColliderOfThePlayer.enabled = true;
            }
        }
        else
        {
            if (isSlotOne)
            {
                pTwoPowerOneImage.color = Color.grey;
                while (elapsedTime < time)
                {

                    elapsedTime += Time.deltaTime;
                    yield return null;
                }
                slotOne = -1;
                //NEEDS TO CHECK IF ITS ON TOP OF ANOTHER BOX
                pTwoPowerOneImage.sprite = powerUpGlobalInfo.GetPowerUpSpriteWithIndex(-1);
                pTwoPowerOneImage.color = Color.clear;
                boxColliderOfThePlayer.enabled = false;
                boxColliderOfThePlayer.enabled = true;
            }
            else
            {
                pTwoPowerTwoImage.color = Color.grey;
                while (elapsedTime < time)
                {

                    elapsedTime += Time.deltaTime;
                    yield return null;
                }
                slotTwo = -1;
                //NEEDS TO CHECK IF ITS ON TOP OF ANOTHER BOX
                pTwoPowerTwoImage.sprite = powerUpGlobalInfo.GetPowerUpSpriteWithIndex(-1);
                pTwoPowerTwoImage.color = Color.clear;
                boxColliderOfThePlayer.enabled = false;
                boxColliderOfThePlayer.enabled = true;
            }
        }
    }

    IEnumerator FakeCoroutine(float time, int index)
    {
        float elapsedTime = 0;
        Debug.Log("Hi its Player "+(playerID.getPlayerID+1)+" Ima start coroutine with index " + index+" time now "+Time.time.ToString());

        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;

            yield return null;
        }
        Debug.Log("Hi its Player "+(playerID.getPlayerID+1)+" coroutine with index " + index + " done at time "+Time.time.ToString());
    }

    IEnumerator LowGravityPlaceholder()
    {
        float elapsedTime = 0;
        OnLowGravity = true;
        while(elapsedTime<powerUpGlobalInfo.GetPowerUpDuration(lowGravityIndex))
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        OnLowGravity = false;
    }

    void FixedUpdate()
    {
        if(OnLowGravity)
        {
            playerRigidbody.AddForce(lowGravityVector, ForceMode.Acceleration);
        }
    }

}
