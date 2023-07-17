using System.Collections;
using System.Collections.Generic;
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
        var inventory = player.GetComponent<PlayerInventory>();
        if (inventory != null)
        {
            inventory.Weapons[0].GetComponentInChildren<Weapon>().Magazine.AmmoAmount += AkAmmoAmount;
            inventory.Weapons[1].GetComponentInChildren<Weapon>().Magazine.AmmoAmount += ShotGunAmmoAmount;
        }
    }
}
