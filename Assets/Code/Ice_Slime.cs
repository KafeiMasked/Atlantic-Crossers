using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Programmer: Noah Schwartz

public class Ice_Slime : Slime_Basic
{

    float health = 1;
    float attack_power = 0.50f;
    float defence = 1;

    Rigidbody2D body;

    float anim_time_ratio = 32f;//53.8983f;

    GameObject player;
    Player player_script;

    Enemy_Parent parent_script;

    Animator anim;

    public GameObject collectables;


    public Ice_Slime() : base()
    {

    }

    public Ice_Slime(float health, float attack_power, float defence) : base(health, attack_power, defence)
    {

    }



    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        anim = GetComponent<Animator>();

        player_script = player.GetComponent<Player>();


        setHealth(health);
        setAttackPower(attack_power);
        setDefence(defence);




        body = GetComponent<Rigidbody2D>();
    }



    void Die()
    {
        DropItems(new Collectables[] { new Collectables("Baby_Ice_Slime", 1f) }, 2);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

        Damaging();

        Wander("Ice_Slime_Move", "Ice_Slime_Idle");

        Move();


        if (health <= 0)
        {
            Die();
        }



        health = GetHealth();
        attack_power = GetAttackPower();
        defence = GetDefence();

    }
}
