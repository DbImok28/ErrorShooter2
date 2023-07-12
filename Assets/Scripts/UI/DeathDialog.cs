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
        Debug.Log("show");
        animator.SetBool("PlayerIsDead", true);
    }

    public void Hide()
    {
        Debug.Log("hide");
        animator.SetBool("PlayerIsDead", false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
