using UnityEngine;

public class AmmoPickUp : MonoBehaviour
{
    public int ShotGunAmmoAmount = 2;
    public int AkAmmoAmount = 10;


    void Start()
    {
        var keyForDoor = GetComponent<KeyForDoor>();
        if (keyForDoor != null)
            keyForDoor.OnPickUp.AddListener(PickUp);
    }

    private void PickUp(GameObject player)
    {
        if (player.TryGetComponent<PlayerInventory>(out var inventory))
        {
            inventory.Weapons[0].GetComponentInChildren<Weapon>().Magazine.IncreaseAmmo(ShotGunAmmoAmount);
            inventory.Weapons[1].GetComponentInChildren<Weapon>().Magazine.IncreaseAmmo(AkAmmoAmount);
        }
        print("Ammo");
    }
}
