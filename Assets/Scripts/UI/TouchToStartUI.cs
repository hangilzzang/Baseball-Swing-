using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class TouchToStartUI : MonoBehaviour
{
    public GameObject PowerGauge;
    public AudioSource TouchToStartAudio; 
    public CanvasRenderer uiComponent;
    public GameObject RewardADUI;
    public GameObject help;
    public GameObject infoUI;

    void Awake()
    {
        if (!GameManager.instance.noAds)
            if (GameManager.instance.gamePlayedTimes == 0)
            {
                IntersitialAdStart.instance.ShowAd();
            }    
            else
            {
                IntersitialAdOver.instance.ShowAd();
            }
    }
    
    void Start()
    {
        GameManager.instance.gameState = GameManager.GameState.GameStart;
        GameManager.instance.parachuteBall = false;
        GameManager.instance.aluminumBat = false;
        GameManager.instance.batADClicked = false;
        GameManager.instance.ballADClicked = false;

        // 광고가 모두 준비되었으면 광고 UI활성화
        if (GameManager.instance.noAds)          
        {
            RewardADUI.SetActive(true);
        }       
        else if (RewardADAluminum.instance._rewardedAd != null && RewardADParachute.instance._rewardedAd != null)
        {
            RewardADUI.SetActive(true);
        }
    }

    public void GameStart() // 버튼 클릭 이벤트로 호출되는 메서드
    {
        if (GameManager.instance.gameState == GameManager.GameState.GameStart)
        {
            if (!GameManager.instance.noAds)
            // 다음 리워드 광고 호출
            {
                RewardADAluminum.instance.LoadAd();
                RewardADParachute.instance.LoadAd();
                IntersitialAdOver.instance.LoadAd();
            }

            TouchToStartAudio.volume = GameManager.instance.TouchToStartVolume;
            TouchToStartAudio.Play();
            
            RewardADUI.SetActive(false); // UI비활성화
            uiComponent.SetAlpha(0f);
            

            // 도움말 UI 추가
            if (GameManager.instance.gamePlayedTimes == 0)
            {
                help.SetActive(true);
            }

            GameManager.instance.gameState = GameManager.GameState.PowerGauge; // 게임상태 변경
            PowerGauge.SetActive(true);
            
            GameManager.instance.gamePlayedTimes += 1;
        }

        else if (GameManager.instance.gameState == GameManager.GameState.InfoUI)
        {
            GameManager.instance.gameState = GameManager.GameState.GameStart;
            infoUI.SetActive(false);
        }
            
    }
}
