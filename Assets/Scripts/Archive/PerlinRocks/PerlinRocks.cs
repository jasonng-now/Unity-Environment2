using UnityEngine;
using System.Collections;

public class PerlinRocks : MonoBehaviour {

    public float interval = 0.005F;
    public float scalar = 0.05F;

    public GameObject[] preFab;
    private float radius = 0.5F;
    private NewObj[] allBoids;

    // Use this for initialization
    void Start () {
        //int numItems = Random.Range(1, 5);
        int numItems = 6;
        allBoids = new NewObj[numItems];

        for (int i = 0; i < numItems; i++)
        {
            NewObj no = new NewObj();
            no.perlinnoise = i * 1000;
            no.interval = interval;
            no.scalar = scalar;

            GameObject currentObject = preFab[Random.Range(0, preFab.Length-1)];
            // Position
            float angle = i * (360 / numItems);
            float x = (float)(radius * Mathf.Sin(angle * Mathf.PI / 180F));
            float y = 0;
            float z = (float)(radius * Mathf.Cos(angle * Mathf.PI / 180F));
            Vector3 pos = new Vector3(gameObject.transform.position.x + x, y, gameObject.transform.position.z + z);

            // Rotation
            Vector3 rotation = new Vector3(currentObject.transform.localRotation.x, Random.Range(0, 360), currentObject.transform.localRotation.z);

            // Scale
            //float scaleValue = Random.Range(0.5F, 1.5F);
            float scaleValue = 0.25F;
            currentObject.transform.localScale = new Vector3(scaleValue, scaleValue, scaleValue);


            no.gameObject = (GameObject)Instantiate(currentObject, pos, Quaternion.Euler(rotation));
            allBoids[i] = no;
            allBoids[i].gameObject.transform.parent = gameObject.transform;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        for (int i = 0; i < allBoids.Length; i++)
        {
            float perlinVal = Mathf.PerlinNoise(allBoids[i].perlinnoise, 0) * allBoids[i].scalar - 0.5F;
            Vector3 pos = new Vector3(allBoids[i].gameObject.transform.position.x, perlinVal, allBoids[i].gameObject.transform.position.z);
            allBoids[i].gameObject.transform.position = pos;
            allBoids[i].perlinnoise += allBoids[i].interval;
        }
    }

    public class NewObj
    {
        public GameObject gameObject { get; set; }
        public float perlinnoise { get; set; }
        public float interval { get; set; }
        public float scalar { get; set; }
    }
}
