using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameResult : MonoBehaviour
{
    public Text currentScoreText;
    public Text bestScoreText;
    public AudioSource gameResultAudio; 

    public GameObject highScoreUI;
    public GameObject newRecordUI;

    void Start()
    {   
        currentScoreText.text = GameManager.instance.scoreValue + "M";

        if (GameManager.instance.newRecord == true)
        {
            highScoreUI.SetActive(false); // 최고기록 ui 비활성화
            gameResultAudio.volume = GameManager.instance.crowdCheerVolume;
            gameResultAudio.Play();
            GameManager.instance.newRecord = false;
        }
        else
        {
            newRecordUI.SetActive(false); // 신기록 ui 비활성화
            int bestScore = PlayerPrefs.GetInt("BestScore");
            bestScoreText.text = bestScore + "M";
        }

    }
}
