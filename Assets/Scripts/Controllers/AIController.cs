using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "AIController", menuName = "InputController/AIController")]
public class AIController : InputController
{

    public override bool RetrieveJumpInput()
    {
        return true;
    }

    public override float RetrieveMoveInput()
    {
        return 1f;
    }

    public override bool RetrieveJumpInputDown()
    {
        return true;
    }

    public override bool RetrieveJumpInputUp()
    {
        return true;
    }

    public override bool RetrieveUseButton()
    {
        return true;
    }

    public override bool RetrieveUseButtonUp()
    {
        return true;
    }

    public override bool RetrieveUseButtonDown()
    {
        return true;
    }
}
