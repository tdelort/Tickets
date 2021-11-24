using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Dialogue
{
    public string name;
    [TextArea(3, 10)]
    public Sentence[] sentences;

    //for now, setDialogue clear sentences and create a new random one of the goos type
    public void SetDialogue(Sentence.SentenceType stype)
    {
        sentences = new Sentence[1];
        sentences.SetValue(Sentences.getRandSentence(stype),0);
    }
}

[System.Serializable]
public class Sentence
{
    public enum SentenceType
    {
        GREETING,           //0
        TRUEPLEAD,          //1
        NOPERMITPLEAD,      //2
        NOIDPLEAD,          //3
        NOATTESTATIONPLEAD, //4
        FALSETICKETPLEAD,   //5
        FALSESUBPLEAD,      //6
        FALSEIDPLEAD,       //7
        FALSEATTESTATION    //8
    }

    public SentenceType type;
    public String text;
}

[Serializable]
public class SentenceList
{
    public Sentence[] sentences;
}

public static class Sentences
{
    private static List<List<Sentence>> sentences;

    public static Sentence getRandSentence(Sentence.SentenceType stype)
    {
        if (sentences == null)
        {
            sentences = new List<List<Sentence>>();

            //get all sentences
            TextAsset textAsset = Resources.Load<TextAsset>("dialog");
            SentenceList rawsentencesList = JsonUtility.FromJson<SentenceList>(textAsset.text);
           
            //create all list sorted by type
            foreach (Sentence.SentenceType type in Enum.GetValues(typeof(Sentence.SentenceType)))
            {
                sentences.Insert((int)type, new List<Sentence>());
            }
           
            //sort sentences
            foreach (Sentence s in rawsentencesList.sentences)
            {
                sentences[(int)s.type].Add(s);
            }
        }
        int i = UnityEngine.Random.Range(0, sentences[(int)stype].Count);
        return sentences[(int)stype][i];
    }
}


