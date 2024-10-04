using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GoNextScene : MonoBehaviour
{
    public string nextSceneName;

    void Start()
    {
        // 3초 후에 씬을 변경하는 코루틴 실행
        StartCoroutine(ChangeSceneAfterDelay());
    }

    IEnumerator ChangeSceneAfterDelay()
    {
        // 3초 대기
        yield return new WaitForSeconds(4f);
        // 다음 씬으로 이동
        SceneManager.LoadScene("StartScene");
    }
}
