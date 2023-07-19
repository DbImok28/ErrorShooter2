using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndCutsceneUI : MonoBehaviour
{
    // Start is called before the first frame update
    public Button MainMenuButton;
    void Start()
    {
        MainMenuButton.onClick.AddListener(GoToMenu);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
