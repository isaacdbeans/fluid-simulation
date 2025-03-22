using System;
using UnityEngine;
using UnityEngine.Serialization;

public class SceneSetup3D : MonoBehaviour
{
    public int particleCount = 125;
    public float particleSpacing = 1;
    public float particleSize = 1;
    public float borderBoxX = 40.0f;
    public float borderBoxY = 40.0f;
    public float borderBoxZ = 40.0f;
    public Vector3[] positions;
    public Vector3[] velocities;

    void Start()
    {
        positions = new Vector3[particleCount];
        velocities = new Vector3[particleCount];
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(0.0f, 0.0f, 0.0f), new Vector3(borderBoxX, borderBoxY, borderBoxZ));
        Gizmos.color = Color.blue;

        foreach (var position in positions)
        {
            Gizmos.DrawSphere(position, particleSize);
        }
    }

    void Update()
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
    
}