using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum GameLifeStates {
    StartScreen,
    ShowControls,
    Running,
    EndScreen
}

public class GameStateManager : MonoBehaviour
{

    public static GameStateManager Instance;

    // only use this in the editor for testing purposes
    public GameLifeStates currentGameLifeState = GameLifeStates.StartScreen;

    private readonly List<string> activeStates = new();

    void Awake() {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialize(currentGameLifeState);
    }

    public void Initialize(GameLifeStates state) {
        currentGameLifeState = state;
        switch (currentGameLifeState)
        {
            case GameLifeStates.StartScreen:
                OpenStartScreen();
                break;
            case GameLifeStates.Running:
                StartGame();
                break;
            default:
                break;
        }
    }

    public bool HasStates(string[] states) {
        foreach(string state in states) {
            if(!activeStates.Contains(state)) {
                return false;
            }
        }
        return true;
    }

    public void AddStates(string[] states) {
        activeStates.AddRange(states);
    }

    public void SetStates(string[] states) {
        activeStates.Clear();
        activeStates.AddRange(states);
    }

    public void RemoveStates(string[] states) {
        foreach(string state in states) {
            if(activeStates.Contains(state)) {
                activeStates.Remove(state);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentGameLifeState)
        {
            case GameLifeStates.StartScreen:
                break;
            default:
                break;
        }
    }

    private void OpenStartScreen() {
        var startScreen = UIManager.Instance.FindObjectByName("StartScreen");
        UIManager.Instance.FadeCanvasGroup(startScreen.GetComponent<CanvasGroup>(), true, 0.1f);
        var test = startScreen.transform.Find("Button").GetComponent<UnityEngine.UI.Button>();
        Debug.Log(test);
        test.onClick.AddListener(() => EndStartScreen());
    }

    private void EndStartScreen() {
        var startScreen = UIManager.Instance.FindObjectByName("StartScreen");
        UIManager.Instance.FadeCanvasGroup(startScreen.GetComponent<CanvasGroup>(), false, 0.1f);
        this.Initialize(GameLifeStates.Running);
    }

    private void StartGame() {
        // EndStartScreen();
        UIManager.Instance.FindObjectByTag("PlayerSpawner").GetComponent<PlayerSpawner>().Spawn();
    }
}
