using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyForDoor : MonoBehaviour, ISaveable, IPickableItem
{

    public string KeyName;

    public bool isPickedUp;

    public UnityEvent<GameObject> OnKeyPickUp;
    public UnityEvent<GameObject> OnPickUp => OnKeyPickUp;

    public void PickUp(GameObject player)
    {
        //OnPickUp.Invoke(player);
        PlayerInventory inventory = player.GetComponentInChildren<PlayerEnvironmentInteraction>().GetInventory();
        inventory.AddKey(this.gameObject);

        Debug.Log("key pick up");

        HideAfterPickUp();
    }

    public void HideAfterPickUp()
    {
        //Debug.Log("hide");
        //gameObject.GetComponentInChildren<Renderer>().enabled = false;
        gameObject.SetActive(false);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
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

        if (keyData != null)
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

        gameData.SaveKey(KeyName, isPickedUp, KeyName);
    }

    public bool IsPickedUp()
    {
        return isPickedUp;
    }
}
