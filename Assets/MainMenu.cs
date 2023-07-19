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
    public Button ExitGameButton;

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
        ExitGameButton.onClick.AddListener(ExitGame);
}


    public void NewGame()
    {

        SaveLoadManager.instance.NewGame();

        SceneManager.LoadScene("StartCutscene");

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

    public void ExitGame()
    {
        Application.Quit();
    }
}
