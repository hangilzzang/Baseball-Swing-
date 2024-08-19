using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UmbrellaUI : MonoBehaviour
{
    public Button uiButton;
    public Button iconButton;
    public Image uiImage; 
    public Image iconImage;
    public Sprite checkUISprite; // 교체할 이미지 추가
    public GameObject infoUI;

    public Sprite errorUISprite;
    

    void Start()
    {
        uiButton.onClick.AddListener(OnButtonClicked);
        EventManager.instance.watchedRewardAd += BallReward;
    }

    void OnDisable()
    {
        // 이벤트 구독 해제 (필수)
        EventManager.instance.watchedRewardAd -= BallReward;
    }

    void OnButtonClicked()
    {
        if (GameManager.instance.gameState == GameManager.GameState.GameStart && !GameManager.instance.ballADClicked)
            {
                if (!GameManager.instance.noAds)
                {
                    RewardADParachute.instance.ShowAd();
                    uiImage.sprite = errorUISprite;
                }
                else
                {
                    EventManager.instance.TriggerWatchedRewardAd("ball");
                }
                uiImage.color = new Color(0.784f, 0.784f, 0.784f, 0.502f);
                iconImage.color = new Color(0.784f, 0.784f, 0.784f, 0.502f);        
                GameManager.instance.ballADClicked = true;
            }
        else if (GameManager.instance.gameState == GameManager.GameState.InfoUI)
        {
            GameManager.instance.gameState = GameManager.GameState.GameStart;
            infoUI.SetActive(false);
        }
    }

    public void BallReward(String type)
    {
        if (type == "ball")
        {
            uiImage.sprite = checkUISprite;   
            GameManager.instance.parachuteBall = true;
        }
    }
}
