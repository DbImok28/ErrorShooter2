using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

public class Cardreader : MonoBehaviour
{

    public string CardreaderKeyName;

    public bool PlayerHasMatchingKey(List<string> playerkeys, out string matchingKey)
    {

        //Debug.Log("PlayerHasMatchingKey");
       
        foreach(string playerKey in playerkeys)
        {
            //Debug.Log($"{playerKey} {CardreaderKeyName}");

                if (playerKey == CardreaderKeyName)
                {
                    matchingKey = playerKey;
                    return true;
                }
                
            
        }

        matchingKey = null;
        return false;
    }
    
}
