using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames.BasicApi;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class LeaderBoard : MonoBehaviour
{
    public static LeaderBoard instance; 
    // void Awake() 
    // {
    //     if (instance == null)
    //     {
    //         instance = this;
    //         DontDestroyOnLoad(gameObject); // 씬 전환 시 파괴되지 않도록 설정
    //     }
    //     else
    //     {
    //         Destroy(gameObject);
    //     }
    // }

    void Start()
    {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication); // 자동로그인 수신 결과 대기   
    }

    void ProcessAuthentication(SignInStatus status) 
    {
        if (status == SignInStatus.Success) 
        {
            // Debug.Log("로그인 성공");
        } 
        else 
        {
            // Debug.Log("로그인 실패");
            gameObject.SetActive(false);
            // Disable your integration with Play Games Services or show a login button
            // to ask users to sign-in. Clicking it should call
            // PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication).
        }
    }

    public void HandleButtonClick()
    {
        PlayGamesPlatform.Instance.ShowLeaderboardUI("CgkI8vee2q8NEAIQAQ"); // 점수 리더보드임
    }
}
