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
    [SerializeField]
    private AudioClip audioclip;

    private GameObject parent;

    ParticleSystem ps;
    AudioSource audioManagerSource = null;
    private AudioSource _mySource;		// The audiosource
    private bool localAudioSource = false;
    private float currentRange = 0;

    // Use this for initialization
    void Start () {
        ps = gameObject.GetComponentInChildren<ParticleSystem>();

        // Add some randomness
        emitScalar = Random.Range(emitScalar - 250, emitScalar + 250);
        sizeScalar = Random.Range(sizeScalar - 25, sizeScalar + 25);
        speedScalar = Random.Range(speedScalar - 250, speedScalar + 250);
        shapeScalar = Random.Range(shapeScalar - 250, shapeScalar + 250);

        float psScale = (parent == null) ? 1F : parent.transform.localScale.x;
        Vector3 psScaleVector = new Vector3(gameObject.transform.localScale.x * psScale, gameObject.transform.localScale.y * psScale, gameObject.transform.localScale.z * psScale);
        ps.transform.localScale = psScaleVector;
    }
	
	// Update is called once per frame
	void Update () {
        float[] spectrum = AudioListener.GetSpectrumData(1024, 0, FFTWindow.Hamming);

        //c1 = 64Hz
        //c3 = 256Hz
        //c4 = 513Hz
        //c5 = 1024Hz

        float c1 = (spectrum[2] + spectrum[2] + spectrum[4]);
        float c3 = (spectrum[11] + spectrum[12] + spectrum[13]);
        float c4 = (spectrum[22] + spectrum[23] + spectrum[24]);
        float c5 = (spectrum[44] + spectrum[45] + spectrum[46] + spectrum[47] + spectrum[48] + spectrum[49]);

        currentRange = c1;
        if (range == "c3")
        {
            currentRange = c3;
        }
        else if (range == "c4")
        {
            currentRange = c4;
        }
        else if (range == "c5")
        {
            currentRange = c5;
        }

        // Emission Rate
        var emission = ps.emission;
        var rate = emission.rate;
        rate.constantMax = currentRange * emitScalar;
        emission.rate = rate;

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
}
