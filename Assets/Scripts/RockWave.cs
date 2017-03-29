using UnityEngine;
using System.Collections;

public class RockWave : MonoBehaviour {

	public GameObject preFab;
	float startAngle = 0;
	public float angleVel = 0.23F;
	public float speed = 0.015F;
	public float height = 10;

	public int items = 5;
	GameObject[] allBoids;
	public float r = 1F;

	// Use this for initialization
	void Start () {
		allBoids = new GameObject[items];

		for (var i = 0; i < items; i++)
		{
			float x = gameObject.transform.position.x + r * Mathf.Cos(2 * Mathf.PI * i / items);
			float z = gameObject.transform.position.z + r * Mathf.Sin(2 * Mathf.PI * i / items);

			Vector3 pos = new Vector3(x, 0, z);
			allBoids[i] = (GameObject)Instantiate(preFab, pos, Quaternion.identity);
		}
	}
	
	// Update is called once per frame
	void Update () {
		startAngle += speed;
		float angle = startAngle;

		for (var i = 0; i < items; i++)
		{
			float y = Map(Mathf.Sin(angle), -1, 1, 0, height);

			Vector3 pos = new Vector3(allBoids[i].transform.position.x, y, allBoids[i].transform.position.z);
			allBoids[i].transform.position = pos;
			angle += angleVel;
		}


	}

	public float Map(float x, float in_min, float in_max, float out_min, float out_max)
	{
		return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
	}
}
