using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour, ISaveable
{
    [SerializeField] public List<GameObject> Weapons = new List<GameObject>();

    [SerializeField] public List<string> KeysNames = new List<string>();

    public UnityEvent<List<string>> KeyPickedUp;

    public void AddKey(GameObject key)
    {
        KeysNames.Add(key.GetComponent<KeyForDoor>().KeyName);

        KeyPickedUp?.Invoke(KeysNames);
    }

    public void LoadData(GameData gameData)
    {
        //Debug.Log("player inventory load data");

        foreach(KeyValuePair<string, KeyData> entry in gameData.keysData)
        {
            if (entry.Value.isPickedUp)
            {
                KeysNames.Add(entry.Value.name);
            }
        }
    }

    public void SaveData(ref GameData gameData)
    {
        
    }

    // ...
}
