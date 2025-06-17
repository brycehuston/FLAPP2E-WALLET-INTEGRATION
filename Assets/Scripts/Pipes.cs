using UnityEngine;

public class Pipes : MonoBehaviour
{
    public float speed = 5f; // This will now be set by the spawner
    private float leftEdge;

    private void Start()
    {
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 1f;
    }

    private void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x < leftEdge)
        {
            Destroy(gameObject);
        }
    }

    // Removed the IncreaseSpeed method since speed management is now centralized
}
