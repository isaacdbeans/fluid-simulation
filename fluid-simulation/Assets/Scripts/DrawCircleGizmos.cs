using System;
using UnityEngine;

public class DrawCircleGizmos : MonoBehaviour
{
    public int circleCount = 1;
    public float radius = 1.0f;
    public Color color = Color.green;
    public Vector3[] positions;
    public Vector3[] velocities;
    public float borderBoxX = 40.0f;
    public float borderBoxY = 40.0f;
    public float borderBoxZ = 40.0f;
    public float gravity = 0.01f;
    public float dampingFactor = 0.9f;

    void Start()
    {
        positions = new Vector3[circleCount];
        velocities = new Vector3[circleCount];
        for (int i = 0; i < circleCount; i++)
        {
            positions[i] = new Vector3(0.0f, 0.0f, i);
            velocities[i] = new Vector3(0.0f, 1.0f, 0.0f);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(0.0f, 0.0f, 0.0f), new Vector3(borderBoxX, borderBoxY, borderBoxZ));
        Gizmos.color = color;
        foreach (Vector3 position in positions)
        {
            Gizmos.DrawSphere(position, radius);
        }
    }

    void Update()
    {
        for (int i =0; i<circleCount; i++)
        {
            velocities[i] += Vector3.down * gravity;
            positions[i] += velocities[i];
            
            if (Math.Abs(positions[i].x) > borderBoxX / 2)
            {
                positions[i].x = (borderBoxX / 2) * Math.Sign(positions[i].x); 
                velocities[i].x *= -1 * dampingFactor;
            }
            if (Math.Abs(positions[i].y) > borderBoxY / 2)
            {
                positions[i].y = (borderBoxY / 2) * Math.Sign(positions[i].y); 
                velocities[i].y *= -1 * dampingFactor;
            }
        }
    }
    
}