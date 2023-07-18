using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotMeleeFactory : BotFactory
{
    public override GameObject FactoryMethod(float x, float y, float z, float health, Quaternion rot)
    {
        var prefab = Resources.Load<GameObject>("Prefabs/Bots/EnemyMelee");

        var go = GameObject.Instantiate(prefab, new Vector3(x, y, z), rot);

        go.GetComponent<HealthComponent>().CurrentHealth = health;

        return go;
    }

}
