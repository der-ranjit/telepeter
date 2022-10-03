using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance;

    void Awake() {
        Instance = this;
    }

    // finds a game object by name; also detects disabled game obejects (useful for ui stuff since it is annoying in the editor view)
    public GameObject FindObjectByName(string objectName) {
        foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[]) {
            if (go.name == objectName) {
                return go;
            }
        }
        return null;
    }

    // finds a game object by tag; also detects disabled game obejects (useful for ui stuff since it is annoying in the editor view)
    public GameObject FindObjectByTag(string objectTag) {
        foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[]) {
            if (go.tag == objectTag) {
                return go;
            }
        }
        return null;
    }

    public void FadeCanvasGroup(CanvasGroup canvasGroup, bool fadeIn, float fadeTime = 0.1f) {
        if (canvasGroup != null) {
            canvasGroup.interactable = fadeIn;
            canvasGroup.blocksRaycasts = fadeIn;
            StartCoroutine(Fade(canvasGroup, canvasGroup.alpha, fadeIn ? 1 : 0, fadeTime));
        }
    }

    private IEnumerator Fade(CanvasGroup canvasGroup, float start, float end, float duration) {
        float counter = 0;
        while (counter < duration) {
            counter += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, counter / duration);
            yield return null;
        }
    }

}
