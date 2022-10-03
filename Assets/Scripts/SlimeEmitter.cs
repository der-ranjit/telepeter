using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEmitter : InteractionTarget
{

    public GameObject slimePrefab;
    public int emittingFrameCount = 220; 

    private static int counter = 0;
    public override void ReactToInteraction(GameObject other) {
        this.emittingFrameCount = 180;
    }

    public void FixedUpdate() {
        if (this.emittingFrameCount > 0) {
            this.emittingFrameCount--;
            if (this.emittingFrameCount % 20 == 0) {
                this.spawnSlime();
            }
        }
    }

    public void spawnSlime() {
        GameObject slime = Instantiate(slimePrefab, this.transform.position + new Vector3(0, -1, 0), Quaternion.identity);
        counter += 1;
        slime.name = "Slime_" + counter;
    }
}
