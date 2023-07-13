using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using static UnityEngine.GraphicsBuffer;

namespace BehaviorDesigner.Runtime.Tasks.Bots
{
    [TaskCategory("Bots")]
    public class LookAtAction : Action
    {
        public SharedGameObject TargetToLook;

        public override TaskStatus OnUpdate()
        {
            if (TargetToLook.Value != null)
            {
                Vector3 targetPostition = new Vector3(TargetToLook.Value.transform.position.x,
                    transform.position.y, TargetToLook.Value.transform.position.z);
                transform.LookAt(targetPostition);
                return TaskStatus.Success;
            }
            return TaskStatus.Failure;
        }
    }
}