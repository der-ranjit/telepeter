using UnityEngine;
using TMPro;
using System.Collections;

public class DialogManager : MonoBehaviour
{

    public static DialogManager Instance;

    public float dialogOpenTimeAfterClipFinished = 1.5f;
    public float autocloseDistance = 5f;

    private GameObject dialogPanel;
    private TextMeshProUGUI dialogPanelNameTextMesh;
    private TextMeshProUGUI dialogPanelDialogTextMesh;

    private GameObject trackedGameObject = null;
    private Coroutine autoCloseCoroutine = null;


    void Awake() {
        Instance = this;
    }

    void Start() {
        GameObject dialogPanel = UIManager.Instance.FindObjectByTag("DialogPanel");
        if (dialogPanel != null) {
            this.dialogPanel = dialogPanel;
            dialogPanelNameTextMesh = dialogPanel.transform.Find("Name")?.GetComponent<TextMeshProUGUI>();
            dialogPanelDialogTextMesh = dialogPanel.transform.Find("Dialog")?.GetComponent<TextMeshProUGUI>();
        }
    }

    void Update() {
        if (trackedGameObject != null) {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null && Vector3.Distance(player.transform.position, trackedGameObject.transform.position) > autocloseDistance) {
                CloseDialog();
            }
        }
    }

    public void TriggerDialog(string name, string text, GameObject gameObject) {
        CloseDialog();
        if (text.Length > 0) {
            autoCloseCoroutine = StartCoroutine(TriggerAndAutocloseDialog(name, text, gameObject));
        }
    }
    
    public void TriggerDialogWithRandomText(string name, string[] texts, GameObject gameObject) {
        if (texts.Length > 0) {
            string text = texts[Random.Range(0, texts.Length)];
            TriggerDialog(name, text, gameObject);
        }
    }


    private IEnumerator TriggerAndAutocloseDialog(string name, string text, GameObject gameObject) {
        if (dialogPanel != null && dialogPanelDialogTextMesh != null && dialogPanelNameTextMesh != null) {
            trackedGameObject = gameObject;
            dialogPanelNameTextMesh.text = name;
            dialogPanelDialogTextMesh.text = text;
            UIManager.Instance.FadeCanvasGroup(dialogPanel.GetComponent<CanvasGroup>(), true);
            yield return Animalesiator.Instance.Animalesiatize(text);
            yield return new WaitForSeconds(dialogOpenTimeAfterClipFinished);
            CloseDialog();
        }
        yield return null;
    }

    private void CloseDialog() {
        if (autoCloseCoroutine != null) {
            StopCoroutine(autoCloseCoroutine);
        }
        trackedGameObject = null;
        dialogPanelNameTextMesh.text = "";
        dialogPanelDialogTextMesh.text = "";
        UIManager.Instance.FadeCanvasGroup(dialogPanel.GetComponent<CanvasGroup>(), false);
    }

   
}
