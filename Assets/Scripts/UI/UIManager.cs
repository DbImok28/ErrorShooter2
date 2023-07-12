using Assets.Scripts.Weapon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour, ISaveable
{
    // Start is called before the first frame update
    public Button PauseButton;
    public Button RespawnButton;
    public Button MainMenuButton;

    public HealthProgressBar HealthProgressBar;
    public AmmoBar AmmoBar;
    public WeaponBar WeaponBar;
    public InteractableInfo InteractableInfo;
    public KeysBar KeysBar;

    public DamageSrceen damageSrceen;
    public DeathDialog deathDialog;

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

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Init()
    {
        player = FindObjectsOfType<PlayerController>()[0];
        health = FindObjectsOfType<PlayerController>()[0].gameObject.GetComponent<HealthComponent>();
        //magazine = FindObjectsOfType<MagazineWeaponAttack>()[0];
        inventory= FindObjectsOfType<PlayerInventory>()[0];
        pei= FindObjectsOfType<PlayerController>()[0].gameObject.GetComponent<PlayerEnvironmentInteraction>();


        //Debug.Log(pei);

        pauseManager = new PauseManager();

        PauseButton.onClick.AddListener(pauseManager.TogglePause);
        RespawnButton.onClick.AddListener(RespawnCommand);
        MainMenuButton.onClick.AddListener(MainMenuCommand);

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
        //Debug.Log("UpdateAmmoBarWeapon");
        AmmoBar.UpdateWeapon(weapon);
    }

    public void RespawnCommand()
    {
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
        //Debug.Log("обновляем интерфейс...");
    }

    public void PlayDamageScreen(HealthComponent hc, float damage) {
        damageSrceen.Play();
    }

    public void ShowDeathDialog() {
        deathDialog.Show();
        UnlockCursor();
    }

    public void HideDeathDialog()
    {
        deathDialog.Hide();
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
