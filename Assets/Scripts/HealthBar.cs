using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    // -------------------------------------------------------
    // GameObject Connections
    // -------------------------------------------------------
    public GameObject HealthBarObject;

    // -------------------------------------------------------
    // Methods
    // Called from Canvas Manager
    // -------------------------------------------------------


    // Update Health Bar
    public void UpdateHealthBar(int health)
    {
        // Get RectTransform of HealthBarObject
        RectTransform healthBarRectTransform = HealthBarObject.GetComponent<RectTransform>();

        // Convert health to float
        float healthFloat = health;

        // Divide health float by 100
        healthFloat = healthFloat / 100;

        // Set HealthBarObject's local scale x to health
        healthBarRectTransform.localScale = new Vector3(healthFloat, healthBarRectTransform.localScale.y, healthBarRectTransform.localScale.z);
    }

}
