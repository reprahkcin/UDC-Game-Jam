using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
    // create array of health bar UI images
    public GameObject[] healthBarUI;

    // Create dark red color for empty health bar
    public Color emptyColor = new Color(0.5f, 0, 0);

    // Create light red color for full health bar
    public Color fullColor = new Color(1, 0, 0);

    // Create a variable to hold the current health
    public int currentHealth = 10;

    // Create a variable to hold the max health
    public int maxHealth = 10;

    void Start()
    {
        // Apply the full color to all health bar UI images
        for (int i = 0; i < healthBarUI.Length; i++)
        {
            healthBarUI[i].GetComponent<Image>().color = fullColor;
        }
    }

    void Update()
    {
        // Update the health bar UI images based on the current health
        for (int i = 0; i < healthBarUI.Length; i++)
        {
            if (i < currentHealth)
            {
                healthBarUI[i].GetComponent<Image>().color = fullColor;
            }
            else
            {
                healthBarUI[i].GetComponent<Image>().color = emptyColor;
            }
        }
    }

    // Create a function to decrease the current health
    public void decreaseHealth(int i)
    {
        currentHealth -= i;
    }

    // Create a function to increase the current health
    public void increaseHealth(int i)
    {
        currentHealth += i;
    }
}
