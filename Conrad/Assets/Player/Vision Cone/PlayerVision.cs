using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerVision : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask; //Controls the Objects that Block the RayCast

    private Mesh mesh;

    private Vector3 m_Origin; 
    private float m_StartingAngle; 
    private float m_FOV = 90f; //Vision Cone FOV 
    int m_RayCount = 180; //How Many Triangles in the Vision Cone - More = Smoother 
    float m_ViewDistance = 3.0f; //View Distance for the Vision Cone 

    public Vector3 GetVectorFromAngle(float _angle) //Function to get a Vector3 from a Angle 
    {
        float angleRad = _angle * (Mathf.PI / 180.0f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    public static float GetAngleFromVectorFloat(Vector3 _dir) //Function to Get a Angle from a Vector3 
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

    void LateUpdate() //Ill add comments later - Patrick 
    {
        float currentAngle = m_StartingAngle;
        float increaseAngle = m_FOV / m_RayCount;
       
        Vector3[] vertices = new Vector3[m_RayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[m_RayCount * 3];

        vertices[0] = m_Origin;

        int vertexIndex = 1;
        int triangleIndex = 0;

        for (int i = 0; i <= m_RayCount; i++)
        {
            Vector3 vertex; 

            RaycastHit2D rayCastHit = Physics2D.Raycast(m_Origin, GetVectorFromAngle(currentAngle), m_ViewDistance, layerMask); 

            if (rayCastHit.collider == null) //Checking if any Rays hit a Object 
            {
                vertex = m_Origin + GetVectorFromAngle(currentAngle) * m_ViewDistance;
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

   public void UpdateFOV() //Temp testing function for swapping FOV - Looking to animate slowly bewtween max FOV and combat FOV - Patrick
    {
        //this is not staying in the final build 
        if (m_FOV != 25 )
        {
          
            m_FOV = 25;
            m_ViewDistance = 5.0f;
            
        }
        else
        {
           m_FOV = 90;
           m_ViewDistance = 3.0f;
        }
        
    }
}
