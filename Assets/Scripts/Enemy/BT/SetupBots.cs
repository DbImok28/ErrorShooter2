using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;

public class SetupBots : MonoBehaviour
{
    public BehaviorTree behavior;
    public GameObject player;

    void Start()
    {
        behavior.SetVariableValue("TargetPlayer", player);
    }
}
