using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameView : MonoBehaviour
{

    public static GameView Instance;

    public Text text_crystals;
    public Text text_bossLevel;

    public GameOverView GameOverView;
    public ReviveView ReviveView;
    public LevelCompleteView LevelCompleteView;

    public Image image_whiteFlash;

    public Image image_bg;
    public Sprite[] sprite_bg;


    private void Awake()
    {
        Instance = this;
    }

    

    private void OnEnable()
    {

        WhiteFlash_Out();
        //if (AllLevels.Instance.allLevelsData[LevelManager.Instance.currentLevelIndex].isBossLevel)
        {
            //EnableBossLeveText();


        }
    }

  

    private void EnableBossLeveText()
    {
        StartCoroutine(WaitAndEnableBossLevelText());
    }

    private void DisableBossLeveText()
    {
        StartCoroutine(WaitAndDisableBossLevelText());
    }

    IEnumerator WaitAndEnableBossLevelText()
    {
        yield return new WaitForSeconds(1);

        text_bossLevel.transform.DOScale(Vector3.one, 1).SetEase(Ease.OutElastic).OnComplete(DisableBossLeveText);
    }

    IEnumerator WaitAndDisableBossLevelText()
    {
        yield return new WaitForSeconds(1);

        text_bossLevel.transform.DOScale(Vector3.zero, 1).SetEase(Ease.OutElastic);
    }



    // Start is called before the first frame update
    void Start()
    {
        UpdateCrystalsCollectedText();

        
    }

    public void SetBG()
    {
        image_bg.sprite = sprite_bg[LevelManager.Instance.currentLevelIndex / 5];
    }

    public void UpdateCrystalsCollectedText()
    {
        text_crystals.text = GameManager.Instance.totalCrystalsCollected.ToString();
        Expand();
    }

    private void Expand()
    {
        text_crystals.transform.DOScale(Vector3.one * 1.3f , 0.3f).OnComplete(ShrinkToNormal); 
    }
    private void ShrinkToNormal()
    {
        text_crystals.transform.DOScale(Vector3.one , 0.3f);
    }

    public void ShowGameOverView()
    {
        GameOverView.gameObject.transform.localScale = Vector3.zero;
        GameOverView.gameObject.SetActive(true);
        GameOverView.gameObject.transform.DOScale(Vector3.one, 0.4f);
    }

    public void HideGameOverView()
    {
        //GameOverView.gameObject.transform.localScale = Vector3.zero;
        GameOverView.gameObject.SetActive(false);
        //GameOverView.gameObject.transform.DOScale(Vector3.one, 0.4f);
    }

    public void ShowReviveView()
    {
        ReviveView.gameObject.transform.localScale = Vector3.zero;
        ReviveView.gameObject.SetActive(true);
        ReviveView.gameObject.transform.DOScale(Vector3.one, 0.4f);
    }

    public void HideReviveView()
    {
        //GameOverView.gameObject.transform.localScale = Vector3.zero;
        ReviveView.gameObject.SetActive(false);
        //GameOverView.gameObject.transform.DOScale(Vector3.one, 0.4f);
    }

    public void ShowLevelCompleteView()
    {
        LevelCompleteView.gameObject.transform.localScale = Vector3.zero;
        LevelCompleteView.gameObject.SetActive(true);
        LevelCompleteView.gameObject.transform.DOScale(Vector3.one, 0.4f);
    }

    public void HideLevelCompleteView()
    {
        //GameOverView.gameObject.transform.localScale = Vector3.zero;
        LevelCompleteView.gameObject.SetActive(false);
        //GameOverView.gameObject.transform.DOScale(Vector3.one, 0.4f);
    }

    public void WhiteFlash_In()
    {
        image_whiteFlash.DOFade(1, 0.5f).OnComplete(OnComplete_In);
    }

    private void OnComplete_In()
    {
        SceneManager.LoadScene("WorldSelection");
    }

    public void WhiteFlash_Out()
    {
        image_whiteFlash.DOFade(0, 0.5f);
    }

    public GameObject go_pauseView;

    public void OnClick_Pause()
    {
        go_pauseView.SetActive(true);
        Time.timeScale = 0;
    }

    public void OnClick_Resume()
    {
        go_pauseView.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnClick_Quit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Home");
    }
}
