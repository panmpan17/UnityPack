using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem
{
    public abstract class AbstractDialogueInterpreter : MonoBehaviour
    {
        public abstract void ChangeToQuestion(QuestionNode node);
        public abstract void ChangeToDialogue(DialogueNode node);
        public abstract void OnDialogueEnd();
    }
}