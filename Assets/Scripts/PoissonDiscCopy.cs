using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoissonDiscCopy : MonoBehaviour {

    public GameObject preFab;
    public int width = 400;
    public int height = 400;
    public int items = 100;
    public int r = 10;
    int k = 30;
    Vector2[] grid;
    float w = 0;
    int cols, rows;
    List<Vector2> active = new List<Vector2>();    
    List<Vector2> ordered = new List<Vector2>();

    // Use this for initialization
    void Start () {
        Setup();
        Draw();
    }

    public void Setup()
    {
        // Initalise variables
        w = r / Mathf.Sqrt(2);

        // STEP 0
        cols = (int)Mathf.Floor(width / w);
        rows = (int)Mathf.Floor(height / w);
        grid = new Vector2[cols * rows];
        for (int n = 0; n < cols * rows; n++)
        {
            grid[n] = Vector3.zero;
        }

        // STEP 1
        int x = width / 2;
        int y = height / 2;
        int i = (int)Mathf.Floor(x / w);
        int j = (int)Mathf.Floor(y / w);
        Vector2 pos = new Vector2(x, y);
        grid[i + j * cols] = pos;
        active.Add(pos);
    }

    public void Draw()
    {
        for (var total = 0; total < items; total++)
        {
            if (active.Count > 0)
            {
                int randIndex = (int)Random.Range(0, active.Count - 1);
                Vector2 pos = active[randIndex];
                bool found = false;
                for (var n = 0; n < k; n++)
                {
                    Vector2 sample = new Vector2();
                    float a = 2 * Mathf.PI * Random.value;
                    float m = Random.Range(r, 2 * r);
                    Vector2 sampleTmp = m * new Vector2(Mathf.Cos(a), Mathf.Sin(a));
                    sample = pos + sampleTmp;

                    int col = (int)Mathf.Floor(sample.x / w);
                    int row = (int)Mathf.Floor(sample.y / w);

                    if (col > -1 && row > -1 && col < cols && row < rows && (grid[col + row * cols] == Vector2.zero))
                    {
                        bool ok = true;
                        for (var i = -1; i <= 1; i++)
                        {
                            for (var j = -1; j <= 1; j++)
                            {
                                int index = (col + i) + (row + j) * cols;
                                Vector2 neighbor = grid[index];
                                if (neighbor != Vector2.zero)
                                {
                                    float d = Vector2.Distance(sample, neighbor);
                                    if (d < r)
                                    {
                                        ok = false;
                                    }
                                }
                            }
                        }

                        if (ok)
                        {
                            found = true;
                            grid[col + row * cols] = sample;
                            active.Add(sample);
                            ordered.Add(sample);

                            // Should we break?
                            break;
                        }
                    }
                }

                if (!found)
                {
                    active.RemoveAt(randIndex);
                }
            }
        }

        Debug.Log(ordered.Count);
        GameObject currentObject = preFab;
        for (int i = 0; i < ordered.Count; i++)
        {
            if (ordered[i] != Vector2.zero)
            {
                Vector3 pos = new Vector3(ordered[i].x, 0, ordered[i].y);
                GameObject go = (GameObject)Instantiate(preFab, pos, Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update() {
    }
}
