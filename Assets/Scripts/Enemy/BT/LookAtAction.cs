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

        public override void OnStart()
        {
        }

        public static bool isApproximate(Quaternion q1, Quaternion q2, float precision)
        {
            return Mathf.Abs(Quaternion.Dot(q1, q2)) >= 1 - precision;
        }

        public override TaskStatus OnUpdate()
        {
            if (TargetToLook.Value != null)
            {
                Vector3 targetPostition = new Vector3(TargetToLook.Value.transform.position.x,
                transform.position.y,
                                       TargetToLook.Value.transform.position.z);
                transform.LookAt(targetPostition);

                return TaskStatus.Success;
            }
            return TaskStatus.Failure;
        }
    }
}