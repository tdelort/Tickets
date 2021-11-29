using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialPassenger : Passenger
{
    public bool canMove = true;
    public new SpecialDialogue dialogue = new SpecialDialogue();

    public void Init(SpecialPassengerType type)
    {
        string name;
        switch (type)
        {
            case SpecialPassengerType.MIGRANT: name = "Nom du Migrant"; break;
            case SpecialPassengerType.RESISTANTE: name = "Nom de la Resistante"; break;
            case SpecialPassengerType.PROPARTI: name = "Nom du Pro-parti"; break;
            default: name = "ERROR"; break;
        }

        dialogue.Init(name, type);
        base.setSprite();
    }

    float min = -6, max = 6;
    float speed = 3f;
    bool movingRight = true;

    private void Update()
    {
        Vector2 target = movingRight ? new Vector2(max, transform.position.y) : new Vector2(min, transform.position.y);
        if(canMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }

        if(transform.position.x == target.x)
            movingRight = !movingRight; 

        gameObject.GetComponent<Animator>().SetBool("isMoving", canMove);
        gameObject.GetComponent<SpriteRenderer>().flipX = !movingRight;
        
    }
    public override bool isSpecial()
    {
        return true;
    }
}
