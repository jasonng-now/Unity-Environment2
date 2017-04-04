using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleWaterfallManager : MonoBehaviour {

    // Public
    [SerializeField]
    private float emitScalar = 5000F;
    [SerializeField]
    private float sizeScalar = 50F;
    [SerializeField]
    private float lifetimeScalar = 500F;

    // Private
    private bool isRunning;
    private float currentRange = 0;
    PreFabManager prefabManager;
    ParticleSystem[] psArray;
    AudioManager audioManager;

    // Use this for initialization
    void Start () {
        prefabManager = gameObject.GetComponent<PreFabManager>();
        psArray = gameObject.GetComponentsInChildren<ParticleSystem>();
        audioManager = GameObject.FindObjectOfType<AudioManager>();

        // Particle scale (2 tier)
        float sceneScale = (gameObject.transform.parent == null) ? 1F : gameObject.transform.parent.transform.localScale.x;
        float gameObjectScale = gameObject.transform.localScale.x;
        foreach (ParticleSystem ps in psArray)
        {
            ps.transform.localScale = new Vector3(sceneScale * gameObjectScale, sceneScale * gameObjectScale, sceneScale * gameObjectScale);
        }
    }

    // Update is called once per frame
    void Update()
    {
        isRunning = prefabManager.isRunning;
        if (isRunning)
        {
            if (audioManager != null)
                currentRange = audioManager.currentRange;

            foreach (ParticleSystem ps in psArray)
            {
                // Set particle system emission rate;
                prefabManager.SetEmissionRate(ps, currentRange * emitScalar);
                ps.startSize = currentRange * sizeScalar;
                ps.startLifetime = Mathf.Clamp(currentRange * lifetimeScalar, 0, 1F);
            }
            
        }
    }
}
