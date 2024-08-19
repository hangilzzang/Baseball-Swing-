using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AluminumUI : MonoBehaviour
{
    public Button uiButton;
    public Image uiImage; // 이미지 컴포넌트 추가
    public Button iconButton;
    public Image iconImage;
    public Sprite checkUISprite; // 교체할 이미지 추가

    public Animator playerAnimator;
    public RuntimeAnimatorController newController;
    public PlayerController playerController;
    public AudioClip newBatHitClip;
    public GameObject infoUI;
    public Sprite errorUISprite;

    // Start is called before the first frame update
    void Start()
    {
        uiButton.onClick.AddListener(OnButtonClicked);
        EventManager.instance.watchedRewardAd += BatReward;
    }


    void OnDisable()
    {
        // 이벤트 구독 해제 (필수)
        EventManager.instance.watchedRewardAd -= BatReward;
    }

    // Update is called once per frame
    void OnButtonClicked()
    {
        if (GameManager.instance.gameState == GameManager.GameState.GameStart && !GameManager.instance.batADClicked)
            {
                if (!GameManager.instance.noAds)
                {
                    RewardADAluminum.instance.ShowAd();
                    uiImage.sprite = errorUISprite;
                }
                else
                {
                    EventManager.instance.TriggerWatchedRewardAd("bat");
                }
                uiImage.color = new Color(0.784f, 0.784f, 0.784f, 0.502f);
                iconImage.color = new Color(0.784f, 0.784f, 0.784f, 0.502f);
                GameManager.instance.batADClicked = true;
            }
        
        else if (GameManager.instance.gameState == GameManager.GameState.InfoUI)
        {
            GameManager.instance.gameState = GameManager.GameState.GameStart;
            infoUI.SetActive(false);
        }
    }

    public void BatReward(String type)
    {
        if (type == "bat")
        {
            uiImage.sprite = checkUISprite;


            playerAnimator.runtimeAnimatorController = newController; // 플레이어 애니메이션 알루미늄배트 애니메이션으로 변경
            GameManager.instance.aluminumBat = true; // 최종점수에 영향을 미친다
            playerController.batHitClip = newBatHitClip; // 배트 소리 변경 
        }
    }
}
