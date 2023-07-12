using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyForDoor : MonoBehaviour, ISaveable
{

    public string KeyName;

    public bool isPickedUp;

    public void Hide()
    {
        //Debug.Log("hide");
        gameObject.GetComponentInChildren<Renderer>().enabled = false; 
    }

    public void LoadData(GameData gameData)
    {
        /*
        Debug.Log("load key");
        KeyData keyData = gameData.LoadKey(gameObject.GetInstanceID());

        isPickedUp = keyData.isPickedUp;
        keyDoorId = keyData.keyDoorId;
        KeyName = keyData.name;

        Debug.Log($"is key picked up {isPickedUp}");

        if (isPickedUp)
            Hide();
        */
        //Debug.Log("key for door load data");

        //Debug.Log(gameObject.GetInstanceID());

        KeyData keyData = gameData.LoadKey(KeyName);

        if (keyData!=null)
        {
            //Debug.Log("keyData!=null");
            isPickedUp = keyData.isPickedUp;
            KeyName = keyData.name;

            //Debug.Log($"isPickedUp {isPickedUp}");

            if (isPickedUp)
                Hide();
        }

        
    }

    public void SaveData(ref GameData gameData)
    {
        //Debug.Log("key for door save data");

        gameData.SaveKey(KeyName, isPickedUp,KeyName);
    }
}
