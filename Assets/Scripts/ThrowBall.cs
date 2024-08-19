using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBall : MonoBehaviour
{
    
    Vector2 sweetSpotPosition = new Vector2(1f, -1.81f);
    Rigidbody2D ballRigidbody;
    Animator animator;
    public GameObject restartText;

    bool hasReachedApex = false;
    float minHeight = 1f;
    float velocityThreshold = 0.1f;


    bool ballOnGround = false;

    public AudioSource ballAudio; // 오디오 컴포넌트

    public SpriteRenderer spriteRenderer;
    public Sprite newSprite;

    
    void Start()
    {
        ballRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    
    
    
    void Update()
    {

        if (minHeight < ballRigidbody.position.y && !hasReachedApex && Mathf.Abs(ballRigidbody.velocity.y) < velocityThreshold) // 공이 최고점에 도달했나?
        {
            hasReachedApex = true;
            MoveToSweetSpot();
        }

        if (ballOnGround == true && GameManager.instance.gameState == GameManager.GameState.BatSwing)
        {
            GameManager.instance.gameState = GameManager.GameState.GameRestart;
            restartText.SetActive(true);       
        }
    }

    void MoveToSweetSpot()
    {
        Vector2 newPosition = new Vector2(sweetSpotPosition.x, ballRigidbody.position.y);
        ballRigidbody.position = newPosition;

        if (GameManager.instance.parachuteBall)
        {
            animator.enabled = false; // 스프라이트 변경을위해 애니메이션 중지
            spriteRenderer.sprite = newSprite;
            ballRigidbody.gravityScale = GameManager.instance.parachuteBallAdvantage; // 중력 스케일 조절
        }
        
    }


    void BallGoesUp() //애니메이션 이벤트로부터 실행
    {
        ballRigidbody.WakeUp(); 
        ballRigidbody.AddForce(Vector2.up * GameManager.instance.throwForce);
    }

    public void PlayBallAnimation()
    {
        animator.SetFloat("AnimationSpeed", GameManager.instance.throwAnimationSpeed);
        animator.SetBool("ThrowBall", true);
    }

    void OnCollisionEnter2D(Collision2D collision) // 공이 땅과 접촉시 실행
    {
        ballOnGround = true;
        ballAudio.Play();
    }

}
