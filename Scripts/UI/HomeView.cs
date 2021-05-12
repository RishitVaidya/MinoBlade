using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class HomeView : MonoBehaviour
{

    public static HomeView Instance;

    public Image image_whiteFlash;
    public GameObject DisplayPlayers;

    public GameObject go_SettingsView;

    public Text text_crystalsBalance;

    private void OnEnable()
    {
        
    }

    private void Awake()
    {
        Instance = this;
        WhiteFlash_Out();
       
    }

    private void Start()
    {
        HomeViewAnimation_In();
        SetText_CrystalsBalance();
    }

    public void OnClick_Play()
    {
        DDOL.Instance.as_click.Play();
        WhiteFlash_In();
       
    }

    public void OnClick_Settings()
    {
        DDOL.Instance.as_click.Play();
        go_SettingsView.transform.localScale = Vector3.zero;
        go_SettingsView.SetActive(true);
        go_SettingsView.transform.DOScale(Vector3.one, 0.4f);
    }

    public void OnClick_CloseSettings()
    {
        DDOL.Instance.as_click.Play();
        go_SettingsView.transform.DOScale(Vector3.zero, 0.4f).OnComplete(OnComplete_SettingsClose);
    }

    private void OnComplete_SettingsClose()
    {
        go_SettingsView.transform.localScale = Vector3.zero;
        go_SettingsView.SetActive(false);

    }

    public Toggle toggle_sound;
    public Toggle toggle_vibration;



    public void OnToggle_Sound()
    {
        if (toggle_sound.isOn)
        {
            AudioListener.volume = 1;
        }
        else
        {
            AudioListener.volume = 0;
        }
        
    }

    public void OnToggle_Vibration()
    {
        if (toggle_vibration.isOn)
        {
            DDOL.Instance.isVibrate = true;
        }
        else
        {
            DDOL.Instance.isVibrate = false;
        }
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

    public Text text_heading;
    public RectTransform rt_buttons;

    public PlayerSelectionView PlayerSelectionView;

    public void OnClick_PlayerSelection()
    {
        DDOL.Instance.as_click.Play();
        text_heading.rectTransform.DOAnchorPosY(1000, 0.3f);
        rt_buttons.DOAnchorPosY(-1000, 0.3f).OnComplete(OnComplete_PlayerSekectionButton);
    }

    private void OnComplete_PlayerSekectionButton()
    {
        PlayerSelectionView.gameObject.SetActive(true);
    }

    public void HomeViewAnimation_In()
    {
        text_heading.rectTransform.DOAnchorPosY(0, 0.3f);
        rt_buttons.DOAnchorPosY(0, 0.3f);

        Camera.main.transform.DOMoveX(DDOL.Instance.playerIndex * 5, 0.4f);
        PlayerSelectionView.currentIndex = DDOL.Instance.playerIndex;
    }

    public void SetText_CrystalsBalance()
    {
        text_crystalsBalance.text = DDOL.Instance.crystals.ToString();
    }
}
