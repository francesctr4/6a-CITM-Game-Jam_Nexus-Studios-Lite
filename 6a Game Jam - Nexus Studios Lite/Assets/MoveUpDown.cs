using UnityEngine;

public class MoveUpDown : MonoBehaviour
{
    public float startY;
    public float amplitude;
    public float speed;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float newY = startY + Mathf.Sin(Time.time * speed) * amplitude;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }
}
