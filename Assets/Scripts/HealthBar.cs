using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    // -------------------------------------------------------
    // GameObject Connections
    // -------------------------------------------------------
    public GameObject healthBar;

    // -------------------------------------------------------
    // Unity Methods
    // -------------------------------------------------------
    // Update Health Bar
    public void UpdateHealthBar(float health)
    {
        // Get RectTransform
        RectTransform rectTransform = healthBar.GetComponent<RectTransform>();

        // Set Health Bar Size scale
        rectTransform.localScale = new Vector3(health, 1, 1);
    }

}
