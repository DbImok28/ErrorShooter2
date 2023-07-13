using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour, ISaveable
{
    public float lateralSensitivity;
    public float verticalSensitivity;
    public float maxPitchAngle;
    public float minPitchAngle;
    public void LoadData(GameData gameData)
    {
        lateralSensitivity = gameData.lateralSensitivity;
        verticalSensitivity = gameData.verticalSensitivity;
        maxPitchAngle = gameData.maxPitchAngle;
        minPitchAngle = gameData.minPitchAngle;

        //Debug.Log($"load settings {gameData.lateralSensitivity}");
    }

    public void SaveData(ref GameData gameData)
    {
        gameData.lateralSensitivity=lateralSensitivity;
        gameData.verticalSensitivity=verticalSensitivity;
        gameData.maxPitchAngle=maxPitchAngle;
        gameData.minPitchAngle=minPitchAngle;

       // Debug.Log($"save settings {gameData.lateralSensitivity}");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
