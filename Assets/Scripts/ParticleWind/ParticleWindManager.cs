using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParticleWindManager: MonoBehaviour
{

    // Poisson Disc variables
    [SerializeField]
    private int PDWidth = 50;
    [SerializeField]
    private int PDHeight = 50;
    [SerializeField]
    private float PDRadius = 11;

    public GameObject[] preFabObjects;
    public int itemCount;
    public float scalar = 100F;
    public float timescalar = 15F;

    private GameObject[] allBoids;
    public float sceneScale = 1F;
    public bool isRunning = true;
    private float currentRange = 0;
    private string range = "c1";

    // Use this for initialization
    void Start()
    {
        // Particle scale
        sceneScale = (gameObject.transform.parent == null) ? 1F : gameObject.transform.parent.transform.localScale.x;

        List<Vector2> ordered = new List<Vector2>();
        PoissonDisc pd = new PoissonDisc();
        ordered = pd.GetOrderedPositions(PDWidth, PDHeight, PDRadius, itemCount);
        allBoids = new GameObject[ordered.Count];

        for (int i = 0; i < ordered.Count; i++)
        {
            if (ordered[i] != Vector2.zero)
            {
                int randomIndex = Random.Range(0, preFabObjects.Length);
                GameObject currentObject = preFabObjects[randomIndex];
                

                // Position
                Vector3 pos = new Vector3(ordered[i].x * sceneScale, gameObject.transform.position.y, ordered[i].y * sceneScale);

                // Rotation
                Vector3 rotation = new Vector3(currentObject.transform.localRotation.x, Random.Range(0, 360), currentObject.transform.localRotation.z);

                // Scale
                float currentScale = 1F;
                //currentObject.transform.localScale = new Vector3(currentScale * sceneScale, currentScale * sceneScale, currentScale * sceneScale);
                //currentObject.transform.localScale = new Vector3(currentScale, currentScale, currentScale);
                GameObject go = (GameObject)Instantiate(currentObject, pos, Quaternion.Euler(rotation));
                go.transform.parent = gameObject.transform;
                allBoids[i] = go;
                //allBoids[i].transform.localScale = new Vector3(5F,5F,5F);
                //allBoids[i].transform.parent = gameObject.transform;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
