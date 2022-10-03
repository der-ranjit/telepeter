using UnityEngine;

public class FloatingAnimationScript : MonoBehaviour
{

    // distance that the float will "travel"
    public float distance = 0.9f;

    // time it takes in seconds to complete one circle
    public float speed = 0.7f;

    private float initialX;
    private float initialY;
    private float[] randomValues = new float[12];

    void Start() {
        initialX = transform.position.x;
        initialY = transform.position.y;
        for (int i = 0; i < 12; i++) {
            randomValues[i] = Random.Range(0.3f, 5f);
            if (Random.Range(0f, 1f) < 0.5) {
                randomValues[i] *= -1;
            }
        }
        Debug.Log(randomValues);

    }

    void Update()
    {
        Vector3 position = transform.position;
        float time = speed * Time.time;
        position.x = initialX + distance * (
            Mathf.Sin(time * randomValues[0] + randomValues[1]) +
            Mathf.Sin(time * randomValues[2] + randomValues[3]) +
            Mathf.Sin(time * randomValues[4] + randomValues[5]) 
        ) / 3;
        position.y = initialY + distance * (
            Mathf.Sin(time * randomValues[6] + randomValues[7]) +
            Mathf.Sin(time * randomValues[8] + randomValues[9]) +
            Mathf.Sin(time * randomValues[10] + randomValues[11]) 
        ) / 3;
        transform.position = position;
    }
}
