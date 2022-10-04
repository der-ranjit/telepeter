using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketIntro : MonoBehaviour
{

    private int currentPhase = 1;
    public float Phase1Duration = 4f; // Rocket flying stably and happily
    public float Phase2Duration = 4f; // Rocket tilting and crashing into earth (animation starts here, but ends a bit later)
    public float Phase3Duration = 3f; // Rocket static in ground, animation finishes, nothing happens yet
    public float RotationDuration = 3.4f; // Rocket rotates during phase 2

    public float BackgroundAnimationStart = 2.5f; // Here we start fading out space bg
    public float BackgroundAnimationDuration = 4f; // So long until space bg is fully faded out

    private GameObject rocketParent;
    private GameObject background;
    private GameObject rocket1;
    private GameObject rocket2;
    private GameObject rocket3;

    private float initialY;
    public float targetY = 0f;

    private float aniStartTime = -1f;

    // Start is called before the first frame update
    void Start()
    {
        rocket1 = UIManager.Instance.FindObjectByName("rocket_initial");
        rocket2 = UIManager.Instance.FindObjectByName("rocket_transformation");
        rocket3 = UIManager.Instance.FindObjectByName("crashed_rocket");
        rocketParent = rocket1.transform.parent.gameObject;
        background = UIManager.Instance.FindObjectByName("background_space");
        initialY = this.gameObject.transform.position.y;
        Telepeter.Instance.gameObject.SetActive(false);
        UIManager.Instance.FindObjectByName("PlayerFollowCamera").GetComponent<FollowPlayerCamera>().SetTagToFollow("Rocket");
    }

    // Update is called once per frame
    void Update()
    {
        float time = Time.time;
    
        if (aniStartTime < 0) {
            if (GameStateManager.Instance.currentGameLifeState == GameLifeStates.Running) {
                this.aniStartTime = time;
            } else {
                return;
            }
        }

        time -= this.aniStartTime;

        if (currentPhase <= 3) {
            // Rocket position
            float p = Mathf.Min(time / (Phase1Duration + Phase2Duration), 1f);
            Vector3 pos = this.gameObject.transform.position;
            float y = initialY * (1 - p) + targetY * p;
            this.gameObject.transform.position = new Vector3(pos.x, y, pos.z);

            // Cam shake
            if (currentPhase < 3) {
                float p5 = time / (Phase1Duration + Phase2Duration);
                float skewed = p5 * p5;
                p5 = 0.5f - 0.5f * Mathf.Cos(2f * Mathf.PI * skewed);
                this.applyCamShake(p5 * 0.4f);
            } else {
                this.applyCamShake(0f);
            }

            // Background Fading
            float bg_alpha = 1;
            float t1 = BackgroundAnimationStart;
            float t2 = t1 + BackgroundAnimationDuration;
            if (time > t1 && time < t2) {
                bg_alpha = 1 - (time - t1) / (t2 - t1);
            } else if (time >= t2) {
                bg_alpha = 0;
            }
            UIManager.Instance.FindObjectByName("background_space").GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,bg_alpha);
        } else {
            return;
        }

        if (currentPhase == 1) {
            if (time > Phase1Duration) {
                // Proceed to Phase 2
                currentPhase = 2;
                // Replace sprite
                rocket1.SetActive(false);
                rocket2.SetActive(true);
            } else {
                // Update phase 1
            }
        } else if (currentPhase == 2) {
            if (time > Phase1Duration + Phase2Duration) {
                // Proceed to Phase 3
                currentPhase = 3;
                rocket2.SetActive(false);
                rocket3.SetActive(true);
            } else {
                // Update phase 2
                // Rotation
                float p = Mathf.Min((time - Phase1Duration) / RotationDuration, 1);
                float smoothed = 0.5f - 0.5f * Mathf.Cos(Mathf.PI * p);
                float rotation = smoothed * 360;
                UIManager.Instance.FindObjectByName("rocketStuff").transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotation));
            }
        } else if (currentPhase == 3) {
            if (time > Phase1Duration + Phase2Duration + Phase3Duration) {
                // Finalize intro, spawn player n shit
                currentPhase = 4;
                Telepeter.Instance.gameObject.SetActive(true);
                UIManager.Instance.FindObjectByName("PlayerFollowCamera").GetComponent<FollowPlayerCamera>().SetTagToFollow("Player");
            } else {
                // Update phase 3
            }
        }
    }

    public void applyCamShake(float force = 1f) {
        this.applyElementShake(rocketParent, force);
        this.applyElementShake(background, force * 0.3f);
    }

    public void applyElementShake(GameObject obj, float force = 1) {
        float dx = force * Random.Range(-1f, 1f);
        float dy = force * Random.Range(-1f, 1f);
        obj.transform.localPosition = new Vector3(dx, dy, 0f);
    }
}
