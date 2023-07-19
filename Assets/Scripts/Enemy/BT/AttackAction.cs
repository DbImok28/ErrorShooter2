using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Assets.Scripts.Weapon;

public class AttackAction : Action
{
    public SharedGameObject Target;
    public Weapon WeaponToAttack;
    public Animator anim;
    public GameObject mech;

    public override void OnStart()
    {
        mech = GameObject.FindWithTag("DistBot");
        anim = mech.GetComponent<Animator>();
        if (WeaponToAttack == null)
            WeaponToAttack = gameObject.GetComponentInChildren<Weapon>();
        anim.SetTrigger("Shoot");
        WeaponToAttack.Release();
    }

    public override TaskStatus OnUpdate()
    {
        if (Target.Value == null || WeaponToAttack == null)
            return TaskStatus.Failure;

        Vector3 targetPostition = new Vector3(Target.Value.transform.position.x, transform.position.y, Target.Value.transform.position.z);
        transform.LookAt(targetPostition);

        WeaponToAttack.Press();
        Debug.Log("Attack");
        return TaskStatus.Success;
    }

    public override void OnEnd()
    {
        anim.SetTrigger("NotShoot");
        WeaponToAttack.Release();
    }
}