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

    public GameData()
    {
        playerHealth = 10;
        //playerAmmoRemain = 30;
        playerX = 0;
        playerY = 0;
        playerZ = 0;

        keysData = new SerializableDictionary<string, KeyData>();

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


}
