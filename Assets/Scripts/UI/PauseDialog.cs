using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseDialog : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Show()
    {
        animator.SetBool("show", true);
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

    public void Hide()
    {
        animator.SetBool("show", false);
    }
}
