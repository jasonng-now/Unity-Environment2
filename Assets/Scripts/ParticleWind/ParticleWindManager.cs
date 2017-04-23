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
    private NewObj[] allBoids;
    public float sceneScale = 1F;
    public bool isRunning = true;
    PreFabManager prefabManager;
    private float currentRange = 0;
    public float interval = 0.005F;
    public float distanceScalar = 0.05F;

    // Use this for initialization
    void Start()
    {
        prefabManager = gameObject.GetComponent<PreFabManager>();

        // Particle scale
        sceneScale = (gameObject.transform.parent == null) ? 1F : gameObject.transform.parent.transform.localScale.x;

        // Single item, place at the game object position

        if (itemCount == 1)
        {
            allBoids = new NewObj[itemCount];
            GameObject currentObject = preFab;

            NewObj no = new NewObj();
            int i = 0;
            no.x = i * 10;
            no.z = i * 50;

            // Position
            Vector3 pos = gameObject.transform.position;

            GameObject go = (GameObject)Instantiate(currentObject, pos, Quaternion.identity);

            no.gameObject = go;
            no.originalPosition = pos;
            allBoids[i] = no;
            allBoids[i].gameObject.transform.parent = gameObject.transform;
        }
        else
        {
            List<Vector2> ordered = new List<Vector2>();
            PoissonDisc pd = new PoissonDisc();
            ordered = pd.GetOrderedPositions(PDWidth, PDHeight, PDRadius, itemCount);
            allBoids = new NewObj[ordered.Count];

            for (int i = 0; i < ordered.Count; i++)
            {
                if (ordered[i] != Vector2.zero)
                {
                    NewObj no = new NewObj();
                    no.x = i * 10;
                    no.z = i * 50;

                    GameObject currentObject = preFab;

                    // Position
                    Vector3 pos = new Vector3(ordered[i].x * sceneScale, gameObject.transform.position.y, ordered[i].y * sceneScale);
                    pos = pos + gameObject.transform.position;


                    // Rotation
                    Vector3 rotation = new Vector3(currentObject.transform.localRotation.x, Random.Range(0, 360), currentObject.transform.localRotation.z);

                    GameObject go = (GameObject)Instantiate(currentObject, pos, Quaternion.Euler(rotation));
                    no.gameObject = go;
                    no.originalPosition = pos;
                    allBoids[i] = no;
                    allBoids[i].gameObject.transform.parent = gameObject.transform;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        isRunning = prefabManager.isRunning;
        if (isRunning)
        {
            for (int i=0; i < allBoids.Length; i++)
            {
                float x1 = Mathf.PerlinNoise(allBoids[i].x, 0) * distanceScalar;
                float z1 = Mathf.PerlinNoise(allBoids[i].z, 0) * distanceScalar;

                Vector3 pos = new Vector3(allBoids[i].originalPosition.x - x1,
                                          allBoids[i].gameObject.transform.position.y,
                                          allBoids[i].originalPosition.z - z1);
                pos = pos + new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z);

                allBoids[i].gameObject.transform.position = pos;

                allBoids[i].x += interval;
                allBoids[i].z += interval;
            }
        }
    }

    public class NewObj
    {
        public GameObject gameObject { get; set; }
        public float x { get; set; }
        public float z { get; set; }
        public Vector3 originalPosition { get; set; }
    }
}
