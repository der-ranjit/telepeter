using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Slime : MonoBehaviour
{

    public bool alive = true;
    public double Lifetime = 1f; // Updated 

    public GameObject turnInto;

    private ContactPoint2D prevContact;


    void Start() {
        this.Lifetime = 1 + 5 * Random.Range(0f, 1f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (alive) {
            // Falling/bouncing
            Lifetime -= Time.fixedDeltaTime;
            if (Lifetime <= 0) {
                ReplaceWithGroundSlime(prevContact);
            }
        } else {
            // Is dead on ground, accelerating player
        }
    }

    private void ReplaceWithGroundSlime(ContactPoint2D prevContact) {
        alive = false;
        // Create ground slime
        Vector2 pos = prevContact.point + new Vector2(0, 0);
        GameObject obj = Instantiate(turnInto, pos, Quaternion.identity);
        float angle = Vector2.SignedAngle(new Vector2(0, 1), prevContact.normal);
        obj.transform.Rotate(new Vector3(0, 0, angle));
        if (Random.Range(0f, 1f) < 0.5f) {
            obj.GetComponent<SpriteRenderer>().flipX = true;
        }
        // Remove blob slime (self)
        GameObject.Destroy(this.gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        prevContact = collision.GetContact(collision.contactCount - 1);
    }
}
