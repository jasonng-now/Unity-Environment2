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

            Vector3 pos = new Vector3(x, 3, z);

            // Rotation
            Vector3 xaxis = new Vector3(1,0,0);

            //Vector3 difference = pos - xaxis;
            //float anglec = Vector3.Angle(Vector3.right, difference);
            float angleb = Vector3.Angle(pos, xaxis);
            Vector3 raxis = Vector3.Cross(xaxis,pos);
            Vector3 rotation = new Vector3(raxis.x, raxis.y , raxis.z);

            allBoids[i] = (GameObject)Instantiate(preFab, pos, Quaternion.identity);
            allBoids[i].transform.Rotate(rotation, angleb);
            allBoids[i].transform.parent = gameObject.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
