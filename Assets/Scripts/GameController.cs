using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{    public void startLevel(int level)
    {
        GameData.setCurrentLevel(level);
        SceneManager.LoadScene("Level");
    }
}

public static class GameData
{
    private static int currentLevel;

    public static int getCurrentLevel()
    {
        return currentLevel;
    }

    public static void setCurrentLevel(int level)
    {
    currentLevel = level;

    NbPassenger = 6;
    NbNotInOrderPassenger = 2;
    NbIlegalActionPassenger = 2;

        switch (level)
        {
            case 0:
                // year - month - day - hour - minute - second
                GameTime = new DateTime(1999, 02, 25, 9, 31, 00);
                break;


            case 1:
                GameTime = new DateTime(1999, 05, 22, 10, 43, 00);
                break;


            case 2:
                GameTime = new DateTime(1999, 12, 31, 18, 10, 00);
                break;

            case 3:
                GameTime = new DateTime(1999, 09, 01, 21, 45, 00);
                break;
        }
    }

    public static DateTime GameTime { get; set; }
    public static int NbPassenger { get; set; }
    public static int NbNotInOrderPassenger { get; set; }
    public static int NbIlegalActionPassenger { get; set; }
}

