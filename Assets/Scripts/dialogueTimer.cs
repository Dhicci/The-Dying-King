using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogueTimer : MonoBehaviour
{
    [SerializeField] dialogueManager theDialogueManager;
    public void Skip()
    {
        theDialogueManager.TimerEnd();
    }
}
