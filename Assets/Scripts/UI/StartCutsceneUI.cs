using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartCutsceneUI : MonoBehaviour
{
    public Button GoToGameButton;
    void Start()
    {
        GoToGameButton.onClick.AddListener(LoadGameScene);
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("MainLabScene");
    }

}
