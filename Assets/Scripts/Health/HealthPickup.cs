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
        if (player.TryGetComponent<HealthComponent>(out var healthComponent))
            healthComponent.Heal(HealAmount);
    }
}
