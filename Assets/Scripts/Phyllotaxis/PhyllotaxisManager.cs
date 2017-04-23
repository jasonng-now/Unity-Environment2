using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhyllotaxisManager : MonoBehaviour {

    public GameObject preFab;
    GameObject[] allBoids;

    public int n = 100;
    public float c = 3;

    // Use this for initialization
    void Start()
    {
        allBoids = new GameObject[n];
        for (int i = 0; i < n; i++)
        {
            //float a = i * radians(137.5);
            float a = i * (Mathf.PI / 180 * 137.5F);
            float r = c * Mathf.Sqrt(i);
            float x = r * Mathf.Cos(a);
            float z = r * Mathf.Sin(a);

            Vector3 pos = new Vector3(x, 2.5F, z);

            allBoids[i] = (GameObject)Instantiate(preFab, pos, Quaternion.identity);
            allBoids[i].transform.parent = gameObject.transform;

            Vector3 newDir = Vector3.RotateTowards(pos, new Vector3(0, 0, 0), 0, 0);
            Debug.DrawRay(allBoids[i].transform.position, newDir, Color.red);
            allBoids[i].transform.rotation = Quaternion.LookRotation(newDir);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
