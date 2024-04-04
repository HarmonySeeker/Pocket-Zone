using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public float playerHealth;
    public int ammoNum;
    public Vector3 playerPosition;
    public SerializableDictionary<string, bool> itemsCollected;

    public GameData()
    {
        this.playerHealth = 100;
        this.ammoNum = 50;
        playerPosition = new Vector3 (4.0f, -1.0f, 0.0f);
        itemsCollected = new SerializableDictionary<string, bool>();
    }
}
