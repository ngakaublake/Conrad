using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Script_FOV : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask; 

    private Mesh mesh;

    private Vector3 m_Origin;
    private float m_StartingAngle;
    private float m_FOV = 90f; 

    public Vector3 GetVectorFromAngle(float _angle)
    {
        float angleRad = _angle * (Mathf.PI / 180.0f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    public static float GetAngleFromVectorFloat(Vector3 _dir)
    {
        _dir = _dir.normalized;
        float n = Mathf.Atan2(_dir.y, _dir.x) * Mathf.Rad2Deg;

        if (n < 0)
        {
            n += 360;
        }

        return n;
    }

 
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    void LateUpdate()
    {
        //float FOV = 90f;
        int rayCount = 180;
        float currentAngle = m_StartingAngle;
        float increaseAngle = m_FOV / rayCount;
        float viewDistance = 3.0f;

        //Vector3 origin = Vector3.zero;

        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = m_Origin;

        int vertexIndex = 1;
        int triangleIndex = 0;

        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex; //= origin + GetVectorFromAngle(currentAngle) * viewDistance;

            RaycastHit2D rayCastHit = Physics2D.Raycast(m_Origin, GetVectorFromAngle(currentAngle), viewDistance, layerMask);

            if (rayCastHit.collider == null)
            {
                vertex = m_Origin + GetVectorFromAngle(currentAngle) * viewDistance;
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


    public void SetOrigin(Vector3 _origin)
    {
        this.m_Origin = _origin; 
    }

    public void SetAimDirection(Vector3 _aimDirection)
    {

        m_StartingAngle = GetAngleFromVectorFloat(_aimDirection) + m_FOV / 2f;
    }

}
