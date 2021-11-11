using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class Passenger : MonoBehaviour
{
    double ticketProb = 0.5;
    double PassportProb = 1;


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
        passengerAge = UnityEngine.Random.Range(15, 85);
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
            double coin = coinFlip();
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
            double coin = coinFlip();
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

        double rand = coinFlip();
        if (rand < ticketProb)
        {
            ticket = new Ticket(true, GameData.GameTime,1);
            subscription = new Subscription(false, passengerName,
                passengerSurname, GameData.GameTime, 365);
        }
        else
        {
            ticket = new Ticket(false, GameData.GameTime, 1);
            subscription = new Subscription(true, passengerName,
                passengerSurname, GameData.GameTime, 365);
        }
    }
    private void setId()
    {
        double rand = coinFlip();
        if (rand < PassportProb)
        {
            passeport = new Passeport(true, passengerName,
                passengerSurname, passengerAge, GameData.GameTime, 5);
            id = new ID(false, passengerName, passengerSurname,
                passengerAge, GameData.GameTime, 15);
        }
        else
        {
            passeport = new Passeport(false, passengerName,
                passengerSurname, passengerAge, GameData.GameTime, 5);
            id = new ID(true, passengerName, passengerSurname,
                passengerAge, GameData.GameTime, 15);
        }
    }
    private void setAutorisation()
    {
        autorisation = new Autorisation(true, passengerName,
            passengerSurname, GameData.GameTime, 5);
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

    private double coinFlip()
    {
        return UnityEngine.Random.Range(0, 1);
    }

    public struct Ticket
    {
        public bool present;
        public DateTime compostTime;
        //in hour
        public int validityPeriod;

        public string ToText()
        {
            return compostTime.ToString() + " \n validity periode : " + validityPeriod;
        }
        public Ticket(bool present, DateTime compostTime, int validityPeriod)
        {
            this.present = present;
            this.compostTime = compostTime;
            this.validityPeriod = validityPeriod;
        }
    };
    public struct Subscription
    {
        public bool present;
        public string name;
        public string surname;
        public DateTime deliveryTime;
        //in day
        public int validityPeriod;

        public string ToText() 
        {
            return name + " " + surname + "\n" +
                deliveryTime.ToString() + " \n validity periode : " + validityPeriod;
        }
        public Subscription(bool present, string name, string surname,
            DateTime deliveryTime, int validityPeriod)
        {
            this.present = present;
            this.name = name;
            this.surname = surname;
            this.deliveryTime = deliveryTime;
            this.validityPeriod = validityPeriod;
        }
    };
    public struct Passeport
    {
        public bool present;
        public string name;
        public string surname;
        public int age;
        public DateTime deliveryTime;
        //in year
        public int validityPeriod;
        public string ToText()
        {
            return name + " " + surname + " - " + age + "\n" +
                deliveryTime.ToString() + " \n validity periode : " + validityPeriod;
        }
        public Passeport(bool present, string name, string surname, int age,
            DateTime deliveryTime, int validityPeriod)
        {
            this.present = present;
            this.name = name;
            this.surname = surname;
            this.age = age;
            this.deliveryTime = deliveryTime;
            this.validityPeriod = validityPeriod;
        }
    };
    public struct ID
    {
        public bool present;
        public string name;
        public string surname;
        public int age;
        public DateTime deliveryTime;
        //in year
        public int validityPeriod;
        public string ToText()
        {
            return name + " " + surname + " - " + age + "\n" +
                deliveryTime.ToString() + " \n validity periode : " + validityPeriod;
        }
        public ID(bool present, string name, string surname, int age,
            DateTime deliveryTime, int validityPeriod)
        {
            this.present = present;
            this.name = name;
            this.surname = surname;
            this.age = age;
            this.deliveryTime = deliveryTime;
            this.validityPeriod = validityPeriod;
        }
    };
    public struct Autorisation
    {
        public bool present;
        public string name;
        public string surname;
        public DateTime deliveryTime;
        //in hour
        public int validityPeriod;


        public string ToText()
        {
            return name + " " + surname + "\n" +
                deliveryTime.ToString() + " \n validity periode : " + validityPeriod;
        }
        public Autorisation(bool present, string name,
            string surname, DateTime deliveryTime, int validityPeriod)
        {
            this.present = present;
            this.name = name;
            this.surname = surname;
            this.deliveryTime = deliveryTime;
            this.validityPeriod = validityPeriod;
        }
    };
}



public static class nameGenerator
{
    private static string[] names;

    public static string getRandName()
    {
        if (names==null)
        {
            names = new string[18239+1];
            string path = "Assets/Resources/names.txt";
            StreamReader reader = new StreamReader(path);
            string data = reader.ReadToEnd();
            names = data.Split(char.Parse("\n"));
            reader.Close();
        }
        int i = UnityEngine.Random.Range(0, 18239);
        return names[i];
    }

}