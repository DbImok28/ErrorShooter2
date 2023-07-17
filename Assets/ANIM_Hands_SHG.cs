using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ANIM_Hands_SHG : MonoBehaviour
{
    public Animator anim;
    public Vector3 OldPosition;
    private string currentAnim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        OldPosition = transform.position;
    }

    // Update is called once per frame
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

        if (Input.GetKeyDown(KeyCode.R))
        {
            anim.SetTrigger("Reload");
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            anim.SetTrigger("Fire");
        }
        OldPosition = transform.position;


    }
}
