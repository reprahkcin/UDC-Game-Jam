using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    // Keep track of all canvases
    public Canvas[] canvases;

    // Keep track of the current canvas
    public Canvas currentCanvas;

    // Keep track of the current canvas index
    public int currentCanvasIndex;

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
}
