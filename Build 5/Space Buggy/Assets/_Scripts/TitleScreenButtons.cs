using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreenButtons : MonoBehaviour {

    [SerializeField]
    Button singlePlayer;
    [SerializeField]
    Button twoPlayersVS;
    [SerializeField]
    Button credit;
    [SerializeField]
    Button quit;

	// Use this for initialization
	void Start () {
        singlePlayer.onClick.AddListener(SinglePlayer);
        twoPlayersVS.onClick.AddListener(TwoPlayer);
        credit.onClick.AddListener(Credits);
        quit.onClick.AddListener(QuitButton);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    

    void SinglePlayer()
    {
        SceneManager.LoadScene("Map1");
    }

    void TwoPlayer()
    {
        SceneManager.LoadScene("Map1");
    }

    void Credits()
    {

    }

    void QuitButton()
    {
        Application.Quit();
    }
}
