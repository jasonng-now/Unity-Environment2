using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneManage : MonoBehaviour {

    // Poisson Disc variables
    public int PDWidth = 200;
    public int PDHeight = 200;
    public float PDR = 4;
    
    // Static objects
    public GameObject[] staticObjects;
    public bool showStaticObjects = true;  
    public int staticObjectItems = 10;

    // Interactice objects
    public GameObject[] interactiveObjects;
    public int interactiveObjectItems = 5;

    public GameObject[] audioObjects;
    public int audioObjectItems = 5;

    public float planeSize = 5F;

    float sceneScale = 1F;

    // Use this for initialization
    void Start () {
        sceneScale = gameObject.transform.localScale.x;
        if (showStaticObjects)
            PopulateStaticObjects();

        if (interactiveObjectItems > 0)
            PopulateInteractiveObjects();

        if (audioObjectItems > 0)
            PopulateAudioObjects();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PopulateStaticObjects()
    {
        List<Vector2> ordered = new List<Vector2>();

        PoissonDisc pd = new PoissonDisc();
        ordered = pd.GetOrderedPositions(PDWidth, PDHeight, PDR, staticObjectItems);

        for (int i = 0; i < ordered.Count; i++)
        {
            if (ordered[i] != Vector2.zero)
            {
                int randomIndex = Random.Range(0, staticObjects.Length);
                GameObject currentObject = staticObjects[randomIndex];

                // Position
                Vector3 pos = new Vector3(ordered[i].x * sceneScale, 0, ordered[i].y * sceneScale);

                // Rotation
                Vector3 rotation = new Vector3(currentObject.transform.localRotation.x, Random.Range(0, 360), currentObject.transform.localRotation.z);

                // Scale
                currentObject.transform.localScale = new Vector3(sceneScale, sceneScale, sceneScale);

                GameObject go = (GameObject)Instantiate(currentObject, pos, Quaternion.identity);
                go.transform.parent = gameObject.transform;
            }
        }
    }

    public void PopulateInteractiveObjects()
    {
        List<Vector2> ordered = new List<Vector2>();

        PoissonDisc pd = new PoissonDisc();
        ordered = pd.GetOrderedPositions(PDWidth, PDHeight, PDR, interactiveObjectItems);

        Vector3 gamepos = gameObject.transform.position;
        for (int i = 0; i < ordered.Count; i++)
        {
            if (ordered[i] != Vector2.zero)
            {
                int randomIndex = Random.Range(0, interactiveObjects.Length);
                GameObject currentObject = interactiveObjects[randomIndex];

                // Position
                Vector3 pos = new Vector3(ordered[i].x * sceneScale, 0, ordered[i].y * sceneScale);

                // Rotation
                Vector3 rotation = new Vector3(currentObject.transform.localRotation.x, Random.Range(0, 360), currentObject.transform.localRotation.z);

                // Scale
                currentObject.transform.localScale = new Vector3(sceneScale, sceneScale, sceneScale);

                GameObject go = (GameObject)Instantiate(currentObject, pos, Quaternion.identity);
                go.transform.parent = gameObject.transform;
            }
        }

        //for (int i = 0; i < interactiveObjectItems; i++)
        //{
        //    int randomIndex = Random.Range(0, interactiveObjects.Length - 1);

        //    // Position
        //    Vector3 pos = new Vector3(Random.Range(-planeSize, planeSize), 0, Random.Range(-planeSize, planeSize));
        //    GameObject go = (GameObject)Instantiate(interactiveObjects[randomIndex], pos, Quaternion.identity);
        //    go.transform.parent = gameObject.transform;
        //}
    }

    public void PopulateAudioObjects()
    {
        for (int i = 0; i < audioObjectItems; i++)
        {
            int randomIndex = Random.Range(0, audioObjects.Length - 1);

            // Position
            Vector3 pos = new Vector3(Random.Range(-planeSize, planeSize), 0, Random.Range(-planeSize, planeSize));
            GameObject go = (GameObject)Instantiate(audioObjects[randomIndex], pos, Quaternion.identity);
            go.transform.parent = gameObject.transform;
        }
    }
}
