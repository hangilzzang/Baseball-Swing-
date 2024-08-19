using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchToRestartUI : MonoBehaviour
{
    public GameObject player;
    public GameObject ball;
    public GameObject powerGauge;
    Animator animator;
    public CanvasRenderer uiComponent;
    // public AudioSource ballAudio; // 오디오 컴포넌트
    // public AudioSource TouchToRestartAudio; 

    
    void Start()
    {
        animator = player.GetComponent<Animator>();
        // ballAudio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        // if (GameManager.instance.gameState == GameManager.GameState.GameRestart && Input.GetMouseButtonUp(0))
        if (GameManager.instance.gameState == GameManager.GameState.GameRestart) // 마우스 입력 받지않고 자동으로 넘어가도록 수정함
        {
            // TouchToRestartAudio.volume = GameManager.instance.TouchToStartVolume;
            // TouchToRestartAudio.Play();
            
            GameManager.instance.gameState = GameManager.GameState.NotGettingAnyInput; // 애니메이션 재생될동안 어떠한 입력도받지않기
            
            // gameObject.SetActive(false);
            uiComponent.SetAlpha(0f);
            
            // ball.SetActive(false);
            powerGauge.SetActive(false);
            
            animator.SetTrigger("PickUp"); 
        }
    }
}
