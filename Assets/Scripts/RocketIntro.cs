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

    private GameObject rocket1;
    private GameObject rocket2;
    private GameObject rocket3;

    private float initialY;
    public float targetY = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // story stuff at beginning
        rocket1 = UIManager.Instance.FindObjectByName("rocket_initial");
        rocket2 = UIManager.Instance.FindObjectByName("rocket_transformation");
        rocket3 = UIManager.Instance.FindObjectByName("crashed_rocket");
        initialY = this.gameObject.transform.position.y;
        Telepeter.Instance.gameObject.SetActive(false);
        UIManager.Instance.FindObjectByName("PlayerFollowCamera").GetComponent<FollowPlayerCamera>().SetTagToFollow("Rocket");
        DialogManager.Instance.TriggerDialog("Storyteller", "Heyooooooo", gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        float time = Time.time;

        if (currentPhase <= 3) {
            // Rocket position
            float p = Mathf.Min(time / (Phase1Duration + Phase2Duration), 1f);
            Vector3 pos = this.gameObject.transform.position;
            float y = initialY * (1 - p) + targetY * p;
            this.gameObject.transform.position = new Vector3(pos.x, y, pos.z);

            // Cam shake
            // TODO
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
                this.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotation));
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
}
