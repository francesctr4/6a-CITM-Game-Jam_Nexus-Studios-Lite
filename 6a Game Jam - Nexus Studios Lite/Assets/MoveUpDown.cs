using UnityEngine;

public class MoveUpDown : MonoBehaviour
{
    public float startY;
    public float amplitude;
    private float timer = 0f;

    void Start()
    {
        startY = transform.position.y;
    }

    void Update()
    {
        timer += Time.deltaTime;
        float newY = startY + Mathf.Sin(timer) * amplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}