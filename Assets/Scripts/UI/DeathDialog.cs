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
        animator.SetBool("isDead", true);
    }

    public void Hide()
    {
        animator.SetBool("isDead", false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
