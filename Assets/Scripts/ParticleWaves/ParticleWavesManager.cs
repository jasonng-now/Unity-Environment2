using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ParticleWavesManager : MonoBehaviour {

    public float angle= 0;
    public float angleVel = 0.015F;

    public GameObject preFab;
    public int cols = 5;
    public int rows = 5;
    public float height = 10;
    GameObject[,] allBoids;
    public float scalar = 5F;
    public float timescalar = 5F;
    public bool move = false;

    public float sceneScale = 1F;
    public bool isRunning;
    private float currentRange = 0;
    PreFabManager prefabManager;
    AudioManager audioManager;

    // Use this for initialization
    void Start () {
        prefabManager = gameObject.GetComponent<PreFabManager>();
        audioManager = GameObject.FindObjectOfType<AudioManager>();

        allBoids = new GameObject[cols, rows];

        for (var ix = 0; ix < cols; ix++)
        {
            float x = (ix - 0.5F * Convert.ToSingle(cols) + 0.5F) * 0.6F;

            for (int iz = 0; iz < rows; iz++)
            {
                float z = (iz - 0.5F * Convert.ToSingle(rows) + 0.5F) * 0.6F;

                Vector3 pos = new Vector3(x, 0, z);
                pos = pos + gameObject.transform.position;

                GameObject go = (GameObject)Instantiate(preFab, pos, Quaternion.identity);
                go.transform.parent = gameObject.transform;
                allBoids[ix, iz] = go;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        isRunning = prefabManager.isRunning;
        if (isRunning)
        {
            if (audioManager != null)
                currentRange = audioManager.currentRange;

            //startAngle += 0.015F;
            //float angle = startAngle;

            float z1 = Map(Mathf.Sin(angle), -1, 1, -height, height);

            for (var x = 0; x < cols; x++)
            {
                for (var z = 0; z < rows; z++)
                {
                    float prevYpos = allBoids[x, z].transform.position.y;
                    float y = Mathf.Lerp(prevYpos, audioManager.spectrum[x+z] * scalar, Time.deltaTime * timescalar);

                    float posz = (move) ? z1 : allBoids[x, z].transform.position.z;
                    posz = posz + gameObject.transform.position.z;

                    Vector3 pos = new Vector3(allBoids[x, z].transform.position.x, y, posz);
                    preFab.transform.position = pos;
                    allBoids[x, z].transform.position = pos;
                }
            }

            angle += currentRange * angleVel;
        }
    }

    public float Map(float x, float in_min, float in_max, float out_min, float out_max)
    {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }
}
