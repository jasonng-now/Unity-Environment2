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
    [SerializeField]
    private GameObject preFab;
    [SerializeField]
    private int itemCount;

    // Private

    private GameObject[] allBoids;
    public float sceneScale = 1F;
    public bool isRunning = true;
    PreFabManager prefabManager;

    // Use this for initialization
    void Start()
    {
        prefabManager = gameObject.GetComponent<PreFabManager>();

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
                GameObject currentObject = preFab;
                
                // Position
                Vector3 pos = new Vector3(ordered[i].x * sceneScale, Random.Range(gameObject.transform.position.y - 5, gameObject.transform.position.y + 5), ordered[i].y * sceneScale);

                // Rotation
                Vector3 rotation = new Vector3(currentObject.transform.localRotation.x, Random.Range(0, 360), currentObject.transform.localRotation.z);

                GameObject go = (GameObject)Instantiate(currentObject, pos, Quaternion.Euler(rotation));
                go.transform.parent = gameObject.transform;
                allBoids[i] = go;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        isRunning = prefabManager.isRunning;
    }
}
