using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotManager : MonoBehaviour, ISaveable
{
    private List<GameObject> bots=new List<GameObject>();

    private void SpawnBot(BotFactory botFactory,float x, float y, float z, float health)
    {
        GameObject bot = botFactory.FactoryMethod(x,y,z,health);

        AddBot(bot);

        bot.GetComponent<HealthComponent>().OnDie.AddListener(RemoveBot);
    }

    public void RemoveBot(GameObject bot)
    {
        bots.Remove(bot);
    }

    public void AddBot(GameObject bot)
    {
        bots.Add(bot);

        Debug.Log($"bots length : {bots.Count}");
    }

    public void LoadData(GameData gameData)
    {

        foreach (KeyValuePair<string, EnemyData> entry in gameData.enemiesData) {

            BotFactory botFactory = new BotDistanceFactory();

            SpawnBot(botFactory, entry.Value.enemyX, entry.Value.enemyY, entry.Value.enemyZ, entry.Value.health);
        }
    }

    public void SaveData(ref GameData gameData)
    {

        foreach (GameObject bot in bots)
        {
            string id = bot.GetComponent<EnemyInterface>().GetId();

            float health = bot.GetComponent<HealthComponent>().CurrentHealth;

            float x = bot.transform.position.x;
            float y = bot.transform.position.y;
            float z = bot.transform.position.z;

            gameData.SaveBot(id, x, y,z, health);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //рандомно выбрать фабрику

        BotFactory bf = new BotMeleeFactory();

        SpawnBot(bf, 0, 0, 0, 10);
    }
}
