using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    // -----------------------------------------------------------
    // Singleton
    // -----------------------------------------------------------

    public static CanvasManager instance;

    // ------------------------------------------------------------
    // GameObjects
    // ------------------------------------------------------------

    // Keep track of all canvases
    public Canvas[] canvases;

    public GameObject healthbarForeground;

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
        int playerHealth = Player.instance.GetComponent<Player>().GetHealth();

        // Calculate ratio
        float ratio = playerHealth / 100;

        // Apply health ratio to local scale of HealthBarForeground
        //healthbarForeground.GetComponent<RectTransform>().localScale = new Vector3(ratio, 1f,1f);
    }

    public void UpdateScore()
    {
        // Get Score from GameManager instance
        int score = GameManager.instance.GetComponent<GameManager>().GetScore();

        // Update the score UI
        scoreText.text = "Score: " + score;
    }
    public void UpdateHotDog()
    {
        // Get Hot Dogs from Player
        int hotDogs = Player.instance.GetHotdogs();
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
    void Awake()
    {
        // Singleton
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

    }


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
        // Update the health bar
        UpdateHealth();

        // Update the score
        UpdateScore();

        // Update the hot dog count
        UpdateHotDog();
    }
}
