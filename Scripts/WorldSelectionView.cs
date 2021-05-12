using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class WorldSelectionView : MonoBehaviour
{
    public Transform[] DisplayEnemies;
    public int currentEnemyIndex;

    public Button button_previous;
    public Button button_next;

    public Transform transform_camera;

    private Tween tween_cameraSwipe;

    public Image image_whiteFlash;

    public Image image_backGround;
    public Sprite[] sprites_worldBg;

    public Text text_progressValue;
    public Text text_failsValue;
    public Text text_playTimeValue;
    public Text text_dashesInARowValue;

    public Text text_levelProgress;
    public Image[] image_allLevelDots;
    public Text text_crystals;

    private void Start()
    {
        WhiteFlash_Out();
        //Tween tween = image_whiteFlash.DOFade(0, 0.5f);
        CalculateStats();
    }


    public void OnClick_Next()
    {
        DDOL.Instance.as_click.Play();
        currentEnemyIndex++;
        CheckInteractability();

        ChangeWorld();

    }

    public void OnClick_Previous()
    {
        DDOL.Instance.as_click.Play();
        currentEnemyIndex--;
        CheckInteractability();

        ChangeWorld();

    }


    private void CheckInteractability()
    {
        if(currentEnemyIndex == 0)
        {
            button_previous.interactable = false;
        }

        else if(currentEnemyIndex == 4)
        {
            button_next.interactable = false;
        }
        else
        {
            button_previous.interactable = true;
            button_next.interactable = true;
        }
    }

    public void OnClick_Continue()
    {
        DDOL.Instance.as_click.Play();
        DDOL.Instance.currentWorldIndex = currentEnemyIndex;
        WhiteFlash_In();

    }

    private void CameraSwipe()
    {

        tween_cameraSwipe.Kill();
        tween_cameraSwipe = transform_camera.DOMoveX(DisplayEnemies[currentEnemyIndex].position.x, 0.4f);
    }

    public void WhiteFlash_In()
    {
        image_whiteFlash.DOFade(1, 0.5f).OnComplete(OnComplete_In);
    }

    private void OnComplete_In()
    {
        SceneManager.LoadScene("Game");
    }

    public void WhiteFlash_Out()
    {
        image_whiteFlash.DOFade(0, 0.5f);
        ShowNewEnemy();
        //CalculateStats();
    }

    private void ChangeWorld()
    {
        image_whiteFlash.DOFade(1, 0.4f).OnComplete(OnCompleteFlasIn);
    }

    private void OnCompleteFlasIn()
    {
        ShowNewEnemy();
        image_whiteFlash.DOFade(0, 0.4f);

        CalculateStats();
    }

    private void ShowNewEnemy()
    {
        for(int i = 0; i < DisplayEnemies.Length; i++)
        {
            DisplayEnemies[i].gameObject.SetActive(false);
        }
        DisplayEnemies[currentEnemyIndex].gameObject.SetActive(true);
        image_backGround.sprite = sprites_worldBg[currentEnemyIndex];
    }

  

    private void CalculateStats()
    {

        text_levelProgress.text = DDOL.Instance.allWorlds[currentEnemyIndex].currentLevelIndex.ToString() + "/" + DDOL.Instance.allWorlds[currentEnemyIndex].allLevels.Length.ToString();

        for(int i = 0; i < 10; i++)
        {
            image_allLevelDots[i].color = Color.grey;
        }

        for(int i = 0; i < DDOL.Instance.allWorlds[currentEnemyIndex].currentLevelIndex; i++)
        {
            image_allLevelDots[i].color = Color.white;
        }

        //int progress = DDOL.Instance.allWorlds[currentEnemyIndex].stats.progress;
        int fails = DDOL.Instance.allWorlds[currentEnemyIndex].stats.fails;
        int playTime = DDOL.Instance.allWorlds[currentEnemyIndex].stats.playTime;
        int dashesInARow = DDOL.Instance.allWorlds[currentEnemyIndex].stats.dashesInARow;

        text_progressValue.text = (DDOL.Instance.allWorlds[currentEnemyIndex].currentLevelIndex * 10).ToString() + "%";

        text_failsValue.text = fails.ToString();

        SetText_PlayTime(playTime);

        text_dashesInARowValue.text = dashesInARow.ToString();


        text_crystals.text = DDOL.Instance.crystals.ToString();

    }

    private void SetText_PlayTime(int playTime)
    {
        int minutes = playTime / 60;
        int seconds = playTime % 60;

        text_playTimeValue.text = minutes.ToString() + "m " + seconds.ToString() + "s"; 
    }

    public void OnClick_Back()
    {
        DDOL.Instance.as_click.Play();
        SceneManager.LoadScene("Home");
    }

}
