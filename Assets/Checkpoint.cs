using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    public AudioClip save_sound;

    private AudioSource source;



    public void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
    }
    public void Save()
    {
        SaveLoadManager.instance.SaveGame();

        source.PlayOneShot(save_sound);
    }
}
