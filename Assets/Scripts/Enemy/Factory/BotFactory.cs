using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BotFactory
{
    public abstract GameObject FactoryMethod(float x, float y, float z, float health, Quaternion rot);
}
