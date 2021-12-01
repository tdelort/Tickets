using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int level = 0;
    public int score = 0;

    public SaveData(int level, int score)
    {
        this.level = level;
        this.score = score;
    }

    public SaveData()
    {
        this.level = 0;
        this.score = 0;
    }
}
