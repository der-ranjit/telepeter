using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject spawnObject;
    public void Spawn() {
        if (spawnObject != null) {
            var spawnedObject = Instantiate(spawnObject, gameObject.transform.position, Quaternion.identity);
            if (spawnedObject != null) {
                Destroy(gameObject);
            }
        }
    }

}
