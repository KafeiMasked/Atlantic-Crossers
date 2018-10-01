using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Programmer: Noah Schwartz

public abstract class Enemy : MonoBehaviour {


    public abstract float getHealth();
    public abstract void setHealth(float health);
    public abstract Vector2 getPosition();
    public abstract void setPosition(Vector2 position);
    public abstract string toString();

    /*float getHealth()
    {
        return health;
    }*/

    /*void setHealth(float health)
    {
        this.health = health;
    }

    Vector2 getPosition()
    {
        return position;
    }

    void setPosition(Vector2 position)
    {
        this.position = position;
    }

    string toString()
    {
        return "Health: " + this.health + "\n" + 
               "Position: " + this.position + "\n";
    }*/
}
