using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    // Emily Icons in order
    public GameObject[] emilyIcons;

    // HotDog details
    public GameObject hotDogDetails;

    // RawDog details
    public GameObject rawDogDetails;

    // Pickle details
    public GameObject pickleDetails;

    // Wave Number Canvas
    public GameObject waveNumberCanvas;

    // Wave Number Text
    public TextMeshProUGUI waveNumberText;

    // Game Over Text
    public TextMeshProUGUI gameOverText;

    public GameObject introCanvas;


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




    // ------------------------------------------------------------
    // Update UI
    // ------------------------------------------------------------

    public void UpdateHealth()
    {
        // Get Health from Player
        float playerHealth = Player.instance.GetComponent<Player>().GetHealth();


        // Apply health ratio to local scale of HealthBarForeground
        healthbarForeground.GetComponent<RectTransform>().localScale = new Vector3(playerHealth, 1, 1);
    }

    public void UpdateScore()
    {
        // Get Score from GameManager instance
        int score = GameManager.instance.GetComponent<GameManager>().GetScore();

        // Update the score UI
        scoreText.text = "Score: " + score;
    }


    public void UpdateEmilyIcons()
    {
        // Get help requests from Player
        int helpReuests = Player.instance.GetHelpRequests();

        // Update Emily Icons
        for (int i = 0; i < emilyIcons.Length; i++)
        {
            if (i < helpReuests)
            {
                emilyIcons[i].SetActive(true);
            }
            else
            {
                emilyIcons[i].SetActive(false);
            }
        }
    }

    public void UpdateWaveNumberText()
    {
        // Get wave number from GameManager instance
        int waveNumber = GameManager.instance.GetComponent<GameManager>().GetWaveNumber();

        // Update the wave number UI
        waveNumberText.text = "Wave: " + waveNumber;
    }

    public void ActivateHotDogDetails()
    {

        hotDogDetails.SetActive(true);
        StartCoroutine(ItemDetailsTimer(5f));


    }

    public void ActivateRawDogDetails()
    {

        rawDogDetails.SetActive(true);
        StartCoroutine(ItemDetailsTimer(5f));

    }

    public void ActivatePickleDetails()
    {
        pickleDetails.SetActive(true);
        StartCoroutine(ItemDetailsTimer(5f));
    }

    // Item Details Timer
    IEnumerator ItemDetailsTimer(float time)
    {
        yield return new WaitForSeconds(time);
        // Turn off details
        hotDogDetails.SetActive(false);
        rawDogDetails.SetActive(false);
        pickleDetails.SetActive(false);

        //reset stats if needed
        Player.instance.SetSpeed(5f);
        Player.instance.SetAttackDamage(15);
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

        // Wait 20 seconds before starting the game
        StartCoroutine(StartGameTimer(21.0f));
    }

    IEnumerator StartGameTimer(float time)
    {
        yield return new WaitForSeconds(time);
        // Start the game

        // Turn off the intro canvas
        introCanvas.SetActive(false);

        // Turn on theme music
        SoundManager.instance.PlayTheme2();

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

        // Update Emily Icons
        UpdateEmilyIcons();

        // Update Wave Number
        UpdateWaveNumberText();
    }
}
