using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public Text nameText;
    public Text dialogueText;
    private Queue<string> names;
    private Queue<string> sentences;
    public Animator animator;
    public bool inDialogue = false;
    private Coroutine typingRoutine;
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        names = new Queue<string>();
        ResetButtons();
    }

    private void ResetButtons()
    {
        buttons[2].onClick.RemoveAllListeners();
        buttons[2].onClick.AddListener(() => { displayNextSentence(); });
        buttons[1].gameObject.SetActive(false);
        buttons[0].gameObject.SetActive(false);
        buttons[2].GetComponentInChildren<Text>().text = "Continue";
    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);
        nameText.text = dialogue.name;
        sentences.Clear();

        foreach(Sentence sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence.text);
        }
        //Debug.Log(sentences.Count);
        displayNextSentence();
        inDialogue = true;
    }
    public void displayNextSentence()
    {
        //Debug.Log("displayNextSentence");
        //if dialog tab is open
        if (animator.GetBool("IsOpen"))
        {
            if (sentences.Count > 0)
            {
                string sentence = sentences.Dequeue();
                if (typingRoutine != null)
                    StopCoroutine(typingRoutine);
                typingRoutine = StartCoroutine(TypeSentence(sentence));
            }
            else
            {
                EndDialogue();
            }
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }
    
    public void EndDialogue()
    {
        inDialogue = false;
        ResetButtons();
        animator.SetBool("IsOpen", false);
    }


    [SerializeField] private Button[] buttons = new Button[3];
    int currentSentence = 0;


    private void SetButtonsForDialogue(SpecialDialogue sd, int a)
    {
        for (int i = 0; i < 3; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < sd.dialogue[a].answers.Length; i++)
        {
            int id = sd.dialogue[a].answers.Length - i - 1;
            buttons[2 - i].onClick.RemoveAllListeners();
            buttons[2 - i].onClick.AddListener(() => { Answer(id, sd); });
            buttons[2 - i].GetComponentInChildren<Text>().text = sd.dialogue[a].answers[id].text;
            buttons[2 - i].gameObject.SetActive(true);
        }
        dialogueText.text = sd.dialogue[a].text;
    }
    public void StartSpecialDialogue(SpecialDialogue dialogue)
    {
        animator.SetBool("IsOpen", true);
        nameText.text = dialogue.name;

        // Setting up buttons
        SetButtonsForDialogue(dialogue, 0);
        currentSentence = 0;
    }

    private void Answer(int answer, SpecialDialogue dialogue)
    {
        int next = dialogue.dialogue[currentSentence].answers[answer].next;
        //Debug.Log("Answer: " + answer + " Next: " + next);
        if (next < 0)
        {
            //Debug.Log("Negative : " + next);
            ResetButtons();
            EndDialogue();
            dialogue.onDialogueEnd.Invoke(next);
            //Debug.Log("End of the dialogue");
        }
        else
        {
            //Debug.Log("Positive : " + next);
            // Setting up buttons
            SetButtonsForDialogue(dialogue, next);
            currentSentence = next;
        }
    }

}
