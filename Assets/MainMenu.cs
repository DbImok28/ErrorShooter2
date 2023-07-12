using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    const string GAMEPLAY_SCENE_NAME = "MainScene";

    public Button NewGameButton;
    public Button ContinueGameButton;

    private void Start()
    {
        if (!SaveLoadManager.instance.HasGameData())
        {
            ContinueGameButton.interactable = false;
        }

        NewGameButton.onClick.AddListener(NewGame);
        ContinueGameButton.onClick.AddListener(ContinueGame);
    }


    public void NewGame()
    {

        SaveLoadManager.instance.NewGame();
        // load the gameplay scene - which will in turn save the game because of
        // OnSceneUnloaded() in the DataPersistenceManager
        SceneManager.LoadSceneAsync(GAMEPLAY_SCENE_NAME);

    }

    public void ContinueGame()
    {
        SceneManager.LoadSceneAsync(GAMEPLAY_SCENE_NAME);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
