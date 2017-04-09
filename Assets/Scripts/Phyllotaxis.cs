using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phyllotaxis : MonoBehaviour {

    float c = 3F;
    List<Vector2> ordered = new List<Vector2>();

    public List<Vector2> GetOrderedPositions(float c, int i)
    {
        int items = i;

        for (int n = 0; n <= items; n++)
        {
            float a = n * (Mathf.PI / 180 * 137.5F);
            float r = c * Mathf.Sqrt(n);
            float x = r * Mathf.Cos(a);
            float y = r * Mathf.Sin(a);

            Vector2 pos = new Vector2(x, y);
            ordered.Add(pos);
        }

        return ordered;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
