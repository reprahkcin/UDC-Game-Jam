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

    // HealthBar: This is the health bar that is displayed on the bottom and consists of a dynamic image.
    public HealthBar healthBar;

    // Player
    public GameObject player;

    // Player Script
    public Player playerScript;

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

    // Hot Dog Text
    public TextMeshProUGUI hotDogText;


    // ------------------------------------------------------------
    // Update UI
    // ------------------------------------------------------------

    public void UpdateHealth()
    {
        // Get Health from Player
        int playerHealth = playerScript.GetHealth();

        // Call function to update health bar
        healthBar.UpdateHealthBar(playerHealth);
    }

    public void UpdateScore()
    {
        // Get Score from GameManager
        int score = gameManager.GetScore();
        // Update the score UI
        scoreText.text = "Score: " + score;
    }
    public void UpdateHotDog()
    {
        // Get Hot Dogs from Player
        int hotDogs = player.GetComponent<Player>().GetHotdogs();
        // Update the hot dog UI Text
        hotDogText.text = "Hot Dogs: " + hotDogs;
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
    // Unity Methods
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

        // Get the player
        player = GameObject.FindGameObjectWithTag("Player");


    }

    void Update()
    {
        // Update the health bar
        UpdateHealth();

        // Update the score
        UpdateScore();

        // Update the hot dog count
        UpdateHotDog();
    }
}
