using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerID : MonoBehaviour
{

    /// <summary>
    /// Number indicating player order. 0 for player 1, 1 for player 2 and so on
    /// </summary>
    [SerializeField]
    int playerID;

    /// <summary>
    /// Returns this player order. 0 for player 1, 1 for player 2 and so on
    /// </summary>
    public int getPlayerID { get { return playerID; } }
}
