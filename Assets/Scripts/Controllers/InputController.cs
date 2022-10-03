using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputController : ScriptableObject
{
    public abstract float RetrieveMoveInput();

    public abstract bool RetrieveJumpInput();

    public abstract bool RetrieveJumpInputDown();

    public abstract bool RetrieveJumpInputUp();

    public abstract bool RetrieveUseButton();

    public abstract bool RetrieveUseButtonUp();

    public abstract bool RetrieveUseButtonDown();
}
