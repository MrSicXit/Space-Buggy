using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpGlobalManager : MonoBehaviour
{

    [SerializeField]
    [Tooltip("power-up frame image for the HUD. Background from Unity Builtin assets being the one used so far.")]
    Sprite spritePowerUpSlotFrame;
    [SerializeField]
    [Tooltip("Drag and drop the sprites for each power-up, in the same order than in PowerUp.cs")]
    Sprite[] powerUpIconsArray;
    [SerializeField]
    [Tooltip("Sprite for empty slot. Leaving this blank should work fine.")]
    Sprite defaultPowerUpSprite;
    [SerializeField]
    [Tooltip("Duration for each power up")]
    float[] powerUpDurationsArray;

    /// <summary>
    /// Will return the sprite stored at PowerUpSlotFrame.
    /// The sprite needs to be provided in the Unity Editor
    /// </summary>
    /// <returns>The sprite for the frame of a power-Up in thr HUD</returns>
    public Sprite GetSpriteForPowerUpHUDPanel()
    {
        return spritePowerUpSlotFrame;
    }

    /// <summary>
    /// Will return the sprite corresponding to the index given. If the value given its outside powerUpIconsArray bounds,
    /// it will return defaultPowerUpSprite
    /// </summary>
    /// <param name="index">the index for the powerUp image to be retrieved. </param>
    /// <returns></returns>
    public Sprite GetPowerUpSpriteWithIndex(int index)
    {   //If index between the bounds of the array, return the corresponding image
        if (!((index < 0) || (index >= powerUpIconsArray.Length)))
            return powerUpIconsArray[index];
        else//otherwise, return the blank image
            return defaultPowerUpSprite;
    }

    /// <summary>
    /// Returns duration of the power up of index
    /// </summary>
    /// <param name="index"></param>
    public float GetPowerUpDuration(int index)
    {
        if (!((index < 0) || (index >= powerUpDurationsArray.Length)))
            return powerUpDurationsArray[index];
        else
            return 0;
    }
}
