using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


namespace MPack
{
    public class DemoDialogueInterpreter : AbstractDialogueInterpreter
    {
        public static DemoDialogueInterpreter ins;
        public Text questionText;
        public GameObject choiceTextPrefab;
        public GameObject nextDialogue;
        public float offsetY;
        public VariableStorage varibleStorageSystem;

        private List<GameObject> aliveChoices;


        private DialogueGraph dialogueGraph;

        private void Start() {
            ins = this;

            aliveChoices = new List<GameObject>();
            varibleStorageSystem ??= ScriptableObject.CreateInstance<VariableStorage>();
            gameObject.SetActive(false);
        }

        public bool StartDialogue(DialogueGraph graph)
        {
            if (dialogueGraph != null) return false;

            gameObject.SetActive(true);

            dialogueGraph = graph;
            dialogueGraph.SetUp(this);
            dialogueGraph.Start();
            dialogueGraph.Proccessing();
            return true;
        }

        public override void ChangeToQuestion(QuestionNode node)
        {
            CleanUpLastNode();

            questionText.text = node.content;

            for (int i = 0; i < node.choices.Length; i++)
            {
                GameObject newChoiceButton = Instantiate(choiceTextPrefab, choiceTextPrefab.transform.parent);
                newChoiceButton.SetActive(true);

                RectTransform transform = newChoiceButton.GetComponent<RectTransform>();
                transform.anchoredPosition += new Vector2(0, offsetY * i);

                Text uiText = newChoiceButton.GetComponentInChildren<Text>();
                uiText.text = node.choices[i].content;

                Button button = newChoiceButton.GetComponent<Button>();
                int index = i;
                button.onClick.AddListener(delegate {
                    ChoiceButtonClicked(node, index);
                });

                aliveChoices.Add(newChoiceButton);
            }
        }

        public override void ChangeToDialogue(DialogueNode node)
        {
            CleanUpLastNode();

            questionText.text = node.content;
            nextDialogue.SetActive(true);
        }

        public override void OnDialogueEnd()
        {
            dialogueGraph.TearDown();
            dialogueGraph = null;
            gameObject.SetActive(false);
        }

        private void CleanUpLastNode()
        {
            for (int i = 0; i < aliveChoices.Count; i++)
            {
                Destroy(aliveChoices[i]);
            }
            nextDialogue.SetActive(false);
        }

        public void ChoiceButtonClicked(QuestionNode node, int index)
        {
            // dialogueGraph.JumpToNode((AbstractNode)node.choices[index].port.Connection.node);
            ((QuestionNode)dialogueGraph.currentNode).MakeChoice(index);
            dialogueGraph.Proccessing();
        }

        public void NextDialogue()
        {
            dialogueGraph.Proccessing();

            // if (dialogueGraph.currentNode != null)
            // {
            //     dialogueGraph.Proccessing();
            // }
            // else
            // {
            //     OnDialogueEnd();
            // }
        }
    }
}