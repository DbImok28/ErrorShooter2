using UnityEngine;
using UnityEngine.Events;

public class AmmoPickUp : MonoBehaviour,IPickableItem
{
    public int ShotGunAmmoAmount = 2;
    public int AkAmmoAmount = 10;

    public UnityEvent<GameObject> OnAmmoPickUp;
    public UnityEvent<GameObject> OnPickUp => OnAmmoPickUp;

    private bool isPickedUp;

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
        if (player.TryGetComponent<PlayerInventory>(out var inventory))
        {
            inventory.Weapons[0].GetComponentInChildren<Weapon>().Magazine.IncreaseAmmo(ShotGunAmmoAmount);
            inventory.Weapons[1].GetComponentInChildren<Weapon>().Magazine.IncreaseAmmo(AkAmmoAmount);

            Debug.Log("ammmo pick up");

            isPickedUp = true;

            HideAfterPickUp();
        }
        print("Ammo");
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
