using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllLevels : MonoBehaviour
{
    public static AllLevels Instance;

   public List<Level> allLevelsData = new List<Level>();

    private void Awake()
    {
        Instance = this;
    }
}

//[System.Serializable]
//public class Level
//{
//    public Level() { }

//    public string levelNumber;
//    public int enemyCount;
//    public int bomb;
//    public int poison;
//    public bool isBonusLevel;
//    public GameObject LevelMap;
//    public Vector2[] poision_Positions;
//    public Vector2[] bomb_Positions;
//    public float HitPointsMultiplier;
//    public bool isBossLevel;
//    public GameObject[] prefab_boss;
//    public int bossCount;
//    public int bossKilled;
//    public int numberOfWalls;
//}
