using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Script_FOV : MonoBehaviour
{

    Vector3 GetVectorFromAngle(float _angle)
    {
        float angleRad = _angle * (Mathf.PI / 180.0f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    // Start is called before the first frame update
    void Start()
    {
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        float FOV = 90f;
        int rayCount = 50;
        float currentAngle = 0;
        float increaseAngle = FOV / rayCount;
        float viewDistance = 3.0f;

        Vector3 origin = Vector3.zero;

        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;

        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;
    
            RaycastHit2D rayCastHit = Physics2D.Raycast(origin, GetVectorFromAngle(currentAngle), viewDistance);

            if (rayCastHit.collider == null)
            {
                vertex = origin + GetVectorFromAngle(currentAngle) * viewDistance;
            }
            else
            {
                vertex = rayCastHit.point;
            }


            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;
                triangleIndex += 3;
            }

            vertexIndex++;
          
            currentAngle -= increaseAngle;
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
