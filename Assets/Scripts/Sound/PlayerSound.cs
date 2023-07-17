using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    

    public AudioClip damage;
    public AudioClip death;
    public AudioClip swap_weapon;

    private AudioSource source;

    private HealthComponent health;
    private PlayerEnvironmentInteraction pei;
    private PlayerController player;

    public void Init() {
        health = FindObjectsOfType<PlayerController>()[0].gameObject.GetComponent<HealthComponent>();
        pei = FindObjectsOfType<PlayerController>()[0].gameObject.GetComponent<PlayerEnvironmentInteraction>();
        player = FindObjectOfType<PlayerController>();
    }

    public void Start()
    {
        Init();

        source = GetComponent<AudioSource>();

        health.OnTakeDamage.AddListener(PlayDamage);
        health.OnDie.AddListener(PlayDeath);
        player.WeaponChanged.AddListener(PlayWeaponSwap);
        
    }

    public void PlayDamage(HealthComponent _health, float _damage)
    {
        Debug.Log("звук дамага");
        source.PlayOneShot(damage);
    }

    public void PlayDeath(GameObject _go)
    {
        Debug.Log("звук смерти");
        source.PlayOneShot(death);
    }

    public void PlayWeaponSwap(Weapon w)
    {
        Debug.Log("звук смерти");
        source.PlayOneShot(swap_weapon);
    }
}
