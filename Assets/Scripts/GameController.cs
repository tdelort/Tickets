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
                Year = 1999;
                Month = 01;
                Day = 15;
                Hour = 9;
                Minute = 32;
                Seconde = 0;
                break;


            case 1:
                Year = 1999;
                Month = 05;
                Day = 22;
                Hour = 10;
                Minute = 43;
                Seconde = 0;
                break;


            case 2:
                Year = 1999;
                Month = 12;
                Day = 31;
                Hour = 18;
                Minute = 10;
                Seconde = 0;
                break;

            case 3:
                Year = 2000;
                Month = 09;
                Day = 1;
                Hour = 21;
                Minute = 45;
                Seconde = 0;
                break;
        }
    }

    public static int Year { get; set; }
    public static int Month { get; set; }
    public static int Day { get; set; }
    public static int Hour { get; set; }
    public static int Minute { get; set; }
    public static int Seconde { get; set; }
    public static int NbPassenger { get; set; }
    public static int NbNotInOrderPassenger { get; set; }
    public static int NbIlegalActionPassenger { get; set; }
}

