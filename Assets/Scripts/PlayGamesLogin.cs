using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using TMPro;

public class PlayGamesLogin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Google Play 게임 서비스 관련 디버그 로그를 활성화
        PlayGamesPlatform.DebugLogEnabled = true;
        // Google Play 게임 플랫폼 활성화
        PlayGamesPlatform.Activate();
        // 사용자 로그인 시도
        SignIn();
    }

    public void SignIn()
    {
        // Google Play 게임 서비스 인증 메서드 호출, 인증 결과 처리를 위해 콜백 메서드로 ProcessAuthentication 전달
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    // 인증 처리 결과를 분석하고 UI에 반영하는 메서드
    internal void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            // // 로그인 성공 시, 사용자 정보 가져오기
            // string name = PlayGamesPlatform.Instance.GetDisplayName(); // 사용자 이름
            // string id = PlayGamesPlatform.Instance.GetUserId(); // 사용자 ID
            // string imgUrl = PlayGamesPlatform.Instance.GetUserImageUrl(); // 사용자 프로필 이미지 URL
            Debug.Log("Login Successful");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
