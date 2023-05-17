using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDialogueTrigger : MonoBehaviour
{

    public bool mousePointed = false;

    private void OnMouseEnter()
    {
        mousePointed= true;
        print(mousePointed);

    }

    private void OnMouseExit()
    {
        mousePointed = false;
    }

}
