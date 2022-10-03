using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameLifeStates {
    StartScreen,
    Running,
    EndScreen
}

public class GameStateManager : MonoBehaviour
{

    public GameStateManager Instance;

    private string[] activeStates = { "" };

    void Awake() {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
