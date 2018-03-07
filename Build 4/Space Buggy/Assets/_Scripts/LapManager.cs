using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapManager : MonoBehaviour
{

    /// <summary>
    /// Number of laps in order to win
    /// </summary>
    [SerializeField]
    int lapsToWin = 3;

    /// <summary>
    ///The order of the last chekpoint 
    /// </summary>
    int lastCheckpointIndex;

    /// <summary>
    /// Each player number of completed laps
    /// </summary>
    [SerializeField]
    int[] lapsDone;

    /// <summary>
    /// UsedForInternalCalculation
    /// </summary>
    [SerializeField]
    int[] checkPointsOfsameOrder;

    /// <summary>
    /// Array with the track checkpoint used to update what players are expected to pass through each
    /// </summary>
    [SerializeField]
    GameObject[] checkpoints;

    // Use this for initialization
    void Start()
    {
        //Initializing checkpoint array and setting laps to 0 for each player
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
        HighestCheckpointOrderValueInArray();//sets value for last checkpoint index
        checkPointsOfsameOrder = new int[checkpoints.Length];
        InitializeCheckpointOrderArray();//Initialize for later use

        lapsDone = new int[players.Length];
        for (int i = 0; i < players.Length; i++)
        {
            lapsDone[i] = 0;
        }


        int pos = 0;
        int val = 0;
        GameObject temp;
        while (pos < checkpoints.Length)
        {
            for (int i = 0; i < checkpoints.Length; i++)
            {
                if (checkpoints[i].GetComponent<CheckpointManager>().getOrder == val)
                {
                    temp = checkpoints[pos];
                    checkpoints[pos] = checkpoints[i];
                    checkpoints[i] = temp;
                    pos++;
                }
            }
            val++;
        }

        //OLD SORT
        /*
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
        */
    }

    /// <summary>
    /// Tells the specify checkpoint not to wait for the specified player any longer, setting the next one to do so.
    /// If the checkpoint its the last of the track, add a lap to the specified player
    /// </summary>
    /// <param name="playerIndex">Index of the player succesfully passing the checkpoint</param>
    /// <param name="checkpointIndex">Index of the checkpoint</param>
    public void UpdateCheckpointStatus(int playerIndex, int checkpointIndex)
    {
        if (checkpointIndex == lastCheckpointIndex)//If its the last checkpoint of the track
        {

            InitializeCheckpointOrderArray();
            GetIndexOfEveryCheckpointOfIDEqualTo(checkpointIndex);
            for (int i = 0; i < checkPointsOfsameOrder.Length; i++)
            {
                if (checkPointsOfsameOrder[i] != -1)
                {
                    checkpoints[checkPointsOfsameOrder[i]].GetComponentInChildren<CheckpointManager>().awaitingPlayers[playerIndex] = false;
                }
            }
            lapsDone[playerIndex]++;
            InitializeCheckpointOrderArray();
            GetIndexOfEveryCheckpointOfIDEqualTo(0);
            for (int i = 0; i < checkPointsOfsameOrder.Length; i++)
            {
                if (checkPointsOfsameOrder[i] != -1)
                {
                    checkpoints[checkPointsOfsameOrder[i]].GetComponentInChildren<CheckpointManager>().awaitingPlayers[playerIndex] = true;
                }
            }
            if (lapsDone[playerIndex] == lapsToWin)//Place to add race winning code
            {
                //SOMEONE WON THE RACE
            }
            //checkpoints[checkpointIndex].GetComponentInChildren<CheckpointManager>().awaitingPlayers[playerIndex] = false;
            //lapsDone[playerIndex]++;
            //checkpoints[0].GetComponentInChildren<CheckpointManager>().awaitingPlayers[playerIndex] = true;
            //if (lapsDone[playerIndex]==lapsToWin)//Place to add race winning code
            //{
            //SOMEONE WON THE RACE
            //}
        }
        else//otherwise
        {
            InitializeCheckpointOrderArray();
            GetIndexOfEveryCheckpointOfIDEqualTo(checkpointIndex);
            for (int i = 0; i < checkPointsOfsameOrder.Length; i++)
            {
                if (checkPointsOfsameOrder[i] != -1)
                {
                    checkpoints[checkPointsOfsameOrder[i]].GetComponentInChildren<CheckpointManager>().awaitingPlayers[playerIndex] = false;
                }
            }
            InitializeCheckpointOrderArray();
            GetIndexOfEveryCheckpointOfIDEqualTo(checkpointIndex + 1);
            for (int i = 0; i < checkPointsOfsameOrder.Length; i++)
            {
                if (checkPointsOfsameOrder[i] != -1)
                {
                    checkpoints[checkPointsOfsameOrder[i]].GetComponentInChildren<CheckpointManager>().awaitingPlayers[playerIndex] = true;
                }
            }
            //checkpoints[checkpointIndex].GetComponentInChildren<CheckpointManager>().awaitingPlayers[playerIndex] = false;
            //checkpoints[checkpointIndex+1].GetComponentInChildren<CheckpointManager>().awaitingPlayers[playerIndex] = true;
        }

    }

    void GetIndexOfEveryCheckpointOfIDEqualTo(int index)
    {
        int pos = 0;
        for (int i = 0; i < checkpoints.Length; i++)
        {
            if (checkpoints[i].GetComponentInChildren<CheckpointManager>().getOrder == index)
            {
                checkPointsOfsameOrder[pos] = i;
                pos++;
            }
        }
    }

    void InitializeCheckpointOrderArray()
    {
        for (int i = 0; i < checkPointsOfsameOrder.Length; i++)
        {
            checkPointsOfsameOrder[i] = -1;//-1 means ignore
        }
    }

    void HighestCheckpointOrderValueInArray()
    {
        for (int i = 0; i < checkpoints.Length; i++)
        {
            if (checkpoints[i].GetComponentInChildren<CheckpointManager>().getOrder > lastCheckpointIndex)
            {
                lastCheckpointIndex = checkpoints[i].GetComponentInChildren<CheckpointManager>().getOrder;
            }
        }
    }

}
