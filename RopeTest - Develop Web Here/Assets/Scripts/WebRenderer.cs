using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WebRenderer : MonoBehaviour {

    public GameObject startPoint;
    public GameObject startTangent;
    public GameObject endPoint;
    public GameObject endTangent;

    public Color color;
    public float width;
    public int numPoints;

	// Use this for initialization
	void Start () 
    {
        LineRenderer lineRenderer = (LineRenderer) GetComponent("LineRenderer");

        if (lineRenderer == null)
        {
            gameObject.AddComponent("LineRenderer");
        }
        lineRenderer = (LineRenderer)GetComponent("LineRenderer");
        lineRenderer.useWorldSpace = true;
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
	}
	
	// Update is called once per frame
	void Update () 
    {
        LineRenderer lineRenderer = (LineRenderer)GetComponent("LineRenderer");
        if(lineRenderer == null || startPoint == null || endPoint == null || startTangent == null || endTangent == null)
            return;
        lineRenderer.SetColors(color, color);
        lineRenderer.SetWidth(width, width);
        if (numPoints > 0)
            lineRenderer.SetVertexCount(numPoints);

        Vector2 pStart = startPoint.transform.position;
        Vector2 pStartTangent = startTangent.transform.position;
        Vector2 pEnd = endPoint.transform.position;
        Vector2 pEndTangent = endTangent.transform.position;

        Vector2 position;
        float t;
        for(int i = 0; i < numPoints; i++)
        {
            t = i / (float)(numPoints - 1.0);
            position = (float)(2.0 * t * t * t - 3.0 * t * t + 1.0) * pStart +
                (float)(t * t * t - 2.0 * t * t + t) * pStartTangent +
                (float)(-2.0 * t * t * t + 3.0 * t * t) * pEnd +
                (t * t * t - t * t) * pEndTangent;
            lineRenderer.SetPosition(i, position);
        }
	}
}
