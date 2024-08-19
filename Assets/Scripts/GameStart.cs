using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameStart : MonoBehaviour
{
    public AudioSource audioSource;
    void GoGameScene()
    {
        GameManager.instance.ChangeScene(GameManager.SceneNames.GameScene);
    }

    void PlayGrassSound()
    {
        audioSource.Play();
    }
}
