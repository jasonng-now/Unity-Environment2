using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioRocks : MonoBehaviour {

    [SerializeField]
    private float sizeScalar = 50F;

    private GameObject parent;
    ParticleSystem[] psArray;
    private string range;
    private float currentRange = 0;


    // Use this for initialization
    void Start () {
        psArray = gameObject.GetComponentsInChildren<ParticleSystem>();
        range = "c3";

        foreach (ParticleSystem ps in psArray)
        {

            float sceneScale = (gameObject.transform.parent.transform.parent == null) ? 1F : gameObject.transform.parent.transform.parent.transform.localScale.x;
            float prefabScale = gameObject.transform.localScale.x;
            float parentScale = ps.transform.parent.localScale.x;
            Vector3 psScaleVector = new Vector3(sceneScale * prefabScale * parentScale,
                                                sceneScale * prefabScale * parentScale,
                                                sceneScale * prefabScale * parentScale);
            ps.transform.localScale = psScaleVector;
        }
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

        foreach (ParticleSystem ps in psArray)
        {
            // Speed
            ps.startSize = Mathf.Clamp(currentRange * sizeScalar, 0F, 0.5F);
        }
    }
}
