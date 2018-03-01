using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapManager : MonoBehaviour {

    /// <summary>
    /// Number of laps in order to win
    /// </summary>
    [SerializeField]
    int lapsToWin = 3;

    /// <summary>
    /// Each player number of completed laps
    /// </summary>
    [SerializeField]
    int[] lapsDone;

    /// <summary>
    /// Array with the track checkpoint used to update what players are expected to pass through each
    /// </summary>
    [SerializeField]
    GameObject[] checkpoints;

	// Use this for initialization
	void Start () {
        //Initializing checkpoint array and setting laps to 0 for each player
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
        lapsDone = new int[players.Length];
        for (int i = 0; i < players.Length; i++)
        {
            lapsDone[i] = 0;
        }
        //checkpoints sort by specified order on each object
        for (int i = 0; i < checkpoints.Length; i++)
        {
            for (int ii = 0; ii < checkpoints.Length-1; ii++)
            {
                if (checkpoints[ii].GetComponent<CheckpointManager>().getOrder == i)
                {
                    GameObject tmpObject = checkpoints[i];
                    checkpoints[i] = checkpoints[ii];
                    checkpoints[ii] = tmpObject;
                }
            }
        }
    }
	
    /// <summary>
    /// Tells the specify checkpoint not to wait for the specified player any longer, setting the next one to do so.
    /// If the checkpoint its the last of the track, add a lap to the specified player
    /// </summary>
    /// <param name="playerIndex">Index of the player succesfully passing the checkpoint</param>
    /// <param name="checkpointIndex">Index of the checkpoint</param>
	public void UpdateCheckpointStatus(int playerIndex, int checkpointIndex)
    {
        if (checkpointIndex==checkpoints.Length-1)//If its the last checkpoint of the track
        {
            checkpoints[checkpointIndex].GetComponentInChildren<CheckpointManager>().awaitingPlayers[playerIndex] = false;
            lapsDone[playerIndex]++;
            checkpoints[0].GetComponentInChildren<CheckpointManager>().awaitingPlayers[playerIndex] = true;

            if (lapsDone[playerIndex]==lapsToWin)//Place to add race winning code
            {
                //SOMEONE WON THE RACE
            }
        }
        else//otherwise
        {
            checkpoints[checkpointIndex].GetComponentInChildren<CheckpointManager>().awaitingPlayers[playerIndex] = false;
            checkpoints[checkpointIndex+1].GetComponentInChildren<CheckpointManager>().awaitingPlayers[playerIndex] = true;
        }

    }

}
