using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;

public class ParticleWavesManager2 : MonoBehaviour {

    public GameObject preFabBody;
    public GameObject preFab;
    List<GameObject> allBoids = new List<GameObject>();
    List<Vector3> allBoids2 = new List<Vector3>();
    Vector3[] vertices;

    AudioManager audioManager;
    public float sizeScalar = 50F;
    public float moveScalar = 1000F;
    public float posScalar = 1000F;
    public float timescalar = 150F;
    LineRenderer lr = new LineRenderer();

    // Use this for initialization
    void Start () {
        audioManager = GameObject.FindObjectOfType<AudioManager>();
        Mesh mesh = preFabBody.GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;

        for (int i = 0; i < vertices.Length; i=i+10)
        {
            Vector3 pos = new Vector3(vertices[i].x * preFabBody.transform.localScale.x, vertices[i].y * preFabBody.transform.localScale.y, vertices[i].z * preFabBody.transform.localScale.z);
            GameObject go = (GameObject)Instantiate(preFab, pos, Quaternion.identity);

            allBoids.Add(go);
            allBoids2.Add(pos);
            go.transform.parent = gameObject.transform;
        }

        foreach (GameObject go in allBoids)
        {
            //go.AddComponent<LineRenderer>();
            LineRenderer lr = go.GetComponent<LineRenderer>();
            lr.startWidth = 0.03F;
            lr.endWidth = 0.03F;
        }
    }
	
	// Update is called once per frame
	void Update () {

        AutoMoveAndRotate amr = gameObject.GetComponent<AutoMoveAndRotate>();
        float xMoveVal = audioManager.currentRange * moveScalar;
        float yMoveVal = audioManager.currentRange * moveScalar;
        float zMoveVal = audioManager.currentRange * moveScalar;
        amr.rotateDegreesPerSecond.value = new Vector3(xMoveVal, yMoveVal, zMoveVal);

        for (int i = 0; i < allBoids.Count - 1; i++)
        {
            // Scale
            float prevXScale = allBoids[i].transform.localScale.x;
            float xScale = Mathf.Lerp(prevXScale, audioManager.spectrum[i] * sizeScalar, Time.deltaTime * timescalar);

            float prevYScale = allBoids[i].transform.localScale.y;
            float yScale = Mathf.Lerp(prevYScale, audioManager.spectrum[i] * sizeScalar, Time.deltaTime * timescalar);

            float prevZScale = allBoids[i].transform.localScale.z;
            float zScale = Mathf.Lerp(prevZScale, audioManager.spectrum[i] * sizeScalar, Time.deltaTime * timescalar);

            allBoids[i].transform.localScale = new Vector3(xScale, yScale, zScale);

            //Vector3 prevPos = allBoids[i].transform.position;
            //Vector3 pos = Vector3.Lerp(prevPos, allBoids2[i] * (1 + (audioManager.spectrum[i] * posScalar)), Time.deltaTime * timescalar);
            //Vector3 pos = Vector3.Scale(allBoids2[i].transform.position, new Vector3(1.25F, 1.25F, 1.25F));
            //Vector3 pos = new Vector3(allBoids2[i].transform.position.x * 1 + audioManager.spectrum[i], allBoids2[i].transform.position.y * 1 + audioManager.spectrum[i], allBoids2[i].transform.position.z * 1 + audioManager.spectrum[i]);
            //allBoids[i].transform.position = pos;

            float speed = 5F;
            float step = speed * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(allBoids[i].transform.position, new Vector3(0, 0, 0), step, 0);
            //Debug.DrawRay(allBoids[i].transform.position, newDir, Color.red);
            allBoids[i].transform.rotation = Quaternion.LookRotation(newDir);

            LineRenderer lr = allBoids[i].GetComponent<LineRenderer>();
            lr.SetPosition(0, allBoids[i].transform.position);
            int endIndex = (i < allBoids.Count - 1) ? i+1 : 0;
            lr.SetPosition(1, allBoids[endIndex].transform.position);
        }
    }
}
