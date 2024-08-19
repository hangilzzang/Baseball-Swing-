using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;
    public delegate void WatchedRewardAd(string type); // 델리게이트정의
    public event WatchedRewardAd watchedRewardAd; // 이벤트 선언
    void Awake()
    {
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

    public void TriggerWatchedRewardAd(string type) //이벤트 트리거 메서드 정의
    {
        watchedRewardAd?.Invoke(type);
    }
}
