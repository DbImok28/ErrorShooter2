﻿using System.Threading.Tasks;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Bots
{
    [TaskCategory("Bots")]
    public class CanSeeObject : Conditional
    {
        public SharedGameObject targetObject;
        public SharedFloat fieldOfViewAngle = 90;
        public SharedFloat viewDistance = 1000;
        public SharedFloat nearViewDistance = 5;
        public SharedGameObject returnedObject;
        public SharedVector3 lastSeePosition;

        public override void OnStart()
        {
            targetObject = BehaviorTree.FindObjectOfType<PlayerController>().gameObject;
        }

        public override TaskStatus OnUpdate()
        {
            returnedObject.Value = WithinSight(targetObject.Value, fieldOfViewAngle.Value, viewDistance.Value);
            if (returnedObject.Value != null)
            {
                lastSeePosition.Value = returnedObject.Value.transform.position;
                return TaskStatus.Success;
            }
            return TaskStatus.Failure;
        }

        private GameObject WithinSight(GameObject targetObject, float fieldOfViewAngle, float viewDistance)
        {
            if (targetObject == null)
            {
                return null;
            }

            var direction = targetObject.transform.position - transform.position;
            direction.y = 0;
            var angle = Vector3.Angle(direction, transform.forward);

            if ((direction.magnitude < viewDistance && angle < fieldOfViewAngle * 0.5f) || direction.magnitude < nearViewDistance.Value)
            {
                if (LineOfSight(targetObject))
                {
                    return targetObject;
                }
            }
            return null;
        }

        private bool LineOfSight(GameObject targetObject)
        {
            RaycastHit hit;
            if (Physics.Linecast(transform.position, targetObject.transform.position, out hit))
            {
                if (hit.transform.IsChildOf(targetObject.transform) || targetObject.transform.IsChildOf(hit.transform))
                {
                    return true;
                }
            }
            return false;
        }

        public override void OnDrawGizmos()
        {
#if UNITY_EDITOR
            var oldColor = UnityEditor.Handles.color;
            var color = Color.yellow;
            color.a = 0.1f;
            UnityEditor.Handles.color = color;

            var halfFOV = fieldOfViewAngle.Value * 0.5f;
            var beginDirection = Quaternion.AngleAxis(-halfFOV, Vector3.up) * Owner.transform.forward;
            UnityEditor.Handles.DrawSolidArc(Owner.transform.position, Owner.transform.up, beginDirection, fieldOfViewAngle.Value, viewDistance.Value);

            UnityEditor.Handles.color = oldColor;
#endif
        }
    }
}