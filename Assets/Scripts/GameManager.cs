 using System;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class GameManager : MonoBehaviour 
{
    public static GameManager instance; 
    public enum GameState
    {
        PowerGauge, //파워게이지 입력받는중
        BatSwing, // 스윙 입력 받는중
        NotGettingAnyInput, // 어떠한 입력 안받는중
        GameStart, // Touch to start 입력받는중
        GameRestart, // Touch To Restart 입력받는중
        InfoUI
    }

    public enum SceneNames
    {
        GameScene,
        ResultScene,
        StartScene
    }




    public GameState gameState;
    public float powerValue;
    public float accuracyValue;
    public int scoreValue;
    public bool newRecord = false;
    public bool gameOver;

    
    float minThrowForce = 500f; // 공에 가해지는 힘 (AddForce)
    float maxThrowForce = 2000f; // 공에 가해지는 힘 (AddForce)
    float minThrowAnimationSpeed = 1f; // 던지기 애니메이션 진행속도 조절(배수)
    float maxThrowAnimationSpeed = 10f; // 던지기 애니메이션 진행속도 조절(배수)

    float minScore = 1f;
    float maxScore = 200f;
    float gradient = 30f;



    public float throwForce;
    public float throwAnimationSpeed;
    public float powerGaugeAnimationSpeed = 1f; // 파워게이지 애니메이션 진행속도 조절(배수)


    
    public float batSwingVolume = 0.3f;
    public float batHitVolume = 1f;
    public float throwBallClipVolume = 0.4f;
    public float grabBallVolume = 0.2f;
    public float TouchToStartVolume = 0.4f;
    public float crowdCheerVolume = 0.6f;



    public bool aluminumBat = false;
    public float aluminumBatAdvantage = 1.5f;

    public bool parachuteBall = false;
    public float parachuteBallAdvantage = 0.5f;

    public int gamePlayedTimes = 0; 

    public bool batADClicked = false;
    public bool ballADClicked = false;
    public bool noAds = false;

    public int IntersitialAdIndex = 8; // 겜끝나고 전면광고 노출 확률 조절 10은 10분의1

    void Awake() 
    {
        // PlayerPrefs.DeleteAll();
        // PlayerPrefs.Save(); // 변경사항 저장
        Application.targetFrameRate = 120; // fps 60으로 설정


        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // 광고 없는 버전임을 확인
        if (RewardADAluminum.instance == null && RewardADParachute.instance == null)
        {
            noAds = true;
        }
    }

    public void CalculateThrowForce() 
    {
        throwForce = Mathf.Lerp(minThrowForce, maxThrowForce, powerValue);
        throwAnimationSpeed = Mathf.Lerp(minThrowAnimationSpeed, maxThrowAnimationSpeed, powerValue);
    }

    public void CalculateScore()
    {   
        float value = Mathf.Pow(gradient, (powerValue * accuracyValue)); // 점수의 급등을 지수함수의 형태로 구현
        float normalizedValue = Normalize(value, 1, gradient); // 정규화
        float _scoreValue = (minScore + (maxScore - minScore) * normalizedValue); // 점수 범위안에서 점수 출력
        
        if (aluminumBat == true)
        {
            _scoreValue = _scoreValue * aluminumBatAdvantage;
        }
        
        scoreValue = (int)_scoreValue; // int로 타입변환
        
        UpdateBestScore(); // 최고점수 업데이트
    }

   // 주어진 값을  0과 1사이의 값으로 정규화
    float Normalize(float value, float min, float max)
    {
        return (value - min) / (max - min);
    }

    public void ChangeScene(SceneNames scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }


    void UpdateBestScore()
    {
        int bestScore = PlayerPrefs.GetInt("BestScore");
        if (scoreValue > bestScore)
        {
            PlayerPrefs.SetInt("BestScore", scoreValue);
            newRecord = true;
        }
    }
}