using UnityEngine;
using System.Collections;

public class Spirograph : MonoBehaviour
{

    // R=100, r=7, d=30, a=5, scale=3

    private float R = 100F;
    private float r = 7F;
    private float d = 30F;
    private float a = 5F;

    private float t = 0;
    private float x1 = 0;
    private float y1 = 0;
    private float z1 = 0;

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
        parameters = this.GetComponentInParent<SpirographManager>();

        float range = 5;
        r = Random.Range(r - range, r + range);
        d = Random.Range(d - range, d + range);
        a = Random.Range(a - range, a + range);

        scale = parameters.scale;
        R = R / scale;
        r = r / scale;
        d = d / scale;

        // Particle scale (2 tier)
        float sceneScale = parameters.sceneScale;
        float gameObjectScale = gameObject.transform.localScale.x;
        ps.transform.localScale = new Vector3(sceneScale * gameObjectScale, sceneScale * gameObjectScale, sceneScale * gameObjectScale);
    }

    // Update is called once per frame
    void Update()
    {
        //R = 100 / 3F;
        //r = 7 / 3F;
        //d = 30 / 3F;
        //a = 5F;
        isRunning = parameters.isRunning;
        if (isRunning)
        {
            //R = parameters.R;
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
