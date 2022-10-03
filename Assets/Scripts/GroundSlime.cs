using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSlime : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Check rarely to not hurt performance unnecessarily
        if (Random.Range(0f, 1f) < 0.1) {
            GameObject target = GameObject.FindGameObjectWithTag("Player");
            Move MoveScript = target.GetComponent<Move>();
            if (target && MoveScript) { // TODO: check if target is currently not sped up
                if (MoveScript.GetSpeedUpDuration() < 0.2f) {
                    // TODO: speed up player for 0.5 seconds or so
                    Vector2 diff = (Vector2)(target.transform.position - this.gameObject.transform.position);
                    if (diff.magnitude < 2) {
                        MoveScript.SetSpeedUpDuration(0.5f);
                    }
                }
            }
        }
    }
}
