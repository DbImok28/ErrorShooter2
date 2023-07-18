using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoadManager : MonoBehaviour
{
    private GameData gameData;

    //private HealthComponent healthComponent;

    private JsonDataHandler jsonDataHandler;

    [SerializeField] private bool initializeDataIfNull = true;


    public static SaveLoadManager instance { get; private set; }

    public List<ISaveable> saveables;

    [SerializeField] private string fileName = "gamedata.json";

    private void Awake()
    {
        if (instance != null)
        {
            //Debug.Log("Found more than one Data Persistence Manager in the scene. Destroying the newest one.");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        this.jsonDataHandler = new JsonDataHandler(Application.persistentDataPath, fileName);

    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        this.saveables = GetSaveables();
        LoadGame();
    }

    public void OnSceneUnloaded(Scene scene)
    {

    }


    /*
    public void FindPlayerHealthComponent()
    {
        healthComponent = FindObjectsOfType<PlayerController>()[0].gameObject.GetComponent<HealthComponent>();
    }
    */



    public void Init()
    {
        jsonDataHandler = new JsonDataHandler(Application.persistentDataPath, fileName);
        saveables = GetSaveables();
    }


    public void NewGame()
    {
        //Debug.Log("new game");

        this.gameData = new GameData();

        //Debug.Log(this.gameData.playerHealth);
        SaveGame();
        jsonDataHandler.SaveData(gameData);

    }

    public void SaveSettings(SettingsManager sm)
    {
        Debug.Log("save settings");
        sm.SaveData(ref gameData);

        jsonDataHandler.SaveData(gameData);
    }

    public void SaveGame()
    {
        Debug.Log("SAVE GAME");

        // if we don't have any data to save, log a warning here
        if (this.gameData == null)
        {
            Debug.LogWarning("No data was found. A New Game needs to be started before data can be saved.");
            return;
        }

        // pass the data to other scripts so they can update it
        foreach (ISaveable dataPersistenceObj in saveables)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }

        // save that data to a file using the data handler
        jsonDataHandler.SaveData(gameData);
    }

    private void OnApplicationQuit()
    {

    }

    public void LoadGame()
    {

        //Debug.Log("LOAD GAME");
        this.gameData = jsonDataHandler.LoadData();

        // start a new game if the data is null and we're configured to initialize data for debugging purposes
        if (this.gameData == null && initializeDataIfNull)
        {
            NewGame();
        }

        // if no data can be loaded, don't continue
        if (this.gameData == null)
        {
            Debug.Log("No data was found. A New Game needs to be started before data can be loaded.");
            return;
        }

        // push the loaded data to all other scripts that need it
        foreach (ISaveable dataPersistenceObj in saveables)
        {
            dataPersistenceObj.LoadData(gameData);
        }
    }

    public List<ISaveable> GetSaveables()
    {
        List<ISaveable> saveables = FindObjectsOfType<MonoBehaviour>()
            .OfType<ISaveable>().ToList();


        /*
        if (SceneManager.GetActiveScene().name == "DeathRespawnScene")
        {

            PlayerController playerController = FindObjectOfType<PlayerController>();
            if (playerController)
            {
                Camera camera = playerController.GetComponentInChildren<Camera>();
                if (camera)
                {

                    GameObject WeaponSocket = camera.gameObject.transform.GetChild(0).gameObject;
                    if (WeaponSocket)
                    {
                        Weapon weapon = WeaponSocket.gameObject.transform.GetChild(0).gameObject.GetComponentInChildren<Weapon>();
                        if (weapon != null)
                        {
                            if (weapon.Magazine.TryGetComponent<ISaveable>(out ISaveable saveable))
                            {
                                ISaveable isaveable = weapon.Magazine.GetComponent<ISaveable>();

                                saveables.Add(isaveable);
                            };

                        }
                    }

                }
            }

        }
        */





        return new List<ISaveable>(saveables);
    }

    public bool HasGameData()
    {
        //Debug.Log($"HAS GAMEDATA {gameData != null}");
        return gameData != null;
    }
}
