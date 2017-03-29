using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoissonDisc : MonoBehaviour {

    int width = 400;
    int height = 400;
    int items = 100;
    float r = 10F;
    int k = 30;
    Vector2[] grid;
    float w = 0;
    int cols, rows;
    List<Vector2> active = new List<Vector2>();    
    List<Vector2> ordered = new List<Vector2>();

    public List<Vector2> GetOrderedPositions(int w, int h, float rad, int i)
    {
        width = w;
        height = h;
        r = rad;
        items = i;

        Setup();
        //Draw();

        if (items > 0)
        {
            StartWithItemCount();
        }
        else
        {
            StartWithWhile();
        }

        for (int n = 0; n < ordered.Count; n++)
        {
            if (ordered[n] != Vector2.zero)
            {
                //Vector2 newpos = new Vector2(ordered[i].x - (width / 2), ordered[i].y - (height / 2));
                ordered[n] = new Vector2(ordered[n].x - (width / 2), ordered[n].y - (height / 2));
            }
        }

        return ordered;
    }

    private void Setup()
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

    private void StartWithItemCount()
    {
        for (var total = 0; total < items; total++)
        {
            if (active.Count > 0)
            {
                Draw();
            }
        }
    }

    private void StartWithWhile()
    {
        while (active.Count > 0)
        {
            Draw();
        }
    }


    private void Draw()
    {
        //for (var total = 0; total < items; total++)
        //{
            //while (active.Count > 0)
            //{
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
                                if (index > 0 && index <= grid.Length - 1)
                                {
                                    Vector2 neighbor = neighbor = grid[index];
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
            //}
        //}
    }
}
