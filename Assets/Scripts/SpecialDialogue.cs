using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum SpecialPassengerType
{
    MIGRANT,
    RESISTANTE,
    PROPARTI
}

// A fournir a un passenger special
public class SpecialDialogue
{
    public string name = "";
    public SpecialPassengerType specialType;

    [Serializable]
    public struct Answer
    {
        public string text;
        public int next;
    }

    [Serializable]
    public struct Answers
    {
        public Answer[] answers;
    }

    [Serializable]
    public struct Entry
    {
        public int id;
        public string text;
        public Answer[] answers;
        // 3 at most I think
    }

    [Serializable]
    public struct Entries
    {
        public Entry[] entries;
    }

    public Dictionary<int, Entry> dialogue;

    public void Init(string name, SpecialPassengerType type)
    {
        this.name = name;
        specialType = type;
        dialogue = new Dictionary<int, Entry>();
        TextAsset asset;
        switch (specialType)
        {
            case SpecialPassengerType.MIGRANT:
                asset = Resources.Load<TextAsset>("migrant_dialog");
                break;
            case SpecialPassengerType.RESISTANTE:
                asset = Resources.Load<TextAsset>("resistante_dialog");
                break;
            case SpecialPassengerType.PROPARTI:
                asset = Resources.Load<TextAsset>("proparti_dialog");
                break;
            default:
                Debug.LogError("Special type not found");
                throw new System.Exception("Special type not found");
        }

        Entries entryList = JsonUtility.FromJson<Entries>(asset.text);
        foreach(Entry entry in entryList.entries)
        {
            dialogue.Add(entry.id, entry);
            //Debug.LogWarning("### Init special dialogue : " + entry.id + " " + entry.text);
            foreach(Answer answer in entry.answers)
            {
                //Debug.LogWarning(" ---> Init special dialogue : " + answer.text + " " + answer.next);
            }
        }
    }
}
