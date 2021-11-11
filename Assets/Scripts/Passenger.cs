using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Passenger : MonoBehaviour
{
    double ticketProb = 0.5;
    double PassportProb = 1;
    //in minutes
    int ticketValidity = 60;
    //in day
    int subscriptionValidity = 360;
    //in year
    int passeportValidity = 5;
    int idValidity = 15;


    public string passengerName;
    public string passengerSurname;
    public int passengerAge;

    public Ticket ticket;
    public Subscription subscription;

    public Passeport passeport;
    public ID id;

    public Autorisation autorisation;

    public bool smoking = false;
    public bool bulky = false;
    public bool mustBeGreen = false;
    public bool mustBeNotGreen = false;

    public void Init(bool inOrder, bool illegal)
    {
        passengerName = nameGenerator.getRandName();
        passengerSurname = nameGenerator.getRandName();
        passengerAge = Random.Range(15, 85);
        int level = GameData.getCurrentLevel();
        setPermit();
        if (level > 0)
        {
            setId();
        }
        if (level > 1)
        {
            setAutorisation();
            mustBeNotGreen = true;
        }

        if (!inOrder)
        {
            float coin = coinFlip();
            switch (level)
            {
                case 0:
                    makePermitFalse();
                    break;


                case 1:
                    if (coin < 0.5)
                    {
                        makePermitFalse();
                    }
                    else
                    {
                        makeIdFalse();
                    }
                    break;


                default:
                    if (coin < 0.3)
                    {
                        makePermitFalse();
                    }
                    else if (coin < 0.6)
                    {
                        makeIdFalse();
                    }
                    else
                    {
                        makeAutorisationFalse();
                    }
                    break;
            }
        }

        if (illegal)
        {
            float coin = coinFlip();
            switch (level)
            {
                case 0:
                    smoking = true;
                    break;


                case 1:
                    if (coin < 0.5)
                    {
                        smoking = true;
                    }
                    else
                    {
                        bulky = true;
                    }
                    break;


                default:
                    if (coin < 0.3)
                    {
                        smoking = true;
                    }
                    else if (coin < 0.6)
                    {
                        bulky = true;
                    }
                    else
                    {
                        mustBeGreen = true;
                    }
                    break;
            }
        }
    }
    
    private void setPermit()
    {

        float rand = Random.Range(0, 1);
        if (rand < ticketProb)
        {
            ticket = new Ticket(true, 10, 30,
                GameData.Day, GameData.Month, GameData.Year);
            subscription = new Subscription(false, passengerName,
                passengerSurname, 01, 01,
                1999);
        }
        else
        {
            ticket = new Ticket(false, 10, 10, GameData.Day,
                GameData.Month, GameData.Year);
            subscription = new Subscription(true, passengerName,
                passengerSurname, 10, 10,
                1000);
        }
    }
    private void setId()
    {
        float rand = Random.Range(0, 1);
        if (rand < PassportProb)
        {
            passeport = new Passeport(true, passengerName,
                passengerSurname, passengerAge, 30, 2, 2017);
            id = new ID(false, passengerName, passengerSurname,
                passengerAge, 30, 2, 2017);
        }
        else
        {
            passeport = new Passeport(false, passengerName,
                passengerSurname, passengerAge, 30, 2, 2017);
            id = new ID(true, passengerName, passengerSurname,
                passengerAge, 30, 2, 2017);
        }
    }
    private void setAutorisation()
    {
        autorisation = new Autorisation(true, passengerName,
            passengerSurname, passengerAge, 30, 2, 2017);
    }

    private void makePermitFalse()
    {
        if (coinFlip() < 0.5)
        {

        }
        else
        {
            passeport.present = false;
            id.present = false;
        }
    }
    private void makeIdFalse()
    {
        if (coinFlip() < 0.5)
        {
            
        }
        else
        {
            subscription.present = false;
            ticket.present = false;
        }
    }
    private void makeAutorisationFalse()
    {
        if (coinFlip() < 0.5)
        {

        }
        else
        {
            autorisation.present = false;
        }
    }

    private float coinFlip()
    {
        return Random.Range(0, 1);
    }

    public struct Ticket
    {
        public bool present;
        public int minute, hour, day, month, year;

        public string ToText()
        {
            return '\n' + day.ToString() + '-' + month.ToString() +
    '-' + year.ToString() + ':' + hour.ToString() + ':' +
    minute.ToString();
        }
        public Ticket(bool present, int minute, int hour, int day,
            int month,
            int year)
        {
            this.present = present;
            this.minute = minute;
            this.hour = hour;
            this.day = day;
            this.month = month;
            this.year = year;
        }
    };
    public struct Subscription
    {
        public bool present;
        public string name;
        public string surname;
        public int day, month, year;

        public string ToText() { return name + '\n' + day + '-' +
                month + '-' + year; }
        public Subscription(bool present, string name, string surname,
            int day, int month, int year)
        {
            this.present = present;
            this.name = name;
            this.surname = surname;
            this.day = day;
            this.month = month;
            this.year = year;
        }
    };
    public struct Passeport
    {
        public bool present;
        public string name;
        public string surname;
        public int age;
        public int day, month, year;
        public string ToText() { return name + ' ' + surname +
                '\n' + day + '-' + month + '-' + year; }
        public Passeport(bool present, string name, string surname,
            int age, int day, int month, int year)
        {
            this.present = present;
            this.name = name;
            this.surname = surname;
            this.age = age;
            this.day = day;
            this.month = month;
            this.year = year;
        }
    };
    public struct ID
    {
        public bool present;
        public string name;
        public string surname;
        public int age;
        public int day, month, year;
        public string ToText() { return name + ' ' + surname +
                '\n' + day + '-' + month + '-' + year; }
        public ID(bool present, string name, string surname,
            int age, int day, int month, int year)
        {
            this.present = present;
            this.name = name;
            this.surname = surname;
            this.age = age;
            this.day = day;
            this.month = month;
            this.year = year;
        }
    };
    public struct Autorisation
    {
        public bool present;
        public string name;
        public string surname;
        public int duration;
        public int day, month, year;


        public string ToText()
        {
            return name + ' ' + surname + '\n' + day + '-' +
    month + '-' + year + '-' + duration;
        }
        public Autorisation(bool present, string name, string surname,
            int duration,
            int day, int month, int year)
        {
            this.present = present;
            this.name = name;
            this.surname = surname;
            this.duration = duration;
            this.day = day;
            this.month = month;
            this.year = year;

        }
    };
}



public static class nameGenerator
{
    private static string[] names;

    public static string getRandName()
    {
        if (names.Length < 1)
        {
            string path = "Assets/Resources/names.txt";
            StreamReader reader = new StreamReader(path);
            string data = reader.ReadToEnd();
            names = data.Split(char.Parse("\n"));
            reader.Close();
            Debug.Log(names);
        }
        int i = Random.Range(0, 18239);
        return names[i];
    }

}