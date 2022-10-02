using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public SerializableDictionary<string, bool> powerUpCollected;

    // the values defined in this constructor will be the default values
    // the game starts with when there's no data to load
    public GameData() 
    {
        powerUpCollected = new SerializableDictionary<string, bool>();
    }
}
