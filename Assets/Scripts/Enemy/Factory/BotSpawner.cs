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

    private bool playerVisitedTrigget;

    private BotDistanceFactory botDistanceFactory=new BotDistanceFactory();
    private BotMeleeFactory botMeleeFactory=new BotMeleeFactory();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>() && !playerVisitedTrigget)
        {
            playerVisitedTrigget = true;

            SpawnBots();
        }
    }

    public void SpawnBots()
    {
        Debug.Log("Spawn bots");

        for(int i=0; i < EnemeMeeleAmount; i++)
        {
            Vector3 randomPosition = PickRandomSpawnPosition();
            float startHelath=5;

            GameObject bot = botMeleeFactory.FactoryMethod(randomPosition.x, randomPosition.y, randomPosition.z, startHelath);

            BotSpawned?.Invoke(bot);
        }

        for (int i = 0; i < EnemyDistanceAmount; i++)
        {
            Vector3 randomPosition = PickRandomSpawnPosition();
            float startHelath = 5;

            GameObject bot = botDistanceFactory.FactoryMethod(randomPosition.x, randomPosition.y, randomPosition.z, startHelath);

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
        float y = 0;

        Debug.Log($"rx : {rX} rZ : {rZ}");

        return new Vector3(rX, y, rZ);
    }
}
