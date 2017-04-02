using UnityEngine;
using System.Collections;

public class ParticleRain : MonoBehaviour
{

    //  Howling Wind
    //  ------------
    //  Start lifetime: 2.5
    //  Emit Scalar: 25000
    //  Size Scalar: 50
    //  Speed Scalar: 1100
    //  Velocity Scalar: 250
    //  Range: c3

    [SerializeField]
    private float emitScalar = 5000F;
    [SerializeField]
    private float sizeScalar = 50F;
    [SerializeField]
    private float lifetimeScalar = 500F;
    [SerializeField]
    private float speedScalar = 500F;
    [SerializeField]
    private bool useVelocity = false;
    [SerializeField]
    private float velocityScalar = 100;
    [SerializeField]
    private float timescalar = 15F;

    // Private
    private bool isRunning = true;
    private float currentRange = 0;
    PreFabManager prefabManager;
    ParticleSystem ps;
    ParticleSystem subEmit;
    AudioManager audioManager;

    // Debugging
    public float maxSpeed = 0F; 

    // Use this for initialization
    void Start () {
        prefabManager = gameObject.GetComponent<PreFabManager>();
        ps = gameObject.GetComponentInChildren<ParticleSystem>();
        subEmit = ps.subEmitters.birth0;
        audioManager = GameObject.FindObjectOfType<AudioManager>();

        // Particle scale (2 tier)
        float sceneScale = (gameObject.transform.parent == null) ? 1F : gameObject.transform.parent.transform.localScale.x;
        float gameObjectScale = gameObject.transform.localScale.x;
        ps.transform.localScale = new Vector3(sceneScale * gameObjectScale, sceneScale * gameObjectScale, sceneScale * gameObjectScale);
        subEmit.transform.localScale = new Vector3(sceneScale * gameObjectScale, sceneScale * gameObjectScale, sceneScale * gameObjectScale);

        // Set particle system emission rate;
        prefabManager.SetEmissionRate(ps, 0);
    }

    // Update is called once per frame
    void Update () {
        isRunning = prefabManager.isRunning;
        if (isRunning)
        {
            if (audioManager != null)
                currentRange = audioManager.currentRange;

            // Set particle system emission rate;
            prefabManager.SetEmissionRate(ps, currentRange * emitScalar);

            // Speed & Size
            if (currentRange * speedScalar > maxSpeed)
                maxSpeed = currentRange * speedScalar;

            ps.startSize = currentRange * sizeScalar;
            //ps.startLifetime = Mathf.Clamp(currentRange * lifetimeScalar,0,3.25F);

            if (useVelocity)
            {
                ParticleSystem.VelocityOverLifetimeModule votm = ps.velocityOverLifetime;
                ParticleSystem.MinMaxCurve velMax = new ParticleSystem.MinMaxCurve(currentRange * velocityScalar);
                votm.x = velMax;
            }
        }
        else
        {
            // Set particle system emission rate;
            prefabManager.SetEmissionRate(ps, 0);
        }
    }
}
