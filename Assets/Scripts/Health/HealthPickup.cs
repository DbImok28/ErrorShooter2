using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public float HealAmount = 10;

    void Start()
    {
        var keyForDoor = GetComponent<KeyForDoor>();
        if (keyForDoor != null)
            keyForDoor.OnPickUp.AddListener(PickUp);
    }

    private void PickUp(GameObject player)
    {
        var healthComponent = player.GetComponent<HealthComponent>();
        if (healthComponent != null)
            healthComponent.Heal(HealAmount);
    }
}
