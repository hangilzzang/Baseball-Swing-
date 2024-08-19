using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBallIcon : MonoBehaviour
{
    public Button uiButton;
    public Image infoImage;
    public Sprite ballInfoSprite;
    public GameObject infoUI;
    void Start()
    {
        uiButton.onClick.AddListener(OnButtonClicked);
    }


    void OnButtonClicked()
    {
        if (GameManager.instance.gameState == GameManager.GameState.GameStart && !GameManager.instance.ballADClicked)
        {
            GameManager.instance.gameState = GameManager.GameState.InfoUI;
            infoImage.sprite = ballInfoSprite;
            infoUI.SetActive(true);
        }

        else if (GameManager.instance.gameState == GameManager.GameState.InfoUI)
        {
            GameManager.instance.gameState = GameManager.GameState.GameStart;
            infoUI.SetActive(false);
        }
    }
}
