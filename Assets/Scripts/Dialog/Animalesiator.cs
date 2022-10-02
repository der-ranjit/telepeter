using System.Collections;
using UnityEngine;

public class Animalesiator : MonoBehaviour
{
    public static Animalesiator Instance;

    private object[] clips;

    private AudioSource audioSource;

    void Awake() {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null) {
            clips = Resources.LoadAll("audio/alphabet", typeof(AudioClip));
        }
    }

    void Start() {
        // Animalesiatize("HEYOOO ZEIG MAL HERH");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Animalesiatize(string text) {
        StartCoroutine(Vocalize(text));
    }

    private IEnumerator Vocalize(string text) {
        foreach(char character in text) {
            var clip = GetCharacterClip(char.ToLower(character));
            if (clip != null) {
                audioSource.clip = clip;
                audioSource.Play();
                yield return new WaitForSeconds(clip.length);
            } else {
                yield return new WaitForSeconds(0.3f);
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
