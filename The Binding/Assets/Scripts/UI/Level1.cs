using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : MonoBehaviour
{
    [SerializeField]
    private LevelData levelData;

    private void Start()
    {
        levelData.currentLevel = 0;
    }
}
