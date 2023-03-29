using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueBox : MonoBehaviour
{
    public Text dialogueText;
    [TextArea(3, 10)]
    public string[] dialogues;
    bool inPresence;
    int currentLine = 0;

    

    //event that happens on trigger
    private void OnTriggerEnter(Collider other)
    {
   
        //if player is standing in front of the NPC
        inPresence = true;

    }

    //event that happens when you leave the collider
    private void OnTriggerExit(Collider other)
    {
        dialogueText.text = "";
        inPresence = false;
    }


    private void Update()
    {
        //if the E button is pressed down and inPresence is true, the dialogue will pop up. 
        if (Input.GetKeyDown(KeyCode.E) && inPresence)
        {
            //If there is dialogue left, it will be displayed.
            if (currentLine < dialogues.Length)
            {
                //dialogue is equal to the current line that's displayed
                dialogueText.text = dialogues[currentLine];
                currentLine++;
            }

            //no more dialogue, dialogue will be reset and start from the beginning
            else
            {
                currentLine = 0;
                dialogueText.text = "";
            }

        }
    }
}