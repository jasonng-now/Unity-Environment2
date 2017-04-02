using UnityEngine;
using System.Collections;

public class SpirographManager : MonoBehaviour
{
    [SerializeField]
    public float speed = 0;
    [SerializeField]
    public float scale = 3F;
    [SerializeField]
    public float speedScalar = 50;
    [SerializeField]
    public float emitScalar = 5000F;
    [SerializeField]
    public float sizeScalar = 50F;

    // Private
    public bool isRunning = true;
    public float currentRange = 0;
    public float sceneScale;
    AudioManager audioManager;

    // Use this for initialization
    void Start()
    {
        // Particle scale
        sceneScale = (gameObject.transform.parent == null) ? 1F : gameObject.transform.parent.transform.localScale.x;
        audioManager = GameObject.FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        if (audioManager != null)
        {
            currentRange = audioManager.currentRange;
            speed = currentRange * speedScalar;
        }
    }
}
