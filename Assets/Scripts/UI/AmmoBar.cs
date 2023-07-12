using Assets.Scripts.Weapon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoBar : MonoBehaviour
{
    private PlayerController _player;

    public Text text;

    public void Init(PlayerController  player)
    {
    }

    public void SetValue(MagazineWeaponAttack magazine)
    {
        //Debug.Log("ammo bar set value");
        text.text = magazine.AmmoInMagazine.ToString()+" / "+magazine._MagazineCapacity;
    }

    public void UpdateWeapon(Weapon weapon) {
       // Debug.Log("UpdateWeapon");
        weapon.Magazine.AmmoAmountChanged.AddListener(SetValue);

        SetValue(weapon.Magazine);
    }
}
