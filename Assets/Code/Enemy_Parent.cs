using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Programmer: Noah Schwartz

public abstract class Enemy_Parent : MonoBehaviour
{

    private float health = 30;
    private float attack_power = 0f;
    private float defence = 1;
    private GameObject enemy_hit;

    /*public Enemy_Parent()
    {
        health = 30;
        attack_power = 0f;
        defence = 1;
    }*/

    /*public Enemy_Parent(float health, float attack_power, float defence)
    {
        this.health = health;
        this.attack_power = attack_power;
        this.defence = defence;
    }*/

    public abstract float GetHealth();

    public abstract void setHealth(float health);

    public abstract float GetAttackPower();

    public abstract void setAttackPower(float attack_power);

    public abstract float GetDefence();

    public abstract void setDefence(float defence);




   /* public GameObject GetEnemyHit()
    {
        return enemy_hit;
    }

    public void SetEnemyHit(GameObject enemy_hit)
    {
        this.enemy_hit = enemy_hit;
    }

    public Enemy_Parent EnemyHit2(Enemy_Parent enemy_script)
    {
        return enemy_script;
    }*/



    // Use this for initialization
   /* void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }*/
}
