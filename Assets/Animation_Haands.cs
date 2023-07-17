using Assets.Scripts.Weapon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_Haands : MonoBehaviour
{
    public Animator anim;
    public Vector3 OldPosition;
    private string currentAnim;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        OldPosition = transform.position;
        var weapon = GetComponentInChildren<MagazineWeaponAttack>();
        weapon.MagazineReloaded.AddListener(PlayReloadAnimation);
        weapon.OnAttack.AddListener(PlayFireAnimation);
    }

    void Update()
    {
        if (OldPosition == transform.position)
        {
            anim.SetInteger("Walk", 0);
        }
        else if (OldPosition != transform.position && !Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetInteger("Walk", 1);
        }
        else if (OldPosition != transform.position && Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetInteger("Walk", 2);
        }
        OldPosition = transform.position;
    }

    void PlayFireAnimation()
    {
        anim.SetTrigger("Fire");
    }

    void PlayReloadAnimation()
    {
        print("reload");
        anim.SetTrigger("Reload");
    }

    void ChangeAnimation(string NewAnim)
    {
        if (currentAnim == NewAnim) return;

        anim.Play(NewAnim);

        currentAnim = NewAnim;
    }
}
