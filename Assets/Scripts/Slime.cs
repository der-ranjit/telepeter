using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Slime : MonoBehaviour
{

    public bool alive = true;
    public double Lifetime = 1f; // Updated 

    public GameObject turnInto;

    private Collision2D prevCollision;
    private ContactPoint2D? prevContact;

    public string dbg = "empty";

    void Start() {
        this.Lifetime = 1 + 5 * Random.Range(0f, 1f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        dbg = prevCollision?.otherCollider.gameObject.name;
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

    private void ReplaceWithGroundSlime(ContactPoint2D? prevContact) {
        alive = false;
        if (prevContact != null) {
            ContactPoint2D pos = (ContactPoint2D)prevContact;
            GameObject obj = Instantiate(turnInto, pos.point, Quaternion.identity);
            // Debug.Log("Collision point " + pos.point + " " + col2.collider.gameObject.name + " " + col2.otherCollider.gameObject.name);
            Debug.Log("Own pos " + this.transform.position + " " + this.name);
            obj.transform.Rotate(new Vector3(0, 0, -12.5f));
        }
        GameObject.Destroy(this.gameObject);
    }

    // public void OnCollisionEnter2D(Collision2D collision) {
    //     // Debug.Log("Enter" + collision.contactCount);
    //     prevCollision = collision;
    // }
    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("Stay" + collision.GetContact(0).point + " " + this.name + " =?= " + collision.otherCollider.gameObject.name);
        prevCollision = collision;
        prevContact = collision.GetContact(collision.contactCount - 1);
    }
    // private void OnCollisionExit2D(Collision2D collision)
    // {
    //     // Debug.Log("Exit" + collision.contactCount);
    //     prevCollision = collision;
    //     ReplaceWithGroundSlime(collision);
    // }
}
