using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Bots
{
    [TaskCategory("Bots")]
    public class Seek : Action
    {
        public SharedFloat speed = 10;
        public SharedFloat angularSpeed = 120;
        public SharedFloat arriveDistance = 0.2f;
        public SharedGameObject target;
        public SharedVector3 targetPosition;

        protected UnityEngine.AI.NavMeshAgent navMeshAgent;

        public override void OnAwake()
        {
            navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        }

        public override void OnStart()
        {
            navMeshAgent.speed = speed.Value;
            navMeshAgent.angularSpeed = angularSpeed.Value;
            navMeshAgent.isStopped = false;
            SetDestination(Target());
        }

        public override TaskStatus OnUpdate()
        {
            if (HasArrived())
            {
                return TaskStatus.Success;
            }

            SetDestination(Target());

            return TaskStatus.Running;
        }

        private Vector3 Target()
        {
            if (target.Value != null)
            {
                return target.Value.transform.position;
            }
            return targetPosition.Value;
        }

        private bool SetDestination(Vector3 destination)
        {
            navMeshAgent.isStopped = false;
            return navMeshAgent.SetDestination(destination);
        }

        private bool HasArrived()
        {
            float remainingDistance;
            if (navMeshAgent.pathPending)
            {
                remainingDistance = float.PositiveInfinity;
            }
            else
            {
                remainingDistance = navMeshAgent.remainingDistance;
            }

            return remainingDistance <= arriveDistance.Value;
        }

        private void Stop()
        {
            if (navMeshAgent.hasPath)
            {
                navMeshAgent.isStopped = true;
            }
        }

        public override void OnEnd()
        {
            Stop();
        }

        public override void OnBehaviorComplete()
        {
            Stop();
        }
    }
}