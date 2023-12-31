using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorDesigner.Runtime.Tasks.Bots
{
    [TaskCategory("Bots")]
    public class MoveToPlayer : Action
    {
        public SharedFloat speed = 10;
        public SharedFloat angularSpeed = 120;
        public SharedFloat arriveDistance = 0.2f;
        public SharedVector3 positionTo;
        public Animator anim;
        public GameObject mech;
        public AudioSource audioSource;
        public AudioClip clip;

        protected UnityEngine.AI.NavMeshAgent navMeshAgent;

        public override void OnAwake()
        {
            navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            mech = GameObject.FindWithTag("DistBot");
            anim = mech.GetComponent<Animator>();
            audioSource = mech.GetComponents<AudioSource>()[0];
            //audioSource.clip = clip;
        }

        public override void OnStart()
        {
            navMeshAgent.speed = speed.Value;
            navMeshAgent.angularSpeed = angularSpeed.Value;
            navMeshAgent.isStopped = false;
            audioSource.Play();
            SetDestination();
        }

        public override TaskStatus OnUpdate()
        {
            if (HasArrived())
            {
                return TaskStatus.Success;
            }

            SetDestination();

            return TaskStatus.Running;
        }

        private bool SetDestination()
        {
            if (positionTo.Value != Vector3.zero)
            {
                anim.SetTrigger("Run");
                navMeshAgent.isStopped = false;
                //Debug.Log(positionTo.Value);
                return navMeshAgent.SetDestination(positionTo.Value);
            }
            return false;
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
            anim.SetTrigger("NotRun");
            audioSource.Stop();
            Stop();
        }

        public override void OnBehaviorComplete()
        {
            Stop();
        }
    }
}