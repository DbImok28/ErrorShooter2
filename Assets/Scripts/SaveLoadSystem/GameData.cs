using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    public float playerHealth;

    public float playerX;
    public float playerY;
    public float playerZ;

    public SerializableDictionary<string, KeyData> keysData;
    public SerializableDictionary<string, EnemyData> enemiesData;

    public float lateralSensitivity;
    public float verticalSensitivity;
    public float maxPitchAngle;
    public float minPitchAngle;

    public GameData()
    {
        playerHealth = 10;
        //playerAmmoRemain = 30;
        playerX = 3.8f;
        playerY = 0;
        playerZ = 3.3f;

        keysData = new SerializableDictionary<string, KeyData>();
        enemiesData = new SerializableDictionary<string, EnemyData>();

        lateralSensitivity = 4;
        verticalSensitivity = 4;
        maxPitchAngle = 90;
        minPitchAngle = -90;

    }

    public KeyData LoadKey(string name)
    {
        KeyData smth;
        keysData.TryGetValue(name, out smth);

        return smth;
    }

    public void SaveKey(string id, bool isPickedUp,string Name)
    {
        if (keysData.ContainsKey(id))
        {
            keysData.Remove(id);
        }

        KeyData keyData = new KeyData(id, isPickedUp, Name);

        keysData.Add(id, keyData);
    }

    public void ClearAllBots()
    {
        Debug.Log("clear all bots");

        enemiesData.Clear();

        Debug.Log($"cleared list length : {enemiesData.Count}");
        enemiesData = new SerializableDictionary<string, EnemyData>();
    }

    public EnemyData LoadBot(string id)
    {
        EnemyData smth;
        enemiesData.TryGetValue(id, out smth);

        return smth;
    }

    public void SaveBot(string id, float x, float y, float z, float health)
    {
        if (enemiesData.ContainsKey(id))
        {
            enemiesData.Remove(id);
        }

        EnemyData enemyData = new EnemyData( x,y,z, health);

        enemiesData.Add(id, enemyData);
    }


}
