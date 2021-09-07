using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    // ------------------------------------------------------------
    // GameObjects
    // ------------------------------------------------------------
    // Keep track of all canvases
    public Canvas[] canvases;

    // GameManager
    public GameManager gameManager;

    // HealthBar
    public HealthBar healthBar;

    // ------------------------------------------------------------
    // State Variables
    // ------------------------------------------------------------
    // Keep track of the current canvas
    public Canvas currentCanvas;

    // Keep track of the current canvas index
    public int currentCanvasIndex;

    // -------------------------------------------------
    // UI Connections
    // TODO: Add these in the inspector (from HUD canvas)
    // -------------------------------------------------

    // Get UI Text for score display
    public TextMeshProUGUI scoreText;

    // Get Player Health
    public int playerHealth;


    // ------------------------------------------------------------
    // Update UI
    // ------------------------------------------------------------
    void UdateUIText()
    {
        //if player exists in scene
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            // Get player
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            // Update the score UI
            scoreText.text = "Score: " + gameManager.GetComponent<GameManager>().GetScore();

            // Get player health
            playerHealth = player.GetComponent<Player>().GetHealth();
            // If player health is more than 100
            if (playerHealth > 100)
            {
                // Set player health to 100
                playerHealth = 100;
            }
            // if player health is less than 0
            if (playerHealth < 0)
            {
                // Set player health to 0
                playerHealth = 0;
            }
            // Update the health bar
            healthBar.UpdateHealthBar(playerHealth / 100f);

        }
    }

    // ------------------------------------------------------------
    // Canvas Control Functions
    // ------------------------------------------------------------
    public void NextCanvas()
    {
        // Deactivate the current canvas
        currentCanvas.gameObject.SetActive(false);

        // If current canvas index is less than the length of the canvases array
        if (currentCanvasIndex < canvases.Length - 1)
        {
            // Increment the current canvas index
            currentCanvasIndex++;
        }
        else
        {
            // Set the current canvas index to 0
            currentCanvasIndex = 0;
        }

        // Activate the next canvas
        currentCanvas = canvases[currentCanvasIndex];
        currentCanvas.gameObject.SetActive(true);
    }

    public void PreviousCanvas()
    {
        // Deactivate the current canvas
        currentCanvas.gameObject.SetActive(false);

        // If current canvas index is greater than 0
        if (currentCanvasIndex > 0)
        {
            // Decrement the current canvas index
            currentCanvasIndex--;
        }
        else
        {
            // Set the current canvas index to the length of the canvases array
            currentCanvasIndex = canvases.Length - 1;
        }

        // Activate the previous canvas
        currentCanvas = canvases[currentCanvasIndex];
        currentCanvas.gameObject.SetActive(true);
    }

    public void SetCanvas(int index)
    {
        // Deactivate the current canvas
        currentCanvas.gameObject.SetActive(false);

        // Set the current canvas index to the index
        currentCanvasIndex = index;

        // Activate the canvas at the current canvas index
        currentCanvas = canvases[currentCanvasIndex];
        currentCanvas.gameObject.SetActive(true);
    }

    // ------------------------------------------------------------
    // Unity Functions
    // ------------------------------------------------------------
    void Start()
    {
        // Deactivate all canvases
        foreach (Canvas canvas in canvases)
        {
            canvas.gameObject.SetActive(false);
        }

        // Set the current canvas to the currentCanvasIndex
        currentCanvas = canvases[currentCanvasIndex];

        // Activate the current canvas
        currentCanvas.gameObject.SetActive(true);
    }

    void Update()
    {
        // Update the UI text
        UdateUIText();
    }
}
