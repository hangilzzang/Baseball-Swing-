using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBatIcon : MonoBehaviour
{
    public Button uiButton;
    public Image infoImage;
    public Sprite batInfoSprite;
    public GameObject infoUI;
    void Start()
    {
        uiButton.onClick.AddListener(OnButtonClicked);
    }

    // Update is called once per frame
    void OnButtonClicked()
    {
        if (GameManager.instance.gameState == GameManager.GameState.GameStart && !GameManager.instance.batADClicked)
        {
            GameManager.instance.gameState = GameManager.GameState.InfoUI;
            infoImage.sprite = batInfoSprite;
            infoUI.SetActive(true);
        }

        else if (GameManager.instance.gameState == GameManager.GameState.InfoUI)
        {
            GameManager.instance.gameState = GameManager.GameState.GameStart;
            infoUI.SetActive(false);
        }
    }
}
