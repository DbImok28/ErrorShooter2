using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KeyData
{
    public KeyData(string id, bool isPickedUp, string name)
    {
        this.id = id;
        this.isPickedUp = isPickedUp;
        this.name = name;

    }

    public string id;
    public bool isPickedUp;
    public string name;
}
