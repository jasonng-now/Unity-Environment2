using UnityEngine;
using System.Collections;

public class ParticleField : MonoBehaviour
{
    // Public
    [SerializeField]
    private float emitScalar = 5000F;
    [SerializeField]
    private float sizeScalar = 50F;
    [SerializeField]
    private float lifetimeScalar = 500F;
    [SerializeField]
    private float speedScalar = 500F;
    [SerializeField]
    private float timescalar = 15F;

    // Private
    private bool isRunning = true;
    private float currentRange = 0;
    private GameObject parent;
    ParticleSystem ps;
    AudioManager audioManager;

    // Debugging
    public float maxSpeed = 0F;
    public float maxLifetime = 0F;

    // Use this for initialization
    void Start()
    {
        ps = gameObject.GetComponentInChildren<ParticleSystem>();
        audioManager = GameObject.FindObjectOfType<AudioManager>();

        // Particle scale (2 tier)
        float sceneScale = (gameObject.transform.parent == null) ? 1F : gameObject.transform.parent.transform.localScale.x;
        float gameObjectScale = gameObject.transform.localScale.x;
        ps.transform.localScale = new Vector3(sceneScale * gameObjectScale, sceneScale * gameObjectScale, sceneScale * gameObjectScale);

        // Emission Rate
        var emission = ps.emission;
        var rate = emission.rate;
        rate.constantMax = 0;
        emission.rate = rate;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            if (audioManager != null)
                currentRange = audioManager.currentRange;

            // Emission Rate
            var emission = ps.emission;
            var rate = emission.rate;
            rate.constantMax = currentRange * emitScalar;
            emission.rate = rate;

            // Speed & Size
            if (currentRange * speedScalar > maxSpeed)
                maxSpeed = currentRange * speedScalar;

            // Lifetime
            if (currentRange * lifetimeScalar > maxLifetime)
                maxLifetime = currentRange * lifetimeScalar;

            ps.startSize = currentRange * sizeScalar;
            ps.startSpeed = currentRange * speedScalar;
            ps.startLifetime = Mathf.Clamp(currentRange * lifetimeScalar, 0, 1F);
        }
    }
}
