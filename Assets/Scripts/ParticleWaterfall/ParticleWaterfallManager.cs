using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleWaterfallManager : MonoBehaviour {

    // Phyllotaxis variables
    [SerializeField]
    private GameObject preFab;
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
    private GameObject[] allBoids;
    public float sceneScale = 1F;
    public bool isRunning;
    private float currentRange = 0;
    PreFabManager prefabManager;
    ParticleSystem[] psArray;
    AudioManager audioManager;

    // Use this for initialization
    void Start()
    {
        prefabManager = gameObject.GetComponent<PreFabManager>();
        audioManager = GameObject.FindObjectOfType<AudioManager>();
        psArray = gameObject.GetComponentsInChildren<ParticleSystem>();

        List<Vector2> ordered = new List<Vector2>();
        Phyllotaxis pt = new Phyllotaxis();
        ordered = pt.GetOrderedPositions(c, itemCount);
        allBoids = new GameObject[ordered.Count];

        for (int i = 0; i < ordered.Count; i++)
        {
            if (ordered[i] != Vector2.zero)
            {
                GameObject currentObject = preFab;

                // Position
                // Random.Range(gameObject.transform.position.y+2, gameObject.transform.position.y + 5)
                float ypos = gameObject.transform.position.y + 3.5F;
                Vector3 pos = new Vector3(ordered[i].x * sceneScale, ypos, ordered[i].y * sceneScale);

                // Rotation
                Vector3 rotation = new Vector3(90, 0, 0);

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
        if (isRunning)
        {
            if (audioManager != null)
                currentRange = audioManager.currentRange;

            foreach (ParticleSystem ps in psArray)
            {
                prefabManager.SetEmissionRate(ps, currentRange * emitScalar);
                ps.startSize = currentRange * sizeScalar;
                ps.gravityModifier = currentRange * gravityScalar;
            }
        }
    }
}
