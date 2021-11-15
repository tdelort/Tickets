using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class Passenger : MonoBehaviour
{
    double ticketProb = 0.5;
    double PassportProb = 1;

    //hour
    int ticketValidity = 1;
    int autValidity = 5;
    //day
    int subValidity = 365;
    //year
    int passValidity = 5;
    int idValidity = 5;


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

    public Dialogue dialogue;

    public void Init(bool inOrder, bool illegal)
    {
        passengerName = nameGenerator.getRandName();
        passengerSurname = nameGenerator.getRandName();
        passengerAge = UnityEngine.Random.Range(15, 85);
        dialogue.name = passengerName + " " + passengerSurname;
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

        setSprite();
    }
    
    private void setPermit()
    {
        
        DateTime texpiredTime = GameData.GameTime;
        texpiredTime.AddHours(UnityEngine.Random.Range(0, ticketValidity));

        
        DateTime sexpiredTime = GameData.GameTime;
        sexpiredTime.AddDays(UnityEngine.Random.Range(0, subValidity));

        double rand = coinFlip();
        if (rand < ticketProb)
        {
            ticket = new Ticket(true, texpiredTime, ticketValidity);
            subscription = new Subscription(false, passengerName,
                passengerSurname, sexpiredTime, subValidity);
        }
        else
        {
            ticket = new Ticket(false, texpiredTime, ticketValidity);
            subscription = new Subscription(true, passengerName,
                passengerSurname, sexpiredTime, subValidity);
        }
    }
    private void setId()
    {
        DateTime pexpiredTime = GameData.GameTime;
        pexpiredTime.AddYears(UnityEngine.Random.Range(0, passValidity));

        DateTime iexpiredTime = GameData.GameTime;
        iexpiredTime.AddYears(UnityEngine.Random.Range(0, idValidity));

        double rand = coinFlip();
        if (rand < PassportProb)
        {
            passeport = new Passeport(true, passengerName,
                passengerSurname, passengerAge, iexpiredTime, passValidity);
            id = new ID(false, passengerName, passengerSurname,
                passengerAge, iexpiredTime, idValidity);
        }
        else
        {
            passeport = new Passeport(false, passengerName,
                passengerSurname, passengerAge, iexpiredTime, passValidity);
            id = new ID(true, passengerName, passengerSurname,
                passengerAge, iexpiredTime, idValidity);
        }
    }
    private void setAutorisation()
    {
        DateTime autExpiredTime = GameData.GameTime;
        autExpiredTime.AddHours(UnityEngine.Random.Range(0, autValidity));

        autorisation = new Autorisation(true, passengerName,
            passengerSurname, autExpiredTime, autValidity);
    }

    private void makePermitFalse()
    {
        if (coinFlip() < 0.5)
        {
            if (ticket.present)
            {
                DateTime texpiredTime = GameData.GameTime;
                texpiredTime.AddHours(-1*UnityEngine.Random.Range(0, ticketValidity));

                ticket.expiredTime = texpiredTime;
            }
            else
            {
                DateTime sexpiredTime = GameData.GameTime;
                sexpiredTime.AddDays(-1 * UnityEngine.Random.Range(0, subValidity));

                subscription.expiredTime = sexpiredTime;
            }
        }
        else
        {
            subscription.present = false;
            ticket.present = false;
        }
    }
    private void makeIdFalse()
    {
        if (coinFlip() < 0.5)
        {
            if (passeport.present)
            {
                DateTime pexpiredTime = GameData.GameTime;
                pexpiredTime.AddYears(-1 * UnityEngine.Random.Range(0, passValidity));

                passeport.expiredTime = pexpiredTime;
            }
            else
            {
                DateTime iexpiredTime = GameData.GameTime;
                iexpiredTime.AddYears(-1 * UnityEngine.Random.Range(0, idValidity));

                id.expiredTime = iexpiredTime;
            }
        }
        else
        {

            passeport.present = false;
            id.present = false;
        }
    }
    private void makeAutorisationFalse()
    {
        if (coinFlip() < 0.5)
        {
            DateTime autExpiredTime = GameData.GameTime;
            autExpiredTime.AddHours(-1 * UnityEngine.Random.Range(0, autValidity));

            autorisation.expiredTime = autExpiredTime;
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

    private void setSprite()
    {
        //TODO Fill this section
    }

    public struct Ticket
    {
        public bool present;
        public DateTime expiredTime;
        //in hour
        public int validityPeriod;

        public string ToText()
        {
            return expiredTime.ToString() + " \n validity periode : " + validityPeriod;
        }
        public Ticket(bool present, DateTime expiredTime, int validityPeriod)
        {
            this.present = present;
            this.expiredTime = expiredTime;
            this.validityPeriod = validityPeriod;
        }
    };
    public struct Subscription
    {
        public bool present;
        public string name;
        public string surname;
        public DateTime expiredTime;
        //in day
        public int validityPeriod;

        public string ToText() 
        {
            return name + " " + surname + "\n" +
                expiredTime.ToString() + " \n validity periode : " + validityPeriod;
        }
        public Subscription(bool present, string name, string surname,
            DateTime expiredTime, int validityPeriod)
        {
            this.present = present;
            this.name = name;
            this.surname = surname;
            this.expiredTime = expiredTime;
            this.validityPeriod = validityPeriod;
        }
    };
    public struct Passeport
    {
        public bool present;
        public string name;
        public string surname;
        public int age;
        public DateTime expiredTime;
        //in year
        public int validityPeriod;
        public string ToText()
        {
            return name + " " + surname + " - " + age + "\n" +
                expiredTime.ToString() + " \n validity periode : " + validityPeriod;
        }
        public Passeport(bool present, string name, string surname, int age,
            DateTime expiredTime, int validityPeriod)
        {
            this.present = present;
            this.name = name;
            this.surname = surname;
            this.age = age;
            this.expiredTime = expiredTime;
            this.validityPeriod = validityPeriod;
        }
    };
    public struct ID
    {
        public bool present;
        public string name;
        public string surname;
        public int age;
        public DateTime expiredTime;
        //in year
        public int validityPeriod;
        public string ToText()
        {
            return name + " " + surname + " - " + age + "\n" +
                expiredTime.ToString() + " \n validity periode : " + validityPeriod;
        }
        public ID(bool present, string name, string surname, int age,
            DateTime expiredTime, int validityPeriod)
        {
            this.present = present;
            this.name = name;
            this.surname = surname;
            this.age = age;
            this.expiredTime = expiredTime;
            this.validityPeriod = validityPeriod;
        }
    };
    public struct Autorisation
    {
        public bool present;
        public string name;
        public string surname;
        public DateTime expiredTime;
        //in hour
        public int validityPeriod;


        public string ToText()
        {
            return name + " " + surname + "\n" +
                expiredTime.ToString() + " \n validity periode : " + validityPeriod;
        }
        public Autorisation(bool present, string name,
            string surname, DateTime expiredTime, int validityPeriod)
        {
            this.present = present;
            this.name = name;
            this.surname = surname;
            this.expiredTime = expiredTime;
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
            //Oula
            names = new string[18239+1];
            TextAsset text = Resources.Load<TextAsset>("names");
            string data = text.ToString();
            names = data.Split(char.Parse("\n"));
        }
        int i = UnityEngine.Random.Range(0, 18239);
        return names[i];
    }

}