using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PowerGaugeController : MonoBehaviour
{
    Animator animator;
    public ThrowBall ball;
    public PlayerController player; 
    public GameObject help;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat("AnimationSpeed", GameManager.instance.powerGaugeAnimationSpeed);
    }

    void Update()
    {
        if (GameManager.instance.gameState == GameManager.GameState.PowerGauge && Input.GetMouseButtonDown(0)) // 파워게이지 게임 상태일때 클릭 입력 발생시
        {
            
            if (GameManager.instance.gamePlayedTimes == 1)
            {
                help.SetActive(false);
            }

            animator.speed = 0; // 파워게이지 애니메이션 중지
            
            GameManager.instance.gameState = GameManager.GameState.NotGettingAnyInput; // 게임 상태 변경
            
            GameManager.instance.powerValue = animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1f; // 애니메이션의 진행정도에 따라 게이지값 할당, 0~1범위
            GameManager.instance.CalculateThrowForce();
            player.PlayThrowBallAnimation();
            ball.PlayBallAnimation();

        }
    }
}
