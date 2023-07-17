using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySound : MonoBehaviour
{
    private AudioClip damage;
    private AudioClip death;
    private AudioClip detected;

    public AudioClip shoot;
    public AudioClip move;

    private AudioSource source;

    private HealthComponent health;

    public void Init()
    {
        health = GetComponent<HealthComponent>();

        damage = Resources.Load<AudioClip>("Sounds/Bots/detected_player");
        death = Resources.Load<AudioClip>("Sounds/Bots/dead");
        detected = Resources.Load<AudioClip>("Sounds/Bots/detected");

    }

    public void Start()
    {
        Init();

        source = GetComponent<AudioSource>();

        health.OnTakeDamage.AddListener(PlayDamage);
        health.OnDie.AddListener(PlayDeath);
        //ѕодписатьс€ на событие "враг заметил игрока"
    }

    public void PlayDamage(HealthComponent _health, float _damage)
    {
        Debug.Log("звук дамага врага");
        source.PlayOneShot(damage);
    }

    public void PlayDetected()
    {
        Debug.Log("звук замечани€");
        source.PlayOneShot(detected);
    }

    public void PlayDeath(GameObject _go)
    {
        Debug.Log("звук смерти врага");
        source.PlayOneShot(death);
    }

    public void PlayMove()
    {
        if (move)
        {
            source.PlayOneShot(move);
        }
        
    }

    public void PlayShoot()
    {
        if (shoot)
        {
            source.PlayOneShot(shoot);

        }
        
    }
}
