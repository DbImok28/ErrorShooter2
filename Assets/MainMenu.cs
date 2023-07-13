using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    const string GAMEPLAY_SCENE_NAME = "MainLabScene";

    public Button NewGameButton;
    public Button ContinueGameButton;
    public Button SettingsButton;
    public Button BackToMenuButton;

    public SettingDialog settingsDialog;

    private void Start()
    {
        if (!SaveLoadManager.instance.HasGameData())
        {
            ContinueGameButton.interactable = false;
        }

        NewGameButton.onClick.AddListener(NewGame);
        ContinueGameButton.onClick.AddListener(ContinueGame);
        SettingsButton.onClick.AddListener(ShowSettingsDialog);
        BackToMenuButton.onClick.AddListener(HandleBackToMenu);
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

    public void HandleBackToMenu()
    {
        SaveLoadManager.instance.SaveGame();
        HideSettingsDialog();
    }

    public void ShowSettingsDialog()
    {
        settingsDialog.gameObject.SetActive(true);
        Debug.Log("ShowSettingsDialog");
        Debug.Log($"settingsDialog.enabled {settingsDialog.enabled}");
        //settingsDialog.Show();
    }

    public void HideSettingsDialog()
    {
        //settingsDialog.Hide();
        settingsDialog.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
