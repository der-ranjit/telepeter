using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private bool onGround;
    private float friction;
    private Vector2 normal;
    private Vector2 right;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EvaluateCollision(collision);
        RetrieveFriction(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        EvaluateCollision(collision);
        RetrieveFriction(collision);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        // Debug.Log("onCollisionExit");
        onGround = false;
        friction = 0;
    }

    private void EvaluateCollision(Collision2D collision)
    {
        onGround = false;
        for (int i = 0; i < collision.contactCount; i++)
        {
            normal = collision.GetContact(i).normal;
            onGround |= normal.y >= 0.5f;
            // Debug.Log(normal+" isGround: "+onGround+" condition: "+(normal.y >= 0.5f));
        }
    }

    private void RetrieveFriction(Collision2D collision)
    {
        PhysicsMaterial2D material = collision?.rigidbody?.sharedMaterial;
        
        friction = 0;
        if (material != null)
        {
            friction = material.friction;
        }
    }

    public bool GetOnGround()
    {
        return onGround;
    }

    public float GetFriction()
    {
        return friction;
    }

    public Vector2 GetNormal()
    {
        return normal;
    }
}
