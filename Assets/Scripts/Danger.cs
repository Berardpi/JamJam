using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PowerUpManager;

public class Danger : MonoBehaviour
{
    [SerializeField]
    PowerUp gainedPowerUp;

    public PowerUp GetGainedPowerUp()
    {
        return gainedPowerUp;
    }
}
