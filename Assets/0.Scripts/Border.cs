using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    public float targetingRange = 5f;
    public int resolution = 10;
    LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.widthMultiplier = 0.05f;
        lineRenderer.positionCount = resolution + 1;
        lineRenderer.useWorldSpace = false;
        lineRenderer.loop = true;
    }

    void Update()
    {
        DrawCircle();
    }

    void DrawCircle()
    {
        for (int i = 0; i <= resolution; i++)
        {
            if (lineRenderer != null)
            {
                float t = i / (float)resolution;
                float angle = t * Mathf.PI * 2;
                float x = Mathf.Cos(angle) * targetingRange;
                float z = Mathf.Sin(angle) * targetingRange;
                lineRenderer.SetPosition(i, new Vector3(x, 0, z));
            }
        }
    }
}
