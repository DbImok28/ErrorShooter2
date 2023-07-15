using System;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyInterface : MonoBehaviour, IPauseHandler
{
    [SerializeField] public NavMeshAgent agent;
    [SerializeField] public Transform target = null;

    [SerializeField] public float distance = 20f;
    [SerializeField] public float distanceForAttake = 20f;
    [SerializeField] public float distanceForFastAttake = 3f;

    [SerializeField] private HealthComponent EnemyHP;

    [SerializeField] private RaycastHit raycastHit;

    [SerializeField] public Transform[] points;
    [SerializeField] private int destPoint = 0;

    public float rotationSpeed = 0.2f;

    private float spawnRate = 2f;
    float nextSpawn = 1.5f;

    public bool IsRunAway;

    public Weapon weapon;

    private bool isPaused;

    private string id;



    private void Start()
    {
        //HPBar.value = health;
        if (agent == null)
            if (!TryGetComponent(out agent))
                print(name + " needs a navmesh agent!");

        IsRunAway = false;
        EnemyHP = gameObject.GetComponent<HealthComponent>();
        EnemyHP.OnDie.AddListener(EnemyDie);

        weapon = gameObject.GetComponentInChildren<Weapon>();
        if (gameObject.tag == "MeleeBot")
            GotoNextPoint();

        
    }

    private string GenerateId()
    {
        return Guid.NewGuid().ToString();
    }

    public string GetId()
    {
        return id;
    }

    private void Awake()
    {
        id = GenerateId();
        agent = GetComponent<NavMeshAgent>();
    }

    public bool IsViewTarget()
    {

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            if (hit.collider.tag == "Player")
                return true;
        }
        return false;
    }

    public void EnemyWalk(Vector3 pos)
    {
        if (isPaused)
            return;

        agent.Resume();
        agent.SetDestination(pos);
    }

    public void RotateToTarget()
    {
        if (isPaused)
            return;

        //transform.LookAt(new Vector3(target.position.x,target.position.y+1.5f,target.position.z));
        //smooth rotate
        var targetRotation = Quaternion.LookRotation(new Vector3(target.position.x, target.position.y + 1.5f, target.position.z) - transform.position, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed);
    }

    public bool IsPointsExist()
    {
        if (points.Length == 0)
            return false;
        return true;
    }

    public void GotoNextPoint()
    {
        if (isPaused)
            return;

        if (IsPointsExist())
        {
            EnemyWalk(points[destPoint].position);
            destPoint = (destPoint + 1) % points.Length;
        }
    }

    public void EnemyRunAway()
    {
        if (isPaused)
            return;

        IsRunAway = true;
        //run away from player
        //Vector3 pos = new Vector3(Random.Range(-transform.position.x -10f, transform.position.x + 10f), 0, Random.Range(-transform.position.z + 10f, transform.position.z + 10f));
        //EnemyWalk(pos);
        if (IsPointsExist())
        {
            int k = 0;
            float MinDistanceBetweenEnemyAndPoint = Vector3.Distance(transform.position, points[0].position);
            for (int i = 1; i < points.Length; i++)
            {
                float distanceBetweenEnemyAndPoint = Vector3.Distance(transform.position, points[i].position);
                if (distanceBetweenEnemyAndPoint < MinDistanceBetweenEnemyAndPoint && CalculateDistanceBetweenPlayerAndPoints(i) > distanceForAttake)
                {
                    MinDistanceBetweenEnemyAndPoint = distanceBetweenEnemyAndPoint;
                    k = i;
                }
            }
            EnemyWalk(points[k].position);
        }
    }

    public float CalculateDistanceBetweenPlayerAndPoints(int i)
    {
        return (Vector3.Distance(transform.position, points[i].position));
    }

    public void ResetIsRunAway()
    {
        IsRunAway = false;
    }

    public void EnemyAttack()
    {
        if (isPaused)
            return;

        agent.Stop();

        // method when Enemy ready give damage to Player with reload attake
        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;
            //gameObject.GetComponentInChildren<Animator>().Play("testAttakeMeleeBot");
            weapon.Press();
            weapon.Release();
        }
    }

    public void EnemyDie(GameObject gameObject)
    {
        Destroy(gameObject);
    }

    public void SetPaused(bool isPaused)
    {
        this.isPaused = isPaused;
    }

    
}
