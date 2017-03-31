using UnityEngine;
using System.Collections;

public class Spirograph : MonoBehaviour
{

    // R=100, r=14, d=30, a=1

    private float R = 100;
    private float r = 80;
    private float d = 15;
    private float a = 0;

    private float t = 0;
    private float x1 = 0;
    private float y1 = 0;
    private float z1 = 0;
	
    private float rMin = 5F;
    private float rMax = 15F;
    private float dMin = 15F;
    private float dMax = 45F;
    private float aMin = 5F;
    private float aMax = 10F;

    // Parameters
    private bool isRunning;
    private float currentRange;
    private float emitScalar;
    private float sizeScalar;
    private float speed;
    private float scale;

    SpirographManager parameters;
    ParticleSystem ps;

    // Use this for initialization
    void Start()
    {
        ps = gameObject.GetComponentInChildren<ParticleSystem>();

        // Get parameters from parent object
        parameters = this.GetComponentInParent<SpirographManager>();

        speed = parameters.speed;
        scale = parameters.scale;
		R = parameters.R;

		rMin = R * 0.7F;
		rMax = R * 0.9F;
		dMin = R * 0.1F;
		dMax = R * 0.3F;
		aMin = 1F;
		aMax = 3F;				
	
        r = Random.Range(rMin, rMax);
        d = Random.Range(dMin, dMax);
        a = Random.Range(aMin, aMax);
		//r = R * 0.8F;
		//d = R * 0.15F;
		//a = 1F;

        R = R / scale;
        r = r / scale;
        d = d / scale;
    }

    // Update is called once per frame
    void Update()
    {		
        isRunning = parameters.isRunning;
        if (isRunning)
        {
            R = parameters.R;
            currentRange = parameters.currentRange;
            emitScalar = parameters.emitScalar;
            sizeScalar = parameters.sizeScalar;
            speed = parameters.speed;
            scale = parameters.scale;

            x1 = X(t, R, r, d, a);
            y1 = Y(t, R, r, d, a);
            z1 = Z(t, R, r, d, a);

            // Translate to game object
            x1 += gameObject.transform.position.x;
            y1 += gameObject.transform.position.y;
            z1 += gameObject.transform.position.z;

            Vector3 pos = new Vector3(x1, y1, z1);
            ps.transform.position = pos;

            // Emission Rate
            var emission = ps.emission;
            var rate = emission.rate;
            rate.constantMax = currentRange * emitScalar;
            emission.rate = rate;

            // Speed & Size
            ps.startSize = currentRange * sizeScalar;

            t += speed;
        }
    }

    // The parametric function X(t).
    private float X(float t, float R, float r, float d, float a)
    {
        return (R - r) * Mathf.Cos(t) + d * Mathf.Cos((R - r) / r * t);
    }

    // The parametric function Y(t).
    private float Y(float t, float R, float r, float d, float a)
    {
        return ((R - r) * Mathf.Sin(t) - d * Mathf.Sin((R - r) / r * t)) * Mathf.Cos(a * t);
    }

    // The parametric function Z(t).
    private float Z(float t, float R, float r, float d, float a)
    {
        return ((R - r) * Mathf.Sin(t) - d * Mathf.Sin((R - r) / r * t)) * Mathf.Sin(a * t);
    }
}
