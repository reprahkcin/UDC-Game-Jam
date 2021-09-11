using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emily : MonoBehaviour
{
    // Singleton
    public static Emily instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    // Trigger Animation to Give_Item
    public void GiveItem()
    {
        GetComponent<Animator>().SetTrigger("Give_Item");
    }
}
