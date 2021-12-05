using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int level = 0;
    public int score = 0;
    public int migrantAlignment = 0;
    public int propartiAlignment = 0;
    public int resistanteAlignment= 0;

    public SaveData(int level, int score, int migrantAlignment, int propartiAlignment, int resistanteAlignment)
    {
        this.level = level;
        this.score = score;
        this.migrantAlignment = migrantAlignment;
        this.propartiAlignment = propartiAlignment;
        this.resistanteAlignment = resistanteAlignment;
    }

    public SaveData()
    {
        this.level = 0;
        this.score = 0;
        this.migrantAlignment = 0;
        this.propartiAlignment = 0;
        this.resistanteAlignment = 0;
    }
}
