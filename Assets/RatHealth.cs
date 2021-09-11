using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatHealth : MonoBehaviour
{
    // rat health bar UI element
    public GameObject healthBar;

    public void UpdateHealthBar(float health)
    {
        // Get RectTransform of health bar
        RectTransform healthBarTransform = healthBar.GetComponent<RectTransform>();

        // Set healthBarTrasform localScale to health
        healthBarTransform.localScale = new Vector3(health, 1, 1);
    }


}
