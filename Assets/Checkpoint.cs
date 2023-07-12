using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    // Start is called before the first frame update
    public void Save()
    {
        Debug.Log("checkpoint savegame");

        SaveLoadManager.instance.SaveGame();
    }
}
