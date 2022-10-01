using TMPro;
using UnityEngine;

public class UiCountdownText : MonoBehaviour
{

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
            textMesh.text = telepeter.remainingTime.ToString(); 
        }
    }
}
