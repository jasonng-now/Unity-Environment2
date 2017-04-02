using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PreFabManager : MonoBehaviour
{
    [SerializeField]
    public bool isRunning = true;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetEmissionRate(ParticleSystem ps, float rateVal)
    {
        var emission = ps.emission;
        var rate = emission.rate;
        rate.constantMax = rateVal;
        emission.rate = rate;
    }
}
