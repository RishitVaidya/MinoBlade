using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverView : MonoBehaviour
{
    public Text txt_stageNumber;
    public Text txt_gemsEarned;

    public void OnEnable()
    {
        txt_stageNumber.text = "Level - " + (LevelManager.Instance.currentLevelIndex + 1).ToString();
        txt_gemsEarned.text = GameManager.Instance.totalCrystalsCollected.ToString();
        

    }

    public void OnClick_Collect()
    {
        GameView.Instance.WhiteFlash_In();
        DDOL.Instance.crystals += GameManager.Instance.totalCrystalsCollected;

        DataManager.Instance.SetIntData("Crystals", DDOL.Instance.crystals);

        float endTime = Time.time;

        float sessionDuration = endTime - GameManager.Instance.startTime;

        int totalSeconds = (int)sessionDuration;

        DDOL.Instance.allWorlds[DDOL.Instance.currentWorldIndex].stats.playTime += totalSeconds;

        DataManager.Instance.SetTotalPlaytimeForCurrentWorld();
    }
}
