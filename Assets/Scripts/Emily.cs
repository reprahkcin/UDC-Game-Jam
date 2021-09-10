using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emily : MonoBehaviour
{
    // Trigger Animation to Give_Item
    public void GiveItem()
    {
        GetComponent<Animator>().SetTrigger("Give_Item");
    }
}
