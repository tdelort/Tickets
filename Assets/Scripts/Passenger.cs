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
    float ticketValidity = 1f;
    float autValidity = 5f;
    //day
    float subValidity = 365f;
    //year
    float passValidity = 5f;
    float idValidity = 5f;


    public string passengerName;
    public string passengerSurname;
    public int passengerAge;
    public bool isInOrder;
    public bool doIllegal;

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
    
    private Vector2 startPosition;
    private Vector2 endPosition;
    private bool movingIn = false;
    private bool movingOut = false;
    private float elapsedTime;
    private float percentageCompleted;
    private float desiredDuration = 3f;
    public void position( Vector2 start, Vector2 end)
    {
        elapsedTime = 0;
        startPosition = start;
        endPosition = end;
        movingIn = true;
    }
    public void leave()
    {
        elapsedTime = 0;
        movingOut = true;
    }
    private void Update() {
        if(movingIn == true)
        {
            elapsedTime += Time.deltaTime;
            percentageCompleted = elapsedTime / desiredDuration;
            this.gameObject.transform.position = Vector3.Lerp(startPosition, endPosition, percentageCompleted);
            if(percentageCompleted > 1)
            {
                movingIn = true;
            }
        }
        if(movingOut == true)
        {
            elapsedTime += Time.deltaTime;
            percentageCompleted = elapsedTime / desiredDuration;
            this.gameObject.transform.position = Vector3.Lerp(endPosition, startPosition, percentageCompleted);
            if(percentageCompleted > 1)
            {
                Debug.Log("try to destroy");
                Destroy(this.gameObject);
            }

        }
        

        
    }


    public void Init(bool inOrder, bool illegal)
    {
        passengerName = nameGenerator.getRandName();
        passengerSurname = nameGenerator.getRandName();
        passengerAge = UnityEngine.Random.Range(15, 85);
        dialogue.name = passengerName + " " + passengerSurname;
        isInOrder = inOrder;
        doIllegal = illegal;
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

        if (!isInOrder)
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

        if (doIllegal)
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
        debugInfo();
    }
    
    private void setPermit()
    {
        
        DateTime texpiredTime = GameData.GameTime;
        texpiredTime = texpiredTime.AddHours(UnityEngine.Random.Range(0f, ticketValidity));

        
        DateTime sexpiredTime = GameData.GameTime;
        sexpiredTime = sexpiredTime.AddDays(UnityEngine.Random.Range(0f, subValidity));

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
        pexpiredTime = pexpiredTime.AddYears(Mathf.RoundToInt(UnityEngine.Random.Range(0f, passValidity-1f)));
        pexpiredTime = pexpiredTime.AddDays(UnityEngine.Random.Range(0f, 365f));

        DateTime iexpiredTime = GameData.GameTime;
        iexpiredTime = iexpiredTime.AddYears(Mathf.RoundToInt(UnityEngine.Random.Range(0f, idValidity-1f)));
        iexpiredTime = iexpiredTime.AddDays(UnityEngine.Random.Range(0f, 365f));

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
        autExpiredTime = autExpiredTime.AddHours(UnityEngine.Random.Range(0f, autValidity));

        autorisation = new Autorisation(true, passengerName,
            passengerSurname, autExpiredTime, autValidity);
    }

    private void makePermitFalse()
    {
        //make it false or remove it
        if (coinFlip() < 0.5)
        {
            if (ticket.present)
            {
                DateTime texpiredTime = GameData.GameTime;
                texpiredTime = texpiredTime.AddHours(-1f*UnityEngine.Random.Range(0f, ticketValidity));

                ticket.expiredTime = texpiredTime;
            }
            else
            {
                //make time false or name
                if (coinFlip() < 0.5)
                {
                    DateTime sexpiredTime = GameData.GameTime;
                    sexpiredTime = sexpiredTime.AddDays(-1f * UnityEngine.Random.Range(0f, subValidity));

                    subscription.expiredTime = sexpiredTime;
                }
                else
                {
                    subscription.name = nameGenerator.getRandName();
                    if (coinFlip() < 0.5)
                    {
                       subscription.surname = nameGenerator.getRandName();
                    }
                }
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
        //make it false or remove it
        if (coinFlip() < 0.5)
        {
            if (passeport.present)
            {
                //make time false or name
                if (coinFlip() < 0.5)
                {
                    DateTime pexpiredTime = GameData.GameTime;
                    pexpiredTime = pexpiredTime.AddYears(Mathf.RoundToInt(-1f * 
                        UnityEngine.Random.Range(0f, passValidity-1f)));
                    pexpiredTime = pexpiredTime.AddDays(-1f *
                        UnityEngine.Random.Range(0f, 365f));

                    passeport.expiredTime = pexpiredTime;
                }
                else
                {
                    passeport.name = nameGenerator.getRandName();
                    if (coinFlip() < 0.5)
                    {
                        passeport.surname = nameGenerator.getRandName();
                    }
                }
            }
            else
            {
                //make time false or name
                if (coinFlip() < 0.5)
                {
                    DateTime iexpiredTime = GameData.GameTime;
                    iexpiredTime = iexpiredTime.AddYears(Mathf.RoundToInt(-1f *
                        UnityEngine.Random.Range(0f, idValidity-1f)));
                    iexpiredTime = iexpiredTime.AddDays(-1f *
                        UnityEngine.Random.Range(0f, 365f));

                    id.expiredTime = iexpiredTime;
                }
                else
                {
                    id.name = nameGenerator.getRandName();
                    if (coinFlip() < 0.5)
                    {
                        id.surname = nameGenerator.getRandName();
                    }
                }
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
        //make it false or remove it
        if (coinFlip() < 0.5)
        {
            
            if (coinFlip() < 0.5)
            {
                DateTime autExpiredTime = GameData.GameTime;
                autExpiredTime = autExpiredTime.AddHours(-1 * UnityEngine.Random.Range(0f, autValidity));

                autorisation.expiredTime = autExpiredTime;
            }
            else
            {
                autorisation.name = nameGenerator.getRandName();
                if (coinFlip() < 0.5)
                {
                    autorisation.surname = nameGenerator.getRandName();
                }
            }
        }
        else
        {
            autorisation.present = false;
        }
    }

    private float coinFlip()
    {
        return UnityEngine.Random.Range(0f, 1f);
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
        public float validityPeriod;

        public string ToText()
        {
            return expiredTime.ToString() + " \n validity periode : " + validityPeriod;
        }
        public Ticket(bool present, DateTime expiredTime, float validityPeriod)
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
        public float validityPeriod;

        public string ToText() 
        {
            return name + " " + surname + "\n" +
                expiredTime.ToString() + " \n validity periode : " + validityPeriod;
        }
        public Subscription(bool present, string name, string surname,
            DateTime expiredTime, float validityPeriod)
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
        public float validityPeriod;
        public string ToText()
        {
            return name + " " + surname + " - " + age + "\n" +
                expiredTime.ToString() + " \n validity periode : " + validityPeriod;
        }
        public Passeport(bool present, string name, string surname, int age,
            DateTime expiredTime, float validityPeriod)
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
        public float validityPeriod;
        public string ToText()
        {
            return name + " " + surname + " - " + age + "\n" +
                expiredTime.ToString() + " \n validity periode : " + validityPeriod;
        }
        public ID(bool present, string name, string surname, int age,
            DateTime expiredTime, float validityPeriod)
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
        public float validityPeriod;


        public string ToText()
        {
            return name + " " + surname + "\n" +
                expiredTime.ToString() + " \n validity periode : " + validityPeriod;
        }
        public Autorisation(bool present, string name,
            string surname, DateTime expiredTime, float validityPeriod)
        {
            this.present = present;
            this.name = name;
            this.surname = surname;
            this.expiredTime = expiredTime;
            this.validityPeriod = validityPeriod;
        }
    };

    public void debugInfo()
    {
        Debug.Log(dialogue.name + " ; " + isInOrder + " ; " +
            doIllegal + " ; ");
        Debug.Log("ticket : " + ticket.present + " ; " + ticket.ToText());
        Debug.Log("subscription : " + subscription.present + " ; " +
            subscription.name + " ; " + subscription.surname + " ; " + 
            subscription.ToText());
    }
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

