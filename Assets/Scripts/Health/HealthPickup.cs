using UnityEngine;
using UnityEngine.Events;

public class HealthPickup : MonoBehaviour, IPickableItem
{
    public float HealAmount = 10;

    private bool isPickedUp;

    public UnityEvent<GameObject> OnPickUp => throw new System.NotImplementedException();

    void Start()
    {
        /*
        var keyForDoor = GetComponent<KeyForDoor>();
        if (keyForDoor != null)
            keyForDoor.OnPickUp.AddListener(PickUp);
        */
    }

    public void PickUp(GameObject player)
    {
        if (player.TryGetComponent<HealthComponent>(out var healthComponent))
            healthComponent.Heal(HealAmount);

        isPickedUp = true;

        Debug.Log("health pick up");

        HideAfterPickUp();
    }


    public bool IsPickedUp()
    {
        return isPickedUp;
    }

    public void HideAfterPickUp()
    {
        Destroy(gameObject);
    }
}
