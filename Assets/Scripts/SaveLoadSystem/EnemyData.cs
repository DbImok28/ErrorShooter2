using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyData
{
    public EnemyData(float enemyX, float enemyY, float enemyZ, float health)
    {
        this.enemyX = enemyX;
        this.enemyY = enemyY;
        this.enemyZ = enemyZ;

        this.health = health;

    }

    public float enemyX;
    public float enemyY;
    public float enemyZ;

    public float health;

}
