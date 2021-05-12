using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;


public class PlayerSelectionView : MonoBehaviour
{

    public bool[] isPlayerPurchased;

    public Button button_previous;
    public Button button_next;

    public int currentIndex;
    
    private Tween tween_camera;

    public Button button_buy;
    public Button button_select;
    public Text text_damage;
    public Text text_cost;
    public int[] allCosts;
    public int[] allDamage;
    public Button button_back;

    private void OnEnable()
    {

        ApplyChanges();
        button_previous.GetComponent<RectTransform>().DOAnchorPosX(0, 0.3f);
        button_back.GetComponent<RectTransform>().DOAnchorPosX(0, 0.3f);
        button_next.GetComponent<RectTransform>().DOAnchorPosX(0, 0.3f);

        text_damage.GetComponent<RectTransform>().DOAnchorPosY(0, 0.3f);
    }


    public void OnClick_Previous()
    {
        DDOL.Instance.as_click.Play();
        if (currentIndex > 0)
        {
            currentIndex--;
            ApplyChanges();

        }
    }

    public void OnClick_Next()
    {
        DDOL.Instance.as_click.Play();
        if (currentIndex < 10)
        {
            currentIndex++;
            ApplyChanges();


        }
    }

    public void CameraTweenMotion()
    {
        if(tween_camera != null)
        {
            tween_camera.Kill();
        }

        tween_camera = Camera.main.transform.DOMoveX(currentIndex*5, 0.4f);
    }

    private void ApplyChanges()
    {
        CameraTweenMotion();

        if (isPlayerPurchased[currentIndex])
        {
            button_buy.gameObject.SetActive(false);
            button_select.gameObject.SetActive(true);
            DDOL.Instance.playerIndex = currentIndex;
        }
        else
        {
            button_buy.gameObject.SetActive(true);
            button_select.gameObject.SetActive(false);
            text_cost.text = allCosts[currentIndex].ToString();
        }

        text_damage.text = "Damage : " + allDamage[currentIndex].ToString();
    }

    public void OnClick_Back()
    {
        DDOL.Instance.as_click.Play();
        button_previous.GetComponent<RectTransform>().DOAnchorPosX(-500, 0.3f);
        button_back.GetComponent<RectTransform>().DOAnchorPosX(-500, 0.3f);
        button_next.GetComponent<RectTransform>().DOAnchorPosX(500, 0.3f);

        text_damage.GetComponent<RectTransform>().DOAnchorPosY(1000, 0.3f).OnComplete(OnComplete_BackButton);
    }

    private void OnComplete_BackButton()
    {
        gameObject.SetActive(false);
        HomeView.Instance.HomeViewAnimation_In();
    }

    public void OnClick_Buy()
    {
        DDOL.Instance.as_click.Play();
        if (DDOL.Instance.crystals > allCosts[currentIndex])
        {
            PurchasePlayer();
        }
    }

    private void PurchasePlayer()
    {
        DDOL.Instance.crystals -= allCosts[currentIndex];
        HomeView.Instance.SetText_CrystalsBalance();
        isPlayerPurchased[currentIndex] = true;
        DDOL.Instance.playerIndex = currentIndex;
        button_buy.gameObject.SetActive(false);
        button_select.gameObject.SetActive(true);
        
    }

    public void OnClick_Select()
    {
        DDOL.Instance.as_click.Play();
        gameObject.SetActive(false);
        HomeView.Instance.HomeViewAnimation_In();
    }

    private void Init()
    {
        currentIndex = DDOL.Instance.playerIndex;
        ApplyChanges();
    }
}
