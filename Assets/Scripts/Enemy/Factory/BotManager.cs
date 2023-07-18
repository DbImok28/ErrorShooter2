using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotManager : MonoBehaviour, ISaveable
{
    private List<GameObject> bots = new List<GameObject>();
    private List<BotSpawner> spawners = new List<BotSpawner>();

    private void Start()
    {
        FindSpawners();
    }
    private void FindSpawners()
    {
        var _spawners = FindObjectsOfType<BotSpawner>();

        foreach (BotSpawner _spawner in _spawners)
        {
            spawners.Add(_spawner);

            _spawner.BotSpawned.AddListener(AddBot);
        }

        //Debug.Log("spawners length " + spawners.Count);
    }

    private void LoadBot(BotFactory botFactory, float x, float y, float z, float health)
    {
        GameObject bot = botFactory.FactoryMethod(x, y, z, health, Quaternion.identity);

        AddBot(bot);

        bot.GetComponent<HealthComponent>().OnDie.AddListener(RemoveBot);
    }

    public void RemoveBot(GameObject bot)
    {
        bots.Remove(bot);

        Debug.Log($"REMOVE bots length : {bots.Count}");
    }

    public void AddBot(GameObject bot)
    {
        bots.Add(bot);

        bot.GetComponent<HealthComponent>().OnDie.AddListener(RemoveBot);

        Debug.Log($"ADD bots length : {bots.Count}");
    }

    public void LoadData(GameData gameData)
    {

        foreach (KeyValuePair<string, EnemyData> entry in gameData.enemiesData)
        {

            BotFactory botFactory = new BotMeleeFactory();

            LoadBot(botFactory, entry.Value.enemyX, entry.Value.enemyY, entry.Value.enemyZ, entry.Value.health);

            Debug.Log("load bot");
        }
    }

    public void SaveData(ref GameData gameData)
    {
        //Debug.Log("bot manager savedata");

        gameData.enemiesData.Clear();
        gameData.enemiesData = new SerializableDictionary<string, EnemyData>();

        //Debug.Log($"cleared list length : {gameData.enemiesData.Count}");

        foreach (GameObject bot in bots)
        {
            //string id = bot.GetComponent<EnemyInterface>().GetId();
            string id = System.Guid.NewGuid().ToString();

            float health = bot.GetComponent<HealthComponent>().CurrentHealth;

            float x = bot.transform.position.x;
            float y = bot.transform.position.y;
            float z = bot.transform.position.z;


            EnemyType type;
            if (bot.GetComponent<EnemyInterface>())
            {
                type = bot.GetComponent<EnemyInterface>().enemyType;
            }
            else
            {
                type = EnemyType.distant;
            }



            gameData.SaveBot(id, x, y, z, health, type);

            Debug.Log("save bot");
        }
    }

}
