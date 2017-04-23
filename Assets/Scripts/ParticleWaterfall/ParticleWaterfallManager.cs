using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleWaterfallManager : MonoBehaviour {

    // Phyllotaxis variables
    [SerializeField]
    private GameObject preFabSmall;
    [SerializeField]
    private GameObject preFabLarge;
    [SerializeField]
    private float c = 3;
    [SerializeField]
    private int itemCount;

    [SerializeField]
    private float emitScalar = 40F;
    [SerializeField]
    private float sizeScalar = 50F;
    [SerializeField]
    private float gravityScalar = 0.15F;

    // Private
    private GameObject[] allBoidsSmall;
    private ParticleSystem[] allBoidsLarge;
    public float sceneScale = 1F;
    public bool isRunning;
    private float currentRange = 0;
    PreFabManager prefabManager;
    AudioManager audioManager;

    // Use this for initialization
    void Start()
    {
        prefabManager = gameObject.GetComponent<PreFabManager>();

        // Particle scale
        sceneScale = (gameObject.transform.parent == null) ? 1F : gameObject.transform.parent.transform.localScale.x;
        float gameObjectScale = gameObject.transform.localScale.x;

        audioManager = GameObject.FindObjectOfType<AudioManager>();
        //psArray = gameObject.GetComponentsInChildren<ParticleSystem>();

        List<Vector2> ordered = new List<Vector2>();
        Phyllotaxis pt = new Phyllotaxis();
        ordered = pt.GetOrderedPositions(c, itemCount);
        allBoidsSmall = new GameObject[ordered.Count];
        for (int i = 0; i < ordered.Count; i++)
        {
            if (ordered[i] != Vector2.zero)
            {
                GameObject currentObject = preFabSmall;

                // Position
                // Random.Range(gameObject.transform.position.y+2, gameObject.transform.position.y + 5)
                float ypos = (gameObject.transform.position.y * sceneScale * gameObjectScale) + (3.5F * sceneScale * gameObjectScale);
                Vector3 pos = new Vector3(ordered[i].x * sceneScale * gameObjectScale, ypos, ordered[i].y * sceneScale * gameObjectScale);
                pos = pos + gameObject.transform.position;

                // Rotation
                Vector3 rotation = new Vector3(90, 0, 0);

                GameObject go = (GameObject)Instantiate(currentObject, pos, Quaternion.Euler(rotation));
                go.transform.parent = gameObject.transform;
                allBoidsSmall[i] = go;
            }
        }

        //Large scale
        float arc = 60;
        float radius = 30;
        int numberOfObjects = 3;
        float angle = 360 / numberOfObjects;
        allBoidsLarge = new ParticleSystem[numberOfObjects];
        for (int i = 0; i < numberOfObjects; i++)
        {
            GameObject currentObject = preFabLarge;

            Vector3 pos = new Vector3(0, 3, 0);
            pos = pos + gameObject.transform.position;


            // Shape
            var shape = preFabLarge.GetComponent<ParticleSystem>().shape;
            shape.arc = arc;
            shape.radius = radius * sceneScale * gameObjectScale;


            // Rotation
            Vector3 rotation = new Vector3(90, i * angle, 0);

            GameObject go = (GameObject)Instantiate(currentObject, pos, Quaternion.Euler(rotation));
            go.transform.parent = gameObject.transform;
            allBoidsLarge[i] = go.GetComponent<ParticleSystem>();

            // Scale
            allBoidsLarge[i].transform.localScale = new Vector3(sceneScale * gameObjectScale, sceneScale * gameObjectScale, sceneScale * gameObjectScale);

            prefabManager.SetEmissionRate(allBoidsLarge[i], 0);
        }

        //foreach (ParticleSystem ps in psArray)
        //{
        //    // Position
        //    Vector3 pos = new Vector3(ps.transform.position.x * sceneScale * gameObjectScale,
        //                              ps.transform.position.y * sceneScale * gameObjectScale,
        //                              ps.transform.position.z * sceneScale * gameObjectScale);
        //    ps.transform.position = pos;

        //    // Scale
        //    ps.transform.localScale = new Vector3(sceneScale * gameObjectScale, sceneScale * gameObjectScale, sceneScale * gameObjectScale);

        //    // Shape (Radius)
        //    var shape = ps.shape;
        //    shape.radius = shape.radius * sceneScale * gameObjectScale;

        //    // Set particle system emission rate;
        //    prefabManager.SetEmissionRate(ps, 0);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        isRunning = prefabManager.isRunning;
        if (isRunning)
        {
            if (audioManager != null)
                currentRange = audioManager.currentRange;

            foreach (ParticleSystem ps in allBoidsLarge)
            {
                prefabManager.SetEmissionRate(ps, currentRange * emitScalar);
                ps.startSize = currentRange * sizeScalar;
                ps.gravityModifier = currentRange * gravityScalar;
            }
        }
        else
        {
            foreach (ParticleSystem ps in allBoidsLarge)
            {
                // Set particle system emission rate;
                prefabManager.SetEmissionRate(ps, 0);
            }
        }
    }
}
