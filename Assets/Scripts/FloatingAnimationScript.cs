using UnityEngine;

public class FloatingAnimationScript : MonoBehaviour
{

    // distance that the float will "travel"
    public float distance = 1;

    // time it takes in seconds to complete one circle
    public float speed = 6;

    private float initialY;

    void Start() {
        initialY = transform.position.y;
    }

    void Update()
    {
        Vector3 position = transform.position;
        position.y = initialY + distance * Mathf.Sin(speed * Time.time);
        transform.position = position;
    }
}
