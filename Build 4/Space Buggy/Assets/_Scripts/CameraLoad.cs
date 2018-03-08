using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Class to setup camera positions on up to 4 players. Each player needs to have a camera attached
public class CameraLoad : MonoBehaviour
{
   //public GameObject[] arrayPlayerCanvas;

    public GameObject playerOneHUDPowerUpSlotOne;
    public GameObject playerOneHUDPowerUpSlotTwo;
    public GameObject playerOneHUDPowerUpOne;
    public GameObject playerOneHUDPowerUpTwo;
    public GameObject canvasPlayerOne;
    public GameObject playerTwoHUDPowerUpSlotOne;
    public GameObject playerTwoHUDPowerUpSlotTwo;
    public GameObject playerTwoHUDPowerUpOne;
    public GameObject playerTwoHUDPowerUpTwo;
    public GameObject canvasPlayerTwo;

    void LoadOnSlotOne(int index)
    {
        if (index == -1)
        {

        }
    }

    // Use this for initialization
    void Start()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        //setting them on order according to player's order
        for (int i = 0; i < players.Length; i++)
        {
            for (int ii = 0; ii < players.Length - 1; ii++)
            {
                if (players[ii].gameObject.GetComponentInChildren<PlayerID>().getPlayerID == i)
                {
                    GameObject tmpObject = players[i];
                    players[i] = players[ii];
                    players[ii] = tmpObject;
                }

            }
        }

        //2 players setup P1 on left, P2 on the right
        if (players.Length >= 2)
        {
            //FINDING BOTH PLAYERS CAMERAS
            players[0].GetComponentInChildren<Camera>().rect = new Rect(new Vector2(0, 0), new Vector2(0.5f, 1));
            players[1].GetComponentInChildren<Camera>().rect = new Rect(new Vector2(0.5f, 0), new Vector2(0.5f, 1));

            //CREATING CANVAS FOR PLAYER ONE
            canvasPlayerOne = new GameObject("hubPlayerOne", new System.Type[] { typeof(CanvasScaler), typeof(GraphicRaycaster) });
            canvasPlayerOne.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
            canvasPlayerOne.GetComponent<Canvas>().worldCamera = players[0].GetComponentInChildren<Camera>();
            canvasPlayerOne.GetComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasPlayerOne.GetComponent<CanvasScaler>().matchWidthOrHeight = 0.5f;

            //CREATING CANVAS FOR PLAYER TWO
            GameObject canvasPlayerTwo = new GameObject("hubPlayerTwo", new System.Type[] { typeof(CanvasScaler), typeof(GraphicRaycaster) });
            canvasPlayerTwo.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
            canvasPlayerTwo.GetComponent<Canvas>().worldCamera = players[1].GetComponentInChildren<Camera>();
            canvasPlayerTwo.GetComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasPlayerTwo.GetComponent<CanvasScaler>().matchWidthOrHeight = 0.5f;

            //PLAYER ONE, SLOT ONE PANEL (FRAME)
            playerOneHUDPowerUpSlotOne = new GameObject("panelPowerSlotOne", typeof(Image));
            playerOneHUDPowerUpSlotOne.transform.SetParent(canvasPlayerOne.transform);
            playerOneHUDPowerUpSlotOne.GetComponent<Image>().sprite = FindObjectOfType<PowerUpGlobalManager>().GetSpriteForPowerUpHUDPanel();
            playerOneHUDPowerUpSlotOne.GetComponent<Image>().type = Image.Type.Sliced;
            playerOneHUDPowerUpSlotOne.GetComponent<RectTransform>().localRotation = new Quaternion(0, 0, 0, 0);
            playerOneHUDPowerUpSlotOne.GetComponent<RectTransform>().localScale = new Vector3(0.6f, 0.6f, 1);
            playerOneHUDPowerUpSlotOne.GetComponent<RectTransform>().anchorMin = new Vector2(0.49f, 1);
            playerOneHUDPowerUpSlotOne.GetComponent<RectTransform>().anchorMax = new Vector2(0.49f, 1);
            playerOneHUDPowerUpSlotOne.GetComponent<RectTransform>().pivot = new Vector2(1, 1);
            playerOneHUDPowerUpSlotOne.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, -5, 0);
            playerOneHUDPowerUpSlotOne.GetComponent<Image>().fillCenter = false;
            playerOneHUDPowerUpSlotOne.GetComponent<Image>().color = new Color(0.8f, 0.3f, 1, 0.4f);

            //PLAYER ONE, SLOT ONE IMAGE
            playerOneHUDPowerUpOne = new GameObject("PowerUpOne", typeof(Image));
            playerOneHUDPowerUpOne.transform.SetParent(playerOneHUDPowerUpSlotOne.transform);
            playerOneHUDPowerUpOne.GetComponent<Image>().sprite = FindObjectOfType<PowerUpGlobalManager>().GetPowerUpSpriteWithIndex(255);
            playerOneHUDPowerUpOne.GetComponent<Image>().color = Color.clear;

            playerOneHUDPowerUpOne.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 1);
            playerOneHUDPowerUpOne.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 1);
            playerOneHUDPowerUpOne.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 1);
            playerOneHUDPowerUpOne.GetComponent<RectTransform>().localRotation = new Quaternion(0, 0, 0, 0);
            playerOneHUDPowerUpOne.GetComponent<RectTransform>().localScale = new Vector3(0.9f, 0.9f, 1);
            playerOneHUDPowerUpOne.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, -5, 0);


            //PLAYER ONE, SLOT TWO PANEL (FRAME)
            playerOneHUDPowerUpSlotTwo = new GameObject("panelPowerSlotTwo", typeof(Image));
            playerOneHUDPowerUpSlotTwo.transform.SetParent(canvasPlayerOne.transform);
            playerOneHUDPowerUpSlotTwo.GetComponent<Image>().sprite = FindObjectOfType<PowerUpGlobalManager>().GetSpriteForPowerUpHUDPanel();
            playerOneHUDPowerUpSlotTwo.GetComponent<Image>().type = Image.Type.Sliced;
            playerOneHUDPowerUpSlotTwo.GetComponent<RectTransform>().localRotation = new Quaternion(0, 0, 0, 0);
            playerOneHUDPowerUpSlotTwo.GetComponent<RectTransform>().localScale = new Vector3(0.6f, 0.6f, 1);
            playerOneHUDPowerUpSlotTwo.GetComponent<RectTransform>().anchorMin = new Vector2(0.51f, 1);
            playerOneHUDPowerUpSlotTwo.GetComponent<RectTransform>().anchorMax = new Vector2(0.51f, 1);
            playerOneHUDPowerUpSlotTwo.GetComponent<RectTransform>().pivot = new Vector2(0, 1);
            playerOneHUDPowerUpSlotTwo.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, -5, 0);
            playerOneHUDPowerUpSlotTwo.GetComponent<Image>().fillCenter = false;
            playerOneHUDPowerUpSlotTwo.GetComponent<Image>().color = new Color(0.8f, 0.3f, 1, 0.4f);
            //playerOneHUDPowerUpSlotTwo.GetComponent<Image>().color = new Color(0, 0, 0, 0);

            //PLAYER ONE, SLOT TWO IMAGE
            playerOneHUDPowerUpTwo = new GameObject("PowerUpTwo", typeof(Image));
            playerOneHUDPowerUpTwo.transform.SetParent(playerOneHUDPowerUpSlotTwo.transform);
            playerOneHUDPowerUpTwo.GetComponent<Image>().sprite = FindObjectOfType<PowerUpGlobalManager>().GetPowerUpSpriteWithIndex(255);
            playerOneHUDPowerUpTwo.GetComponent<Image>().color = Color.clear;

            playerOneHUDPowerUpTwo.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 1);
            playerOneHUDPowerUpTwo.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 1);
            playerOneHUDPowerUpTwo.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 1);
            playerOneHUDPowerUpTwo.GetComponent<RectTransform>().localRotation = new Quaternion(0, 0, 0, 0);
            playerOneHUDPowerUpTwo.GetComponent<RectTransform>().localScale = new Vector3(0.9f, 0.9f, 1);
            playerOneHUDPowerUpTwo.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, -5, 0);


            //PLAYER TWO, SLOT ONE PANEL (FRAME)
            playerTwoHUDPowerUpSlotOne = new GameObject("panelPowerSlotOne", typeof(Image));
            playerTwoHUDPowerUpSlotOne.transform.SetParent(canvasPlayerTwo.transform);
            playerTwoHUDPowerUpSlotOne.GetComponent<Image>().sprite = FindObjectOfType<PowerUpGlobalManager>().GetSpriteForPowerUpHUDPanel();
            playerTwoHUDPowerUpSlotOne.GetComponent<Image>().type = Image.Type.Sliced;
            playerTwoHUDPowerUpSlotOne.GetComponent<RectTransform>().localRotation = new Quaternion(0, 0, 0, 0);
            playerTwoHUDPowerUpSlotOne.GetComponent<RectTransform>().localScale = new Vector3(0.6f, 0.6f, 1);
            playerTwoHUDPowerUpSlotOne.GetComponent<RectTransform>().anchorMin = new Vector2(0.49f, 1);
            playerTwoHUDPowerUpSlotOne.GetComponent<RectTransform>().anchorMax = new Vector2(0.49f, 1);
            playerTwoHUDPowerUpSlotOne.GetComponent<RectTransform>().pivot = new Vector2(1, 1);
            playerTwoHUDPowerUpSlotOne.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, -5, 0);
            playerTwoHUDPowerUpSlotOne.GetComponent<Image>().fillCenter = false;
            playerTwoHUDPowerUpSlotOne.GetComponent<Image>().color = new Color(0.8f, 0.3f, 1, 0.4f);
            //playerTwoHUDPowerUpSlotOne.GetComponent<Image>().color = new Color(0, 0, 0, 0);

            //PLAYER TWO, SLOT ONE IMAGE
            playerTwoHUDPowerUpOne = new GameObject("PowerUpOne", typeof(Image));
            playerTwoHUDPowerUpOne.transform.SetParent(playerTwoHUDPowerUpSlotOne.transform);
            playerTwoHUDPowerUpOne.GetComponent<Image>().sprite = FindObjectOfType<PowerUpGlobalManager>().GetPowerUpSpriteWithIndex(255);
            playerTwoHUDPowerUpOne.GetComponent<Image>().color = Color.clear;

            playerTwoHUDPowerUpOne.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 1);
            playerTwoHUDPowerUpOne.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 1);
            playerTwoHUDPowerUpOne.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 1);
            playerTwoHUDPowerUpOne.GetComponent<RectTransform>().localRotation = new Quaternion(0, 0, 0, 0);
            playerTwoHUDPowerUpOne.GetComponent<RectTransform>().localScale = new Vector3(0.9f, 0.9f, 1);
            playerTwoHUDPowerUpOne.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, -5, 0);


            //PLAYER TWO, SLOT TWO PANEL (FRAME)
            playerTwoHUDPowerUpSlotTwo = new GameObject("panelPowerSlotTwo", typeof(Image));
            playerTwoHUDPowerUpSlotTwo.transform.SetParent(canvasPlayerTwo.transform);
            playerTwoHUDPowerUpSlotTwo.GetComponent<Image>().sprite = FindObjectOfType<PowerUpGlobalManager>().GetSpriteForPowerUpHUDPanel();
            playerTwoHUDPowerUpSlotTwo.GetComponent<Image>().type = Image.Type.Sliced;
            playerTwoHUDPowerUpSlotTwo.GetComponent<RectTransform>().localRotation = new Quaternion(0, 0, 0, 0);
            playerTwoHUDPowerUpSlotTwo.GetComponent<RectTransform>().localScale = new Vector3(0.6f, 0.6f, 1);
            playerTwoHUDPowerUpSlotTwo.GetComponent<RectTransform>().anchorMin = new Vector2(0.51f, 1);
            playerTwoHUDPowerUpSlotTwo.GetComponent<RectTransform>().anchorMax = new Vector2(0.51f, 1);
            playerTwoHUDPowerUpSlotTwo.GetComponent<RectTransform>().pivot = new Vector2(0, 1);
            playerTwoHUDPowerUpSlotTwo.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, -5, 0);
            playerTwoHUDPowerUpSlotTwo.GetComponent<Image>().fillCenter = false;
            playerTwoHUDPowerUpSlotTwo.GetComponent<Image>().color = new Color(0.8f, 0.3f, 1, 0.4f);

            //Debug.Log("defaultColor"+playerTwoHUDPowerUpSlotTwo.GetComponent<Image>().color);
            //playerTwoHUDPowerUpSlotTwo.GetComponent<Image>().color = new Color(0,0,0,0);

            //PLAYER TWO, SLOT TWO IMAGE
            playerTwoHUDPowerUpTwo = new GameObject("PowerUpTwo", typeof(Image));
            playerTwoHUDPowerUpTwo.transform.SetParent(playerTwoHUDPowerUpSlotTwo.transform);
            playerTwoHUDPowerUpTwo.GetComponent<Image>().sprite = FindObjectOfType<PowerUpGlobalManager>().GetPowerUpSpriteWithIndex(255);
            playerTwoHUDPowerUpTwo.GetComponent<Image>().color = Color.clear;

            playerTwoHUDPowerUpTwo.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 1);
            playerTwoHUDPowerUpTwo.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 1);
            playerTwoHUDPowerUpTwo.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 1);
            playerTwoHUDPowerUpTwo.GetComponent<RectTransform>().localRotation = new Quaternion(0, 0, 0, 0);
            playerTwoHUDPowerUpTwo.GetComponent<RectTransform>().localScale = new Vector3(0.9f, 0.9f, 1);
            playerTwoHUDPowerUpTwo.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, -5, 0);


        }

        else//just one player
        {
            //CREATING CANVAS FOR PLAYER ONE
            canvasPlayerOne = new GameObject("hubPlayerOne", new System.Type[] { typeof(CanvasScaler), typeof(GraphicRaycaster) });
            canvasPlayerOne.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
            canvasPlayerOne.GetComponent<Canvas>().worldCamera = players[0].GetComponentInChildren<Camera>();
            canvasPlayerOne.GetComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasPlayerOne.GetComponent<CanvasScaler>().matchWidthOrHeight = 0.5f;

            //PLAYER ONE, SLOT ONE PANEL (FRAME)
            playerOneHUDPowerUpSlotOne = new GameObject("panelPowerSlotOne", typeof(Image));
            playerOneHUDPowerUpSlotOne.transform.SetParent(canvasPlayerOne.transform);
            playerOneHUDPowerUpSlotOne.GetComponent<Image>().sprite = FindObjectOfType<PowerUpGlobalManager>().GetSpriteForPowerUpHUDPanel();
            playerOneHUDPowerUpSlotOne.GetComponent<Image>().type = Image.Type.Sliced;
            playerOneHUDPowerUpSlotOne.GetComponent<RectTransform>().localRotation = new Quaternion(0, 0, 0, 0);
            playerOneHUDPowerUpSlotOne.GetComponent<RectTransform>().localScale = new Vector3(0.6f, 0.6f, 1);
            playerOneHUDPowerUpSlotOne.GetComponent<RectTransform>().anchorMin = new Vector2(0.49f, 1);
            playerOneHUDPowerUpSlotOne.GetComponent<RectTransform>().anchorMax = new Vector2(0.49f, 1);
            playerOneHUDPowerUpSlotOne.GetComponent<RectTransform>().pivot = new Vector2(1, 1);
            playerOneHUDPowerUpSlotOne.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, -5, 0);
            playerOneHUDPowerUpSlotOne.GetComponent<Image>().fillCenter = false;
            playerOneHUDPowerUpSlotOne.GetComponent<Image>().color = new Color(0.8f, 0.3f, 1, 0.4f);

            //PLAYER ONE, SLOT ONE IMAGE
            playerOneHUDPowerUpOne = new GameObject("PowerUpOne", typeof(Image));
            playerOneHUDPowerUpOne.transform.SetParent(playerOneHUDPowerUpSlotOne.transform);
            playerOneHUDPowerUpOne.GetComponent<Image>().sprite = FindObjectOfType<PowerUpGlobalManager>().GetPowerUpSpriteWithIndex(255);
            playerOneHUDPowerUpOne.GetComponent<Image>().color = Color.clear;

            playerOneHUDPowerUpOne.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 1);
            playerOneHUDPowerUpOne.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 1);
            playerOneHUDPowerUpOne.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 1);
            playerOneHUDPowerUpOne.GetComponent<RectTransform>().localRotation = new Quaternion(0, 0, 0, 0);
            playerOneHUDPowerUpOne.GetComponent<RectTransform>().localScale = new Vector3(0.9f, 0.9f, 1);
            playerOneHUDPowerUpOne.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, -5, 0);


            //PLAYER ONE, SLOT TWO PANEL (FRAME)
            playerOneHUDPowerUpSlotTwo = new GameObject("panelPowerSlotTwo", typeof(Image));
            playerOneHUDPowerUpSlotTwo.transform.SetParent(canvasPlayerOne.transform);
            playerOneHUDPowerUpSlotTwo.GetComponent<Image>().sprite = FindObjectOfType<PowerUpGlobalManager>().GetSpriteForPowerUpHUDPanel();
            playerOneHUDPowerUpSlotTwo.GetComponent<Image>().type = Image.Type.Sliced;
            playerOneHUDPowerUpSlotTwo.GetComponent<RectTransform>().localRotation = new Quaternion(0, 0, 0, 0);
            playerOneHUDPowerUpSlotTwo.GetComponent<RectTransform>().localScale = new Vector3(0.6f, 0.6f, 1);
            playerOneHUDPowerUpSlotTwo.GetComponent<RectTransform>().anchorMin = new Vector2(0.51f, 1);
            playerOneHUDPowerUpSlotTwo.GetComponent<RectTransform>().anchorMax = new Vector2(0.51f, 1);
            playerOneHUDPowerUpSlotTwo.GetComponent<RectTransform>().pivot = new Vector2(0, 1);
            playerOneHUDPowerUpSlotTwo.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, -5, 0);
            playerOneHUDPowerUpSlotTwo.GetComponent<Image>().fillCenter = false;
            playerOneHUDPowerUpSlotTwo.GetComponent<Image>().color = new Color(0.8f, 0.3f, 1, 0.4f);
            //playerOneHUDPowerUpSlotTwo.GetComponent<Image>().color = new Color(0, 0, 0, 0);

            //PLAYER ONE, SLOT TWO IMAGE
            playerOneHUDPowerUpTwo = new GameObject("PowerUpTwo", typeof(Image));
            playerOneHUDPowerUpTwo.transform.SetParent(playerOneHUDPowerUpSlotTwo.transform);
            playerOneHUDPowerUpTwo.GetComponent<Image>().sprite = FindObjectOfType<PowerUpGlobalManager>().GetPowerUpSpriteWithIndex(255);
            playerOneHUDPowerUpTwo.GetComponent<Image>().color = Color.clear;

            playerOneHUDPowerUpTwo.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 1);
            playerOneHUDPowerUpTwo.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 1);
            playerOneHUDPowerUpTwo.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 1);
            playerOneHUDPowerUpTwo.GetComponent<RectTransform>().localRotation = new Quaternion(0, 0, 0, 0);
            playerOneHUDPowerUpTwo.GetComponent<RectTransform>().localScale = new Vector3(0.9f, 0.9f, 1);
            playerOneHUDPowerUpTwo.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, -5, 0);


            //GETTING REFERENCES TO AVOID NULL CALLS
            playerTwoHUDPowerUpSlotOne = new GameObject("panelPowerSlotOne", typeof(Image));
            playerTwoHUDPowerUpOne = new GameObject("PowerUpOne", typeof(Image));
            playerTwoHUDPowerUpOne.transform.SetParent(playerTwoHUDPowerUpSlotOne.transform);
            playerTwoHUDPowerUpSlotTwo = new GameObject("panelPowerSlotTwo", typeof(Image));
            playerTwoHUDPowerUpTwo = new GameObject("PowerUpTwo", typeof(Image));
            playerTwoHUDPowerUpTwo.transform.SetParent(playerTwoHUDPowerUpSlotTwo.transform);
        }

    }
    // Update is called once per frame
    void Update()
    {
        //SetPanelOnTopCenter();
    }

    /// <summary>
    /// Sets CanvasplayerOne's HudPowerUpSlotOne centered on top
    /// </summary>
    void SetPanelOnTopCenter()
    {
        playerOneHUDPowerUpSlotOne = new GameObject("panelPowerUps", typeof(Image));
        playerOneHUDPowerUpSlotOne.transform.SetParent(canvasPlayerOne.transform);
        playerOneHUDPowerUpSlotOne.GetComponent<Image>().sprite = FindObjectOfType<PowerUpGlobalManager>().GetSpriteForPowerUpHUDPanel();
        playerOneHUDPowerUpSlotOne.GetComponent<Image>().type = Image.Type.Sliced;
        playerOneHUDPowerUpSlotOne.GetComponent<RectTransform>().localRotation = new Quaternion(0, 0, 0, 0);
        playerOneHUDPowerUpSlotOne.GetComponent<RectTransform>().localScale = new Vector3(0.8f, 0.8f, 1);
        playerOneHUDPowerUpSlotOne.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 1);
        playerOneHUDPowerUpSlotOne.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 1);
        playerOneHUDPowerUpSlotOne.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 1);
        playerOneHUDPowerUpSlotOne.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, -5, 0);
        playerOneHUDPowerUpSlotOne.GetComponent<Image>().fillCenter = false;
    }

    /// <summary>
    /// Sets CanvasplayerOne and Two centered on top
    /// </summary>
    void SetTwoPanels()
    {

    }

}
