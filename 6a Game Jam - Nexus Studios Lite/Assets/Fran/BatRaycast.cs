using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatRaycast : MonoBehaviour
{
    public GameObject obstacleRayObject;
    private float obstacleRayDistance;

    private LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        GameObject lineObject = new GameObject("RaycastLine");
        lineRenderer = lineObject.AddComponent<LineRenderer>();
    }

    void Update()
    {
        Vector3 mousePositionScreen = Input.mousePosition;

        Vector3 mousePositionWorld = Camera.main.ScreenToWorldPoint(mousePositionScreen);

        obstacleRayDistance = Vector2.Distance(obstacleRayObject.transform.position, new Vector2(mousePositionWorld.x, mousePositionWorld.y));

        RaycastHit2D hitObstacle = Physics2D.Raycast(obstacleRayObject.transform.position, new Vector2(mousePositionWorld.x, mousePositionWorld.y));
           
        lineRenderer.startWidth = 0.001f;
        lineRenderer.endWidth = obstacleRayDistance * 0.5f;
        lineRenderer.startColor = Color.green;
        lineRenderer.endColor = Color.green;
        lineRenderer.SetPosition(0, obstacleRayObject.transform.position);
        lineRenderer.SetPosition(1, new Vector2(mousePositionWorld.x, mousePositionWorld.y));

        RaycastHit hit;

        if (Physics.Linecast(obstacleRayObject.transform.position, new Vector2(mousePositionWorld.x, mousePositionWorld.y), out hit))
        {
            // Check if the collision is with an object having the specified tag
            if (hit.collider.CompareTag("Wall"))
            {
                // Hide the LineRenderer
                lineRenderer.enabled = false;
            }
            else
            {
                // Show the LineRenderer
                lineRenderer.enabled = true;
            }
        }

    }

}
