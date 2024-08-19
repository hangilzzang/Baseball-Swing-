using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator animator;    
    public Collider2D batCollider;
    public Collider2D ballCollider;
    public Rigidbody2D ballRigidbody;
    
    public AudioSource playerAudio; // 오디오 컴포넌트
    public AudioClip batHitClip; 
    public AudioClip batSwingClip; 
    public AudioClip throwBallClip; 
    public AudioClip grabBallClip;

    public GameObject ball;
  



    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (GameManager.instance.gameState == GameManager.GameState.BatSwing && Input.GetMouseButtonDown(0))
        {
            animator.SetBool("Swing", true);
            GameManager.instance.gameState = GameManager.GameState.NotGettingAnyInput;
            playerAudio.clip = batSwingClip;
            playerAudio.volume = GameManager.instance.batSwingVolume;
            playerAudio.Play();
        }
    }

    public void PlayThrowBallAnimation()
    {
        animator.SetFloat("AnimationSpeed", GameManager.instance.throwAnimationSpeed);
        animator.SetBool("ThrowBall", true);

    }


    void PlaySwing2Animation() // 애니메이션 이벤트로부터 실행
    {
        CalculateHitAccuracy();


        if (GameManager.instance.accuracyValue == 0)
        {
            animator.SetBool("Swing", false);
        }
        else
        {
            playerAudio.clip = batHitClip; // 클립 바꾸기
            playerAudio.volume = GameManager.instance.batHitVolume;
            playerAudio.Play();


            GameManager.instance.CalculateScore();
            // 떨어지는 공을 멈추고 멈춘 상태를 유지
            ballRigidbody.isKinematic = true; 
            ballRigidbody.velocity = Vector2.zero;
            // Debug.Log("scoreValue: " + GameManager.instance.scoreValue);
            GameManager.instance.gameOver = true;
        }
    }

    void SwingAvailable() // 애니메이션 이벤트로부터 실행
    {
        GameManager.instance.gameState = GameManager.GameState.BatSwing;
    }


    void CalculateHitAccuracy()
    {
        // 바운딩 박스 계산
        Bounds batBounds = batCollider.bounds;
        Bounds ballBounds = ballCollider.bounds;

        // 겹치는 영역 계산
        float xMin = Mathf.Max(batBounds.min.x, ballBounds.min.x);
        float xMax = Mathf.Min(batBounds.max.x, ballBounds.max.x);
        float yMin = Mathf.Max(batBounds.min.y, ballBounds.min.y);
        float yMax = Mathf.Min(batBounds.max.y, ballBounds.max.y);

        if (xMin < xMax && yMin < yMax)
        {
            // 겹치는 영역 면적
            float overlapArea = (xMax - xMin) * (yMax - yMin);

            // 배트와 공의 전체 면적 계산 (겹치는 부분 제외)
            float batArea = batBounds.size.x * batBounds.size.y;
            float ballArea = ballBounds.size.x * ballBounds.size.y;
            float totalArea = batArea + ballArea - overlapArea;

            // 정확도 계산
            float accuracy = overlapArea / totalArea;


            GameManager.instance.accuracyValue = accuracy;
        }
        else
        {
            // 겹치는 부분이 없을 경우 정확도는 0%
            GameManager.instance.accuracyValue = 0f;
        }
    }

    void RestartScene()
    {
        GameManager.instance.ChangeScene(GameManager.SceneNames.GameScene);
    }

    void PlayThrowSound() // 애니메이션 이벤트로부터 실행
    {
        playerAudio.clip = throwBallClip;
        playerAudio.volume = GameManager.instance.throwBallClipVolume;
        playerAudio.Play();
    }

    void PlayGrabSound() // 애니메이션 이벤트로부터 실행
    {
        playerAudio.clip = grabBallClip;
        playerAudio.volume = GameManager.instance.grabBallVolume;
        playerAudio.Play();
        ball.SetActive(false);
    }
}
