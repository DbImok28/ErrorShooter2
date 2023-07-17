using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSound : MonoBehaviour
{
    public AudioClip shoot;

    private AudioClip reload;
    

    private AudioSource source;

    private Weapon weapon;

    public void Start()
    {
        source = GetComponent<AudioSource>();

        Init();

        reload=Resources.Load<AudioClip>("Sounds/weapon/weapon_reload");

        weapon.TriggerPressed.AddListener(Shoot);
        weapon.Magazine.MagazineReloaded.AddListener(Reload);


    }

    public void Init()
    {
        weapon = GetComponent<Weapon>();
    }

    public void Shoot()
    {
        source.PlayOneShot(shoot);
    }

    public void Reload()
    {
        source.PlayOneShot(reload);
    }



}
