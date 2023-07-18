using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BotSpawner : MonoBehaviour
{
    public BoxCollider BotSpawnTerritory;

    public int EnemeMeeleAmount;
    public int EnemyDistanceAmount;

    public UnityEvent<GameObject> BotSpawned;

    public GameObject LookTarget;

    private bool playerVisitedTrigget;

    private BotDistanceFactory botDistanceFactory = new BotDistanceFactory();
    private BotMeleeFactory botMeleeFactory = new BotMeleeFactory();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>() && !playerVisitedTrigget)
        {
            playerVisitedTrigget = true;

            SpawnBots();
        }
    }

    public void LookAt(GameObject bot)
    {
        GameObject toLookObject = LookTarget != null ? LookTarget : FindObjectOfType<PlayerController>().gameObject;

        Vector3 targetPostition = new Vector3(toLookObject.transform.position.x,
                    transform.position.y, toLookObject.transform.position.z);
        bot.transform.LookAt(targetPostition);

    }

    public void SpawnBots()
    {
        Debug.Log("Spawn bots");

        for (int i = 0; i < EnemeMeeleAmount; i++)
        {
            Vector3 randomPosition = PickRandomSpawnPosition();
            float startHelath = 5;

            GameObject bot = botMeleeFactory.FactoryMethod(randomPosition.x, randomPosition.y, randomPosition.z, startHelath);
            LookAt(bot);

            BotSpawned?.Invoke(bot);
        }

        for (int i = 0; i < EnemyDistanceAmount; i++)
        {
            Vector3 randomPosition = PickRandomSpawnPosition();
            float startHelath = 5;

            GameObject bot = botDistanceFactory.FactoryMethod(randomPosition.x, randomPosition.y, randomPosition.z, startHelath);
            LookAt(bot);

            BotSpawned?.Invoke(bot);
        }
    }

    public Vector3 PickRandomSpawnPosition()
    {
        float minX = BotSpawnTerritory.bounds.min.x;
        float maxX = BotSpawnTerritory.bounds.max.x;
        float minZ = BotSpawnTerritory.bounds.min.z;
        float maxZ = BotSpawnTerritory.bounds.max.z;

        Debug.Log($"minX : {minX} maxX: {maxX} minZ: {minZ} maxZ : {maxZ}");

        float rX = Random.Range(minX, maxX);
        float rZ = Random.Range(minZ, maxZ);
        float y = 2;

        Debug.Log($"rx : {rX} rZ : {rZ}");

        return new Vector3(rX, y, rZ);
    }
}
