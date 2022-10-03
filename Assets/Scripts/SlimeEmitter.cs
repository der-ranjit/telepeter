using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEmitter : InteractionTarget
{

    public GameObject slimePrefab;
    public int emittingFrameCount = 0;

    public int firstEmissionFrameCount = 500;

    public int furtherEmissionFrameCounts = 50;

    public int emittingFrameDelayMin = 7;
    public int emittingFrameDelayMax = 14;

    private int remainingFramesTillEmit = 0;

    private int numOfEmissions = 0;

    private static int counter = 0;
    public override void ReactToInteraction(GameObject other) {
        this.numOfEmissions++;
        if (this.numOfEmissions <= 1) {
            this.emittingFrameCount = this.firstEmissionFrameCount;
        } else {
            this.emittingFrameCount = this.furtherEmissionFrameCounts;
        }
        // TODO play some bouncy animation
    }

    public void FixedUpdate() {
        if (this.emittingFrameCount > 0) {
            this.emittingFrameCount--;
            this.remainingFramesTillEmit--;
            if (this.remainingFramesTillEmit <= 0) {
                this.spawnSlime();
                this.remainingFramesTillEmit = Random.Range(this.emittingFrameDelayMin, this.emittingFrameDelayMax);
            }
        }
    }

    public void spawnSlime() {
        GameObject slime = Instantiate(slimePrefab, this.transform.position + new Vector3(0.55f, 0.7f, 0), Quaternion.identity);
        counter += 1;
        slime.name = "Slime_" + counter;
        slime.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(1f, 5f), Random.Range(2f, 7f));
        // Ensure not to collide with slime
        Physics2D.IgnoreCollision(slime.GetComponent<Collider2D>(), this.GetComponent<Collider2D>());
    }

    public void OnCollisionEnter2D(Collision2D collision) {
        GameObject target = collision.collider.gameObject;
        if (target.tag == "Player") {
            if (target.transform.position.y > this.transform.position.y) {
                if (this.emittingFrameCount <= 0) {
                    this.ReactToInteraction(target);
                }
                this.bounceAway(target);
            }
        }
    }

    public void bounceAway(GameObject other) {
        Rigidbody2D body = other.GetComponent<Rigidbody2D>();
        bool right = (other.transform.position.x > this.transform.position.x);
        body.velocity = new Vector2(body.velocity.x + (right ? 20f : -20f), Random.Range(15f, 22f));
    }
}
