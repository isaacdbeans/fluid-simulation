using System;
using UnityEngine;
using UnityEngine.Serialization;

public class SceneSetup3D : MonoBehaviour
{
    private int particleCount = 125;
    private float particleSpacing = 1;
    private float particleSize = 1;
    public float borderBoxSize = 100.0f;
    public float gravity = 0.0f;
    public float dampingFactor = 0.7f;
    public Vector3[] positions;
    public Vector3[] velocities;
    
    void Start()
    {
        positions = new Vector3[particleCount];
        velocities = new Vector3[particleCount];
        
        int particlesPerRow = (int)Mathf.Ceil(Mathf.Pow(particleCount, 1f / 3f));
        int particlesPerLayer = (int)Mathf.Ceil((float)particleCount / (particlesPerRow * particlesPerRow));

        float spacing = particleSize * 2 + particleSpacing;

        for (int i = 0; i < particleCount; i++)
        {
            int row = i % particlesPerRow;
            int col = (i / particlesPerRow) % particlesPerRow;
            
            int layer = i / (particlesPerRow * particlesPerRow);
            
            float x = (row - particlesPerRow / 2f + 0.5f) * spacing;
            float y = (col - particlesPerRow / 2f + 0.5f) * spacing;
            float z = (layer - particlesPerLayer / 2f + 0.5f) * spacing;

            positions[i] = new Vector3(x, y, z);
        }
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(0.0f, 0.0f, 0.0f), new Vector3(borderBoxSize, borderBoxSize, borderBoxSize));
        Gizmos.color = Color.blue;

        foreach (var position in positions)
        {
            Gizmos.DrawSphere(position, particleSize);
        }
    }

    void Update()
    {
        for (int i =0; i<particleCount; i++)
        {
            velocities[i] += Vector3.down * gravity;
            positions[i] += velocities[i];
            
            if (Math.Abs(positions[i].x) > borderBoxSize / 2)
            {
                positions[i].x = (borderBoxSize / 2) * Math.Sign(positions[i].x); 
                velocities[i].x *= -1 * dampingFactor;
            }
            if (Math.Abs(positions[i].y) > borderBoxSize / 2)
            {
                positions[i].y = (borderBoxSize / 2) * Math.Sign(positions[i].y); 
                velocities[i].y *= -1 * dampingFactor;
            }
            
            if (Math.Abs(positions[i].z) > borderBoxSize / 2)
            {
                positions[i].z = (borderBoxSize / 2) * Math.Sign(positions[i].z); 
                velocities[i].z *= -1 * dampingFactor;
            }
        }
    }
    
}