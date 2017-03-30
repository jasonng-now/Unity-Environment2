using UnityEngine;
using System.Collections;

public class SpirographParam : MonoBehaviour
{
    public float speed = 0.005F;
    public float scale = 5F;
    public float R = 10F;
    public bool isRunning = true;
    public float currentRange = 0;
    AudioManager audioManager;

    // Use this for initialization
    void Start()
    {
        audioManager = GameObject.FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        if (audioManager != null)
        {
            currentRange = audioManager.currentRange;
            speed = currentRange;
        }
    }
}
