using System.Collections;
using UnityEngine;

public class Animalesiator : MonoBehaviour
{
    public static Animalesiator Instance;

    public float minPitch = 1.3f;
    public float maxPitch = 1.7f;
    public float specialCharacterPause = 0.075f;
    public float soundLength = 0.5f;

    private object[] clips;

    private AudioSource audioSource;

    void Awake() {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null) {
            clips = Resources.LoadAll("audio/alphabet", typeof(AudioClip));
        }
    }

    public Coroutine Animalesiatize(string text, bool lowVoice = false) {
        if (audioSource != null && clips.Length > 0) {
            return StartCoroutine(Vocalize(text, lowVoice));
        }
        return null;
    }

    private IEnumerator Vocalize(string text, bool lowVoice = false) {
        foreach(char character in text) {
            var clip = GetCharacterClip(char.ToLower(character));
            if (clip != null) {
                float randomPitch = Random.Range(minPitch * (lowVoice ? .6f : 1f), maxPitch * (lowVoice ? .6f : 1f));
                audioSource.pitch = randomPitch;
                audioSource.clip = clip;
                audioSource.Play();
                yield return new WaitForSeconds(clip.length / audioSource.pitch * (lowVoice ? soundLength / 2 : 1f));
            } else {
                yield return new WaitForSeconds(specialCharacterPause);
            }
        }
    } 

    private AudioClip GetCharacterClip(char character) {
        foreach(object clip in clips) {
            AudioClip audioClip = (AudioClip)clip;
            if (audioClip.name == character.ToString()) {
                return audioClip;
            }
        }
        return null;
    }
}
