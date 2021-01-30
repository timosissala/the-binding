using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameDataManager : MonoBehaviour
{
    [SerializeField]
    private GameData gameData;

    [SerializeField]
    private Tilemap groundMap;

    [SerializeField]
    private Tilemap terrainMap;

    [SerializeField]
    private Tilemap objectMap;

    private void Awake()
    {
        gameData.InitialiseTilemapDatas(groundMap, terrainMap, objectMap);
    }
}
