using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emily : MonoBehaviour
{
    // ------------------------------------------------------------------------------
    // GameObjects
    // ------------------------------------------------------------------------------

    // Animator
    private Animator animator;

    //  ------------------------------------------------------------------------------
    //  Functions
    //  ------------------------------------------------------------------------------

    // Trigger Animation to Give_Item
    public void GiveItem()
    {
        //animator.SetTrigger("Give_Item");
    }

    // ------------------------------------------------------------------------------
    // Unity Methods
    // ------------------------------------------------------------------------------

    void Start()
    {
        // Get Animator componenet
        animator = GetComponent<Animator>();
    }
}
