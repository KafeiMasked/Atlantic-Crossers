using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Programmer: Noah Schwartz

public class Enemy_I : Enemy {

    private float health;
    private Vector2 position;
    private Color color;

    public Enemy_I()
    {
        health = 30;
        position = new Vector2(0, 0);
        color = new Color(255, 0, 0);
    }

    public Enemy_I(float health1, Vector2 position1, Color color)
    {
        health = health1;
        position = position1;
        this.color = color;
    }

    override public float getHealth()
    {
        return health;
    }

    override public void setHealth(float health)
    {
        this.health = health;
    }

    override public Vector2 getPosition()
    {
        return position;
    }

    override public void setPosition(Vector2 position)
    {
        this.position = position;
    }

    override public string toString()
    {
        return "Health: " + this.health + "\n" + 
               "Position: " + this.position + "\n";
    }

    // Use this for initialization
    void Start () {
        GetComponent<Renderer>().material.color = color;
    }
	
	// Update is called once per frame
	void Update () {
        
	}
}
