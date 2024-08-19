using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchToRestartUI2 : MonoBehaviour
{
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.gameState == GameManager.GameState.GameRestart && Input.GetMouseButtonUp(0))
        {
            GameManager.instance.ChangeScene(GameManager.SceneNames.GameScene);
        }
    }
}
