using TMPro;
using UnityEngine;

public class UiCountdownText : MonoBehaviour
{
    public int maxTextLineLength = 6;

    private TextMeshProUGUI textMesh;
    private Telepeter telepeter;

    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        telepeter = Telepeter.Instance;
        if (telepeter == null) {
            Debug.Log("[UICountdownText] - missing telepeter :(");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (textMesh != null && telepeter != null) {
            string countdownText = TruncateString(telepeter.remainingTime.ToString(), maxTextLineLength);
            string iterationText = TruncateString(telepeter.currentIteration.ToString(), maxTextLineLength);
            textMesh.text = countdownText + "\n" + iterationText;
        }
    }


    private string TruncateString(string input, int maxLength) {
        if(input.Length > maxLength) {
            return input.Substring(0, maxLength);
        }
        return input;
    }
}
