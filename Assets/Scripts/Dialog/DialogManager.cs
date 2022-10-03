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
        GameObject dialogPanel = FindDialogPanel();
        if (dialogPanel != null) {
            this.dialogPanel = dialogPanel;
            dialogPanelNameTextMesh = dialogPanel.transform.Find("Name")?.GetComponent<TextMeshProUGUI>();
            dialogPanelDialogTextMesh = dialogPanel.transform.Find("Dialog")?.GetComponent<TextMeshProUGUI>();
            CloseDialog();
        }
    }

    void Update() {
        if (trackedGameObject != null) {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Debug.Log(Vector3.Distance(player.transform.position, trackedGameObject.transform.position));
            if (player != null && Vector3.Distance(player.transform.position, trackedGameObject.transform.position) > autocloseDistance) {
                CloseDialog();
            }
        }
    }

    public void TriggerDialog(string name, string text, GameObject gameObject) {
        CloseDialog();
        autoCloseCoroutine = StartCoroutine(TriggerAndAutocloseDialog(name, text, gameObject));
    }


    private IEnumerator TriggerAndAutocloseDialog(string name, string text, GameObject gameObject) {
        if (dialogPanel != null && dialogPanelDialogTextMesh != null && dialogPanelNameTextMesh != null) {
            trackedGameObject = gameObject;
            dialogPanelNameTextMesh.text = name;
            dialogPanelDialogTextMesh.text = text;
            dialogPanel.SetActive(true);
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
        dialogPanel.SetActive(false);
        dialogPanelNameTextMesh.text = "";
        dialogPanelDialogTextMesh.text = "";
    }

    private GameObject FindDialogPanel() {
        foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[]) {
            if (go.tag == "DialogPanel") {
                return go;
            }
        }
        return null;
    }
}
