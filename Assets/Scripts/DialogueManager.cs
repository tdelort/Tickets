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
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        names = new Queue<string>();

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
        displayNextSentence();
    }
    public void displayNextSentence()
    {
        //if dialog tab is open
        if (animator.GetBool("IsOpen"))
        {
            if (sentences.Count > 0)
            {
                string sentence = sentences.Dequeue();
                StopAllCoroutines();
                StartCoroutine(TypeSentence(sentence));
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
        animator.SetBool("IsOpen", false);
    }
}
