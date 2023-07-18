using BehaviorDesigner.Runtime;
using UnityEngine;

public class MoveToAfterRespawn : MonoBehaviour
{
    public BehaviorTree BotBT;
    public GameObject Target;
    public bool MovingAfterSpawn = true;

    private void Start()
    {
        if (MovingAfterSpawn)
            StartMoving();
    }

    void StartMoving()
    {
        if (BotBT == null) return;
        if (Target == null) Target = gameObject;

        BotBT.SetVariableValue("LastSeePos", Target.transform.position);
    }
}
