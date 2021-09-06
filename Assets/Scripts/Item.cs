using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    // ---------------------------------------------------
    // Item Variables
    // ---------------------------------------------------
    private int healthBuff = 15;

    // -----------------------------------------------------
    // Getters
    // -----------------------------------------------------
    public int getHealthBuff()
    {
        return healthBuff;
    }

}
