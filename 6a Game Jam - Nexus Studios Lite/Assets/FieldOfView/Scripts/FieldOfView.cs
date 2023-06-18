using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class FieldOfView : MonoBehaviour {

    private bool isMousePressed = false;
    [SerializeField] private List<LayerMask> layerMasks;
    private Mesh mesh;
    public float fov;
    public float viewDistance;
    private Vector3 origin;
    private float startingAngle;

    private void Start() 
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        //fov = 45;
        //viewDistance = 5;
        origin = Vector3.zero;
    }

    private void LateUpdate() 
    {
        if (Input.GetMouseButtonDown(0))
        {
            isMousePressed = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isMousePressed = false;
        }

        if (isMousePressed)
        {
            int rayCount = 50;
            float angle = startingAngle;
            float angleIncrease = fov / rayCount;

            Vector3[] vertices = new Vector3[rayCount + 1 + 1];
            Vector2[] uv = new Vector2[vertices.Length];
            int[] triangles = new int[rayCount * 3];

            vertices[0] = origin;

            int vertexIndex = 1;
            int triangleIndex = 0;
            for (int i = 0; i <= rayCount; i++)
            {
                bool hasPoint = false;
                Vector2 closestPoint = new Vector2(float.MaxValue, float.MaxValue);

                foreach (LayerMask layer in layerMasks)
                {
                    RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, UtilsClass.GetVectorFromAngle(angle), viewDistance, layer);

                    if(raycastHit2D.collider != null)
                    {
                        if(!hasPoint)
                        {
                            closestPoint = raycastHit2D.point;
                            hasPoint = true;
                            continue;
                        }

                        float closestDistance = Vector2.Distance(closestPoint, origin) + 400;
                        float currentDistance = Vector2.Distance(raycastHit2D.point, origin);

                        if(currentDistance < closestDistance)
                        {
                            closestPoint = raycastHit2D.point;
                        }
                    }
                }

                Vector3 vertex;

                if (!hasPoint)
                {
                    // No hit
                    vertex = origin + UtilsClass.GetVectorFromAngle(angle) * viewDistance;
                }
                else
                {
                    // Hit object
                    vertex = closestPoint;
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
                angle -= angleIncrease;
            }


            mesh.vertices = vertices;
            mesh.uv = uv;
            mesh.triangles = triangles;
            mesh.bounds = new Bounds(origin, Vector3.one * 1000f);
        }
        else
        {
            mesh.Clear();
        }
    }
    


    public void SetOrigin(Vector3 origin) {
        this.origin = origin;
    }

    public void SetAimDirection(Vector3 aimDirection) {
        startingAngle = UtilsClass.GetAngleFromVectorFloat(aimDirection) + fov / 2f;
    }

    public void SetFoV(float fov) {
        this.fov = fov;
    }

    public void SetViewDistance(float viewDistance) {
        this.viewDistance = viewDistance;
    }

}
