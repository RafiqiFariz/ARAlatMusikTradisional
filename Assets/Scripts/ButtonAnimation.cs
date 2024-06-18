using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAnimation : MonoBehaviour
{
    public GameObject playButton;
    public GameObject quitButton;
    public float animationTime = 1f; // Duration of the animation
    public float targetY = 1f; // Target Y position for the animation

    // Start is called before the first frame update
    void Start()
    {
        // Initial position for play button
        float initialPlayButtonY = playButton.transform.position.y;

        // Move play button from the bottom
        playButton.transform.position = new Vector3(playButton.transform.position.x, -targetY, playButton.transform.position.z);
        LeanTween.moveY(playButton, initialPlayButtonY, animationTime);

        // Initial position for quit button
        float initialQuitButtonY = quitButton.transform.position.y;

        // Move quit button from the bottom
        quitButton.transform.position = new Vector3(quitButton.transform.position.x, -targetY, quitButton.transform.position.z);
        LeanTween.moveY(quitButton, initialQuitButtonY, animationTime);
    }
}