using UnityEngine;
using System.Collections;

public class ParticleWind : MonoBehaviour {

    [SerializeField]
    private  float emitScalar = 40F;
    [SerializeField]
    private string range;
    [SerializeField]
    private float sizeScalar = 50F;
    [SerializeField]
    private float speedScalar = 500F;
    [SerializeField]
    private float shapeScalar = 500F;
    [SerializeField]
    private bool useVelocity = false;
    [SerializeField]
    private float velocityScalar = 100;

    private float currentRange = 0;
    private bool isRunning = true;
    PreFabManager prefabManager;
    ParticleWindManager particleWindManager;
    ParticleSystem ps;
    AudioManager audioManager;

    // Use this for initialization
    void Start () {
        // Scale the prefab
        gameObject.transform.localScale = new Vector3(1F, 1F, 1F);

        ps = gameObject.GetComponentInChildren<ParticleSystem>();
        audioManager = GameObject.FindObjectOfType<AudioManager>();
        particleWindManager = GameObject.FindObjectOfType<ParticleWindManager>();
        prefabManager = particleWindManager.GetComponent<PreFabManager>();

        // Add some randomness
        emitScalar = Random.Range(emitScalar - 250, emitScalar + 250);
        sizeScalar = Random.Range(sizeScalar - 25, sizeScalar + 25);
        speedScalar = Random.Range(speedScalar - 250, speedScalar + 250);
        shapeScalar = Random.Range(shapeScalar - 250, shapeScalar + 250);

        // Particle scale (2 tier)
        float sceneScale = particleWindManager.sceneScale;
        float gameObjectScale = gameObject.transform.localScale.x;
        ps.transform.localScale = new Vector3(sceneScale * gameObjectScale, sceneScale * gameObjectScale, sceneScale * gameObjectScale);

        // Set particle system emission rate;
        prefabManager.SetEmissionRate(ps, 0);
    }

    // Update is called once per frame
    void Update () {
        isRunning = particleWindManager.isRunning;
        if (isRunning)
        {
            if (audioManager != null)
                currentRange = audioManager.currentRange;

            // Set particle system emission rate;
            prefabManager.SetEmissionRate(ps, currentRange * emitScalar);

            // Speed & Size
            ps.startSize = currentRange * sizeScalar;
            ps.startSpeed = currentRange * speedScalar;

            if (useVelocity)
            {
                ParticleSystem.VelocityOverLifetimeModule votm = ps.velocityOverLifetime;
                ParticleSystem.MinMaxCurve velMax = new ParticleSystem.MinMaxCurve(currentRange * velocityScalar);
                votm.x = velMax;
            }

            var shape = ps.shape;
            shape.angle = currentRange * shapeScalar;
        }
        else
        {
            // Set particle system emission rate;
            prefabManager.SetEmissionRate(ps, 0);
        }
    }
}
