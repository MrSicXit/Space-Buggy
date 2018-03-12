using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{

    /// <summary>
    /// returns the order number for this checkpoint. 0 in case its the first on the track, 1 if second...
    /// </summary>
    public int getOrder { get { return checkpointOrder; } }

    /// <summary>
    /// This number sets the order of the checkpoint on the course in order for the players to go through.
    /// 0 in case its the first on the track, 1 if second...
    /// </summary>
    [SerializeField]
    int checkpointOrder;

    /// <summary>
    /// Stores whether players need to go through this checkpoint next or not
    /// [true,true] meaning this checkpoint's next to go for both players
    /// [true, false] meaning player one needs to go through while player 2 does not
    /// </summary>
    public bool[] awaitingPlayers;// { get; private set; }

    /// <summary>
    /// Stores a collider from each player, used to calculate whether a player goes through the checkpoint
    /// </summary>
    [SerializeField]
    Collider[] collidersOfPlayers;

    /// <summary>
    /// LapManager object to update the state of players checkpoints and laps
    /// </summary>
    [SerializeField]
    LapManager lm;

    // Use this for initialization
    void Start()
    {
        //Setting the array to have a collider from each player
        collidersOfPlayers = new Collider[GameObject.FindGameObjectsWithTag("Player").Length];
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < collidersOfPlayers.Length; i++)
        {
            collidersOfPlayers[i] = players[i].GetComponentInChildren<Collider>();
        }

        //setting them on order according to player's order
        for (int i = 0; i < collidersOfPlayers.Length; i++)
        {
            for (int ii = 0; ii < collidersOfPlayers.Length - 1; ii++)
            {
                if (collidersOfPlayers[ii].gameObject.GetComponentInChildren<PlayerID>().getPlayerID == i)
                {
                    Collider tmpObject = collidersOfPlayers[i];
                    collidersOfPlayers[i] = collidersOfPlayers[ii];
                    collidersOfPlayers[ii] = tmpObject;
                }
            }
        }
        for (int i = 0; i < collidersOfPlayers.Length; i++)
        {
            //Debug.Log(collidersOfPlayers[i].gameObject.GetComponentInChildren<PlayerID>().getPlayerID);
        }
            lm = FindObjectOfType<LapManager>();//Initialization

        //Finding out amount of players in scene
        //Setting awaiting players as true for every player on checkpoint of order=0
        //Setting it to false for every player elsewhere
        awaitingPlayers = new bool[GameObject.FindGameObjectsWithTag("Player").Length];
        for (int i = 0; i < awaitingPlayers.Length; i++)
        {
            if (checkpointOrder == 0)
            {
                awaitingPlayers[i] = true;
            }
            else
            {
                awaitingPlayers[i] = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < collidersOfPlayers.Length; i++)
        {//Checking if the colliding object its a player this checkpoint waits for
            if (collidersOfPlayers[i] == other)
            {
                if (awaitingPlayers[i] == true)
                {//If so, updating checkpoint and lap status on the LapManager
                    lm.UpdateCheckpointStatus(i, checkpointOrder);
                }
            }
        }
    }
}
