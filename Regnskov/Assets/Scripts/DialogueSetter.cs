using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSetter : MonoBehaviour
{

    DialogueController dialogueController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        
        dialogueController = Resources.FindObjectsOfTypeAll<DialogueController>()[0];

        dialogueController.SetInstance();

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
