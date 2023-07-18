using Assets.Scripts.Weapon;
using ECM.Components;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour, ISaveable
{
    // Start is called before the first frame update
    public Button ContinueGameButton;
    public Button RespawnButton;
    public Button MainMenuButton;
    public Button PauseMainMenuButton;
    public Button SettingsShowButton;
    public Button SettingsHideButton;

    public HealthProgressBar HealthProgressBar;
    public AmmoBar AmmoBar;
    public WeaponBar WeaponBar;
    public InteractableInfo InteractableInfo;
    public KeysBar KeysBar;

    public DamageSrceen damageSrceen;
    public PauseDialog pauseDialog;
    public DeathDialog deathDialog;
    public SettingDialog settingsDialog;

    private PlayerController player;
    private HealthComponent health;
    private MagazineWeaponAttack magazine;
    private PlayerInventory inventory;
    private PlayerEnvironmentInteraction pei;

    private PauseManager pauseManager;

    
    void Start()
    {
        Init();
        InitUIItems();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!pauseManager.IsPaused)
            {
                StartPause();
            }
            else
            {
                EndPause();
            }
            
            

        }
    }

    public void StartPause()
    {
        pauseDialog.gameObject.SetActive(true);

        pauseManager.SetPaused(true);
    }

    public void EndPause()
    {
        pauseDialog.gameObject.SetActive(false);

        pauseManager.SetPaused(false);
    }


    public void Init()
    {
        pauseManager = new PauseManager();

        player = FindObjectsOfType<PlayerController>()[0];
        health = FindObjectsOfType<PlayerController>()[0].gameObject.GetComponent<HealthComponent>();
        inventory= FindObjectsOfType<PlayerInventory>()[0];
        pei= FindObjectsOfType<PlayerController>()[0].gameObject.GetComponent<PlayerEnvironmentInteraction>();

        SubscrbiePauseHandlers();

        RespawnButton.onClick.AddListener(RespawnCommand);
        MainMenuButton.onClick.AddListener(MainMenuCommand);
        PauseMainMenuButton.onClick.AddListener(MainMenuCommand);
        ContinueGameButton.onClick.AddListener(EndPause);
        SettingsShowButton.onClick.AddListener(ShowSettingsDialog);
        SettingsHideButton.onClick.AddListener(HideSettingsDialog);

        health.OnTakeDamage.AddListener(PlayDamageScreen);
        health.OnDie.AddListener(ShowDeathDialog);

        player.WeaponChanged.AddListener(UpdateAmmoBarWeapon);

    }

    public void InitUIItems()
    {
        HealthProgressBar.Init(health);
        AmmoBar.Init(player);
        InteractableInfo.Init(pei);
        WeaponBar.Init(player,inventory);
        KeysBar.Init(inventory);

    }

    public void UpdateAmmoBarWeapon(Weapon weapon)
    {
        AmmoBar.UpdateWeapon(weapon);
    }

    public void RespawnCommand()
    {
        health.Respawn();
        HideDeathDialog();
        SaveLoadManager.instance.LoadGame();
    }

    public void MainMenuCommand()
    {
        HideDeathDialog();
        SceneManager.LoadSceneAsync("MainMenu");
    }

    public void SaveData(ref GameData gameData)
    {
        
    }

    public void LoadData(GameData gameData)
    {
        Init();
        InitUIItems();
    }

    public void PlayDamageScreen(HealthComponent hc, float damage) {
        damageSrceen.Play();
    }

    public void ShowDeathDialog(GameObject _gameobject) {
        RespawnButton.interactable = true;
        MainMenuButton.interactable = true;

        deathDialog.gameObject.SetActive(true);
    }

    

    public void HideDeathDialog()
    {
        RespawnButton.interactable = false;
        MainMenuButton.interactable = false;

        deathDialog.gameObject.SetActive(false);
    }

    public void ShowSettingsDialog()
    {
        //RespawnButton.interactable = true;
        //MainMenuButton.interactable = true;

        settingsDialog.gameObject.SetActive(true);
    }



    public void HideSettingsDialog()
    {
        //RespawnButton.interactable = false;
        //MainMenuButton.interactable = false;

        settingsDialog.gameObject.SetActive(false);
    }

    public void LockCursor()
    {
        player.gameObject.GetComponent<MouseLook>().lockCursor = true;
        Cursor.visible = false;
    }

    public void UnlockCursor()
    {
        player.gameObject.GetComponent<MouseLook>().lockCursor = false;
        Cursor.visible = true;
    }

    public void AddPauseHandler(IPauseHandler pauseHandler)
    {
        pauseManager.AddPauseHandler(pauseHandler);
    }

    private void SubscrbiePauseHandlers()
    {
        pauseManager.AddPauseHandler(player);
        pauseManager.AddPauseHandler(player.gameObject.GetComponent<MouseLook>());

        List<IPauseHandler> enemies = FindObjectsOfType<MonoBehaviour>().OfType<EnemyInterface>().OfType<IPauseHandler>().ToList();

        foreach(IPauseHandler enemy in enemies)
        {
            AddPauseHandler(enemy);
        }
    }

}
