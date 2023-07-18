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
        PlayerInventory inventory = player.GetComponentInChildren<PlayerEnvironmentInteraction>().GetInventory();
        inventory.AddKey(this.gameObject);

        HideAfterPickUp();
    }

    public void HideAfterPickUp()
    {
        gameObject.SetActive(false);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void LoadData(GameData gameData)
    {
        KeyData keyData = gameData.LoadKey(KeyName);

        if (keyData != null)
        {
            isPickedUp = keyData.isPickedUp;
            KeyName = keyData.name;

            if (isPickedUp)
                Hide();
        }


    }

    public void SaveData(ref GameData gameData)
    {
        gameData.SaveKey(KeyName, isPickedUp, KeyName);
    }

    public bool IsPickedUp()
    {
        return isPickedUp;
    }
}
