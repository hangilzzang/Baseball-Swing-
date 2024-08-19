using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOutWhite : MonoBehaviour
{
    float imageAlpha = 0;
    float transitionSpeed = 0.33f;
    public Image transitionImage; 
    void Update() 
    {
        if (GameManager.instance.gameOver == true)
        {
            if ( imageAlpha < 0.8f )
            {
                imageAlpha += Time.deltaTime * transitionSpeed;
                transitionImage.color = new Color(transitionImage.color.r, transitionImage.color.g, transitionImage.color.b, imageAlpha);
            }
            else
            {
                GameManager.instance.gameOver = false;
                GameManager.instance.ChangeScene(GameManager.SceneNames.ResultScene);
                GameManager.instance.gameState = GameManager.GameState.GameRestart;
            }
        }
    }
}
