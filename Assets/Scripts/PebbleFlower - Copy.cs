using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class PebbleFlower2 : MonoBehaviour {

    public UnityEvent OnMouseEnterEvent;
    public UnityEvent OnMouseExitEvent;

    ParticleSystem ps;
    Light lt;

    // Use this for initialization
    void Start () {
        ps = gameObject.GetComponentInChildren<ParticleSystem>();
        lt = gameObject.GetComponentInChildren<Light>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseEnter()
    {
        OnMouseEnterEvent.Invoke();
    }

    void OnMouseExit()
    {
        OnMouseExitEvent.Invoke();
    }

    public void MouseEnter()
    {
        //Debug.Log("ENTER");
        var emission = ps.emission;
        var rate = emission.rate;
        rate.constantMax = 10F;
        emission.rate = rate;

        lt.intensity = 5F;
    }

    public void MouseExit()
    {
        //Debug.Log("EXIT");
        var emission = ps.emission;
        var rate = emission.rate;
        rate.constantMax = 0F;
        emission.rate = rate;

        lt.intensity = 0;
    }
}
