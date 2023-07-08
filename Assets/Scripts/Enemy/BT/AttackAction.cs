using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Assets.Scripts.Weapon;

public class AttackAction : Action
{
    public SharedGameObject Target;
    public Weapon WeaponToAttack;

    public override void OnStart()
    {
        if (WeaponToAttack == null)
            WeaponToAttack = gameObject.GetComponentInChildren<Weapon>();
        WeaponToAttack.Release();
    }

    public override TaskStatus OnUpdate()
    {
        if (Target.Value == null || WeaponToAttack == null)
            return TaskStatus.Failure;

        //WeaponToAttack.Release();
        WeaponToAttack.Press();
        Debug.Log("Attack");
        return TaskStatus.Success;
        //return TaskStatus.Success;
    }

    public override void OnEnd()
    {
        WeaponToAttack.Release();
    }
}