using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    private void Awake()
    {
        Instance = this;
        //PlayerPrefs.DeleteAll();
        
    }
    private void OnEnable()
    {
        if (PlayerPrefs.HasKey("Crystals"))
        {

            DDOL.Instance.crystals = GetIntData("Crystals");
            DDOL.Instance.playerIndex = GetIntData("CurrentPlayerIndex");

            for(int i = 1; i <=10; i++)
            {
                string isPlayerBought = GetStringData("IsPlayerBought_" + i.ToString());

                if (isPlayerBought.Equals("False"))
                {
                    HomeView.Instance.PlayerSelectionView.isPlayerPurchased[i] = false;
                }
                else
                {
                    HomeView.Instance.PlayerSelectionView.isPlayerPurchased[i] = true;
                }
            }

            for(int i = 0; i < DDOL.Instance.allWorlds.Length; i++)
            {
                DDOL.Instance.allWorlds[i].currentLevelIndex = GetIntData("CurrentLevelInWorld_" + i.ToString());
                DDOL.Instance.allWorlds[i].stats.fails = GetIntData("TotalFailsInWorld_" + i.ToString());
                DDOL.Instance.allWorlds[i].stats.playTime = GetIntData("TotalPlaytimeInWorld_" + i.ToString());
            }

            
        }
        else
        {
            SetDataFirstTime();
            
        }

       
    }

    private void ApplySavedData()
    {
        HomeView.Instance.SetText_CrystalsBalance();
        
    }
  
    private void SetDataFirstTime()
    {

        SetIntData("Crystals", 0);
        SetIntData("CurrentPlayerIndex", 0);

        SetStringData("IsPlayerBought_1", "False");
        SetStringData("IsPlayerBought_2", "False");
        SetStringData("IsPlayerBought_3", "False");
        SetStringData("IsPlayerBought_4", "False");
        SetStringData("IsPlayerBought_5", "False");
        SetStringData("IsPlayerBought_6", "False");
        SetStringData("IsPlayerBought_7", "False");
        SetStringData("IsPlayerBought_8", "False");
        SetStringData("IsPlayerBought_9", "False");
        SetStringData("IsPlayerBought_10", "False");

        SetIntData("CurrentLevelInWorld_0", 0);
        SetIntData("CurrentLevelInWorld_1", 0);
        SetIntData("CurrentLevelInWorld_2", 0);
        SetIntData("CurrentLevelInWorld_3", 0);
        SetIntData("CurrentLevelInWorld_4", 0);

        SetIntData("TotalFailsInWorld_0", 0);
        SetIntData("TotalFailsInWorld_1", 0);
        SetIntData("TotalFailsInWorld_2", 0);
        SetIntData("TotalFailsInWorld_3", 0);
        SetIntData("TotalFailsInWorld_4", 0);

        SetIntData("TotalPlaytimeInWorld_0", 0);
        SetIntData("TotalPlaytimeInWorld_1", 0);
        SetIntData("TotalPlaytimeInWorld_2", 0);
        SetIntData("TotalPlaytimeInWorld_3", 0);
        SetIntData("TotalPlaytimeInWorld_4", 0);



       
    }

    public void SetIntData(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
    }

    public void SetStringData(string key , string value)
    {
        PlayerPrefs.SetString(key, value);
    }

    public int GetIntData(string key)
    {
        return PlayerPrefs.GetInt(key);
    }

    public string GetStringData(string key)
    {
        return PlayerPrefs.GetString(key);
    }

    public void SetIsPlayerBought(int playerIndex , string isBought)
    {
        SetStringData("IsPlayerBought_" + playerIndex.ToString(), isBought);
    }

    public void SetCurrentLevelIndexForWorld()
    {
        SetIntData("CurrentLevelInWorld_" + DDOL.Instance.currentWorldIndex.ToString(), DDOL.Instance.allWorlds[DDOL.Instance.currentWorldIndex].currentLevelIndex);
    }

    public void SetTotalFailsForCurrentWorld()
    {
        SetIntData("TotalFailsInWorld_" + DDOL.Instance.currentWorldIndex.ToString(), DDOL.Instance.allWorlds[DDOL.Instance.currentWorldIndex].stats.fails);
    }

    public void SetTotalPlaytimeForCurrentWorld()
    {
        SetIntData("TotalPlaytimeInWorld_" + DDOL.Instance.currentWorldIndex.ToString(), DDOL.Instance.allWorlds[DDOL.Instance.currentWorldIndex].stats.playTime);
    }
}
