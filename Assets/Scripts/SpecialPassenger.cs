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

        dialogue.onDialogueEnd.AddListener((status) => onDialogueEnd(type, status));
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

    private void onDialogueEnd(SpecialPassengerType type, int val)
    {
        //Debug.Log("Dialogue ended : " + type + " " + val);
        switch (type)
        {
            case SpecialPassengerType.MIGRANT:
                if (val == -1)
                    GameManager.migrantAlignment = 1;
                else if (val == -2)
                {
                    GameManager.migrantAlignment = -1;
                    GameManager.score += 100;
                }
                break;
            case SpecialPassengerType.RESISTANTE:
                if (val == -1)
                    GameManager.resistanteAlignment = 1;
                else if (val == -2)
                {
                    GameManager.resistanteAlignment = -1;
                    GameManager.score += 100;
                }
                else if (val == -3)
                {
                    GameManager.resistanteAlignment = -2;
                }
                break;
            case SpecialPassengerType.PROPARTI:
                if (val == -1)
                    GameManager.propartiAlignment = 1;
                else if (val == -2)
                {
                    GameManager.propartiAlignment = -1;
                    GameManager.score += 100;
                }
                break;
        }
    }
}
