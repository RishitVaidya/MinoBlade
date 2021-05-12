using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public GameObject entry;
    public int currentWorldIndex = 0;
    public int currentLevelIndex = 0;
    public Level currentLevel;

    public List<Base_Enemy> list_allEnemies;

    public GameObject prefab_wall;

    void Awake()
    {
        Instance = this;
        //if (PlayerPrefs.HasKey("Level"))
        //{
        //    activeLevelNumber = PlayerPrefs.GetInt("Level");
        //}
        //else
        //{
        //    activeLevelNumber = 0;
        //    PlayerPrefs.SetInt("Level",0);
        //}

        currentWorldIndex = DDOL.Instance.currentWorldIndex;
        currentLevelIndex = DDOL.Instance.allWorlds[currentWorldIndex].currentLevelIndex;
        currentLevel = DDOL.Instance.allWorlds[currentWorldIndex].allLevels[currentLevelIndex];

        
        
    }

    private void Start()
    {
        GameView.Instance.SetBG();
        StartNewLevel();

        GameView.Instance.image_bg.sprite = GameView.Instance.sprite_bg[currentWorldIndex];
    }

    public void StartNewLevel()
    {
        if(currentLevelIndex == 9)
        {
            CommenceBossLevel();
        }
        else
        {
            CommenceLevel();
        }
    }

    public void CommenceLevel()
    {

        SpawnEnemies(currentLevel);

        RandomlyPlaceWalls();
    }

 
    private void ArrangeWallsRandomly()
    {

        for (int i = 0; i < currentLevel.numberOfWalls; i++)
        {
            Debug.Log(1);
            Vector3 birthposition = Vector3.zero;

            birthposition.x = Random.Range(GameManager.Instance.boundaries[2].position.x + 1, GameManager.Instance.boundaries[3].position.x - 1);
            birthposition.z = Random.Range(GameManager.Instance.boundaries[0].position.z + 1, GameManager.Instance.boundaries[1].position.z -1);



            GameObject newWall = Instantiate(prefab_wall, birthposition, prefab_wall.transform.rotation);

            Vector3 rotation = new Vector3(newWall.transform.localEulerAngles.x, Random.Range(0, 180), newWall.transform.localEulerAngles.z);
        }
    }

    private void RandomlyPlaceWalls()
    {
        List<Transform> list_placedWalls = new List<Transform>();



        Vector3 birthPosition = GetRandomBirthPositionOfWall();
        GameObject newWall = Instantiate(prefab_wall, birthPosition, prefab_wall.transform.rotation);
        list_placedWalls.Add(newWall.transform);

        int wallsRemaining = currentLevel.numberOfWalls;

        while(wallsRemaining > 0)
        {
            birthPosition = GetRandomBirthPositionOfWall();
            bool isValidPosition = true;

            for(int i = 0; i < list_placedWalls.Count; i++)
            {
                if(Vector3.Distance(list_placedWalls[i].position , birthPosition) < 2)
                {
                    isValidPosition = false;
                    break;
                }
            }

            if (isValidPosition)
            {
                newWall = Instantiate(prefab_wall, birthPosition, prefab_wall.transform.rotation);
                list_placedWalls.Add(newWall.transform);
                wallsRemaining--;
            }
        }


    }

    private Vector3 GetRandomBirthPositionOfWall()
    {
        Vector3 birthposition = Vector3.zero;

        birthposition.x = Random.Range(GameManager.Instance.boundaries[2].position.x + 1.5f, GameManager.Instance.boundaries[3].position.x - 1.5f);
        birthposition.z = Random.Range(GameManager.Instance.boundaries[0].position.z + 1.5f, GameManager.Instance.boundaries[1].position.z - 1.5f);

        return birthposition;
    }

    private void CommenceBossLevel()
    {
        SpawnBoss();
    }
    

    //private void CreatePoison()
    //{
    //    Vector3 spawnPosition = Vector3.zero;
    //    spawnPosition.x = Random.Range(-1.7f, 1.7f);
    //    spawnPosition.y = 0;
    //    spawnPosition.z = Random.Range(-4f, 4f);
    //    Instantiate(prefab_poison, spawnPosition, prefab_poison.transform.rotation);
    //}

    //private void CreateBomb()
    //{
    //    Vector3 spawnPosition = Vector3.zero;
    //    spawnPosition.x = Random.Range(-1.7f, 1.7f);
    //    spawnPosition.y = 0;
    //    spawnPosition.z = Random.Range(-4f, 4f);
    //    Instantiate(prefab_bomb, spawnPosition, prefab_poison.transform.rotation);

    //}

    private void SpawnEnemies(Level currentLevel)
    {
        StartCoroutine(SpawnEnemies_(currentLevel));
    }

    IEnumerator SpawnEnemies_(Level currentLevel)
    {
        int totalEnemiesToBeSpawned = currentLevel.enemyCount;

        while (totalEnemiesToBeSpawned > 0)
        {
            yield return new WaitForSeconds(2);


            Base_Enemy newEnemySpawned = null;

            int random = Random.Range(0, 2);
            

            if (random == 0)
            {
                newEnemySpawned = Instantiate(DDOL.Instance.allWorlds[currentWorldIndex].prefab_enemy_1).GetComponent<Base_Enemy>();
            }

            else
            {
                newEnemySpawned = Instantiate(DDOL.Instance.allWorlds[currentWorldIndex].prefab_enemy_2).GetComponent<Base_Enemy>();
            }

            Vector3 position = newEnemySpawned.transform.position;
            position.y = Random.Range(-0.1f, 0.1f);

            int randomSideIndex = Random.Range(0, 2);

            if(randomSideIndex == 0)
            {
                position.x = Random.Range(-3, -5);
            }
            else
            {
                position.x = Random.Range(3, 5);
            }

            randomSideIndex = Random.Range(0, 2);

            if (randomSideIndex == 0)
            {
                position.z = Random.Range(-6, -8);
            }
            else
            {
                position.z = Random.Range(6, 8);
            }

            newEnemySpawned.transform.position = position;

            totalEnemiesToBeSpawned--;
            list_allEnemies.Add(newEnemySpawned);
        }
    }

    private void SpawnBoss()
    {     
        GameObject bossSpawned = Instantiate(DDOL.Instance.allWorlds[currentWorldIndex].prefab_boss);
    }
}


