using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level Data", menuName = "Game Data/Level Data")]
public class LevelData : ScriptableObject
{
    public enum WinCondition
    {
        Escape
    }

    [SerializeField]
    private Data[] levels = null;
    public Data[] Levels { get { return levels; } }

    [SerializeField]
    public int currentLevel;

    [SerializeField]
    private string defaultLevel = null;
    public string DefaultLevel { get { return defaultLevel; } }

    [SerializeField]
    private string mainMenu = null;
    public string MainMenu { get { return mainMenu; } }

    [System.Serializable]
    public class Data
    {
        public string levelName = "";
        public WinCondition winCondition = 0;
    }
}
