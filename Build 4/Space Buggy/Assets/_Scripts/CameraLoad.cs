using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class to setup camera positions on up to 4 players. Each player needs to have a camera attached
public class CameraLoad : MonoBehaviour {

	// Use this for initialization
	void Start () {
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
        if (players.Length==2)
        {
            players[0].GetComponentInChildren<Camera>().rect = new Rect(new Vector2(0, 0), new Vector2(0.5f,1));
            players[1].GetComponentInChildren<Camera>().rect = new Rect(new Vector2(0.5f, 0), new Vector2(0.5f, 1));
        }
        else
        {   //3 players setup P1 on top, P2 bottom left, P3 bottom right
            if(players.Length==3)
            {
                players[0].GetComponentInChildren<Camera>().rect = new Rect(new Vector2(0, 0.5f), new Vector2(1, 0.5f));
                players[1].GetComponentInChildren<Camera>().rect = new Rect(new Vector2(0, 0), new Vector2(0.5f, 0.5f));
                players[2].GetComponentInChildren<Camera>().rect = new Rect(new Vector2(0.5f, 0), new Vector2(0.5f, 0.5f));
            }
            else
            {
                if(players.Length!=1)//4 players or more P1 top-left, P2 top-right,P3 bottom-left, P4 bottom-right
                {
                    players[0].GetComponentInChildren<Camera>().rect = new Rect(new Vector2(0, 0.5f), new Vector2(0.5f, 0.5f));
                    players[1].GetComponentInChildren<Camera>().rect = new Rect(new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f));
                    players[2].GetComponentInChildren<Camera>().rect = new Rect(new Vector2(0, 0), new Vector2(0.5f, 0.5f));
                    players[3].GetComponentInChildren<Camera>().rect = new Rect(new Vector2(0.5f, 0), new Vector2(0.5f, 0.5f));
                }
            }
        }
	}
}
