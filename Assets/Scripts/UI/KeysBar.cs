using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeysBar : MonoBehaviour
{
    public Text text;

    public void Init(PlayerInventory inventory)
    {
        inventory.KeyPickedUp.AddListener(SetValue);

        SetValue(inventory.KeysNames);
    }


    public void SetValue(List<string> keysNames)
    {
        text.text = "";
        foreach(string keyName in keysNames)
        {
            text.text += keyName+"\n";
        }
    }
}
