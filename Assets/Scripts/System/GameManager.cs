using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Vector2 minimumCoords;
    [SerializeField] private Vector2 maximumCoords;

    [SerializeField] private GameObject monsterSpawn;
    [SerializeField] private int spawnCount;

    private void Awake()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            float x = Random.Range(minimumCoords.x, maximumCoords.x);
            float y = Random.Range(minimumCoords.y, maximumCoords.y);
            Vector3 pos = new Vector3(x, y, 0.0f);
            Instantiate(monsterSpawn, pos, Quaternion.identity);
        }
    }
}
