using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    melee,
    distant
}

[System.Serializable]
public class EnemyData
{
    public EnemyData(float enemyX, float enemyY, float enemyZ, float health, EnemyType type)
    {
        this.enemyX = enemyX;
        this.enemyY = enemyY;
        this.enemyZ = enemyZ;

        this.health = health;

        this.type = type;

    }

    public float enemyX;
    public float enemyY;
    public float enemyZ;

    public float health;

    public EnemyType type;

}
