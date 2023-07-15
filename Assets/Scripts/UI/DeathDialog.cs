using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathDialog : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Show() {
        animator.SetBool("show", true);
    }

   
    public void Hide()
    {
        animator.SetBool("show", false);
    }

    public void OnEnable()
    {
        animator = GetComponent<Animator>();

        Show();
    }

    public void OnDisable()
    {
        Hide();
    }
}
