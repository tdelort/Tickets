using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passenger : MonoBehaviour
{
    public struct Ticket
    {
        public bool present;
        public string name;
        public int hour, day, month, year;

        public string ToText() { return name + '\n' + day + '-' + month + '-' + year + ':' + hour; }
        public Ticket(bool present, string name, int hour, int day, int month, int year)
        {
            this.name = name;
            this.present = present;
            this.hour = hour;
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
        public string ToText() { return name + ' ' + surname + '\n' + day + '-' + month + '-' + year; }
        public Passeport(bool present, string name, string surname, int age, int day, int month, int year)
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
    
    public Ticket ticket {get; set;}
    public Passeport passeport {get; set;}

    public Passenger(Ticket ticket, Passeport passeport)
    {
        this.ticket = ticket;
        this.passeport = passeport;
    }
}