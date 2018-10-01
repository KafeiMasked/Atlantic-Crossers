using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Programmer: Noah Schwartz

public class Slime_Level2 : Slime_Basic {

    float health = 7;
    float attack_power = 0.75f;
    float defence = 1;

    Rigidbody2D body;

    float anim_time_ratio = 32f;//53.8983f;

    GameObject player;
    Player player_script;

    Enemy_Parent parent_script;

    Animator anim;

    Vector3 dist;
    float vanish_dist = 3f;
    float square_size;
    bool invisible = false;
    // Use this for initialization

    public Slime_Level2() : base()
    {

    }

    public Slime_Level2(float health, float attack_power, float defence) : base(health, attack_power, defence)
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

        square_size = GameObject.Find("Main Camera").GetComponent<Grid>().GetSquareSize();

        vanish_dist *= square_size;
    }






    void Die()
    {
        DropItems(new Collectables[] { new Collectables("Coin", 0.3f), new Collectables("Health_Potion", 0.7f) }, 10);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {


        Damaging();

        dist = player.transform.position - transform.position;

        if (invisible)
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Grey_Slime_Vanish"))
            {
                Wander("Grey_Slime_Gone", "Grey_Slime_Gone_Default");
            }
        }

        if (dist.magnitude <= vanish_dist)
        {
            if(!invisible)
            {
                anim.Play("Grey_Slime_Vanish");
            }
            invisible = true;
        }

        if (!invisible)
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Grey_Slime_Appear"))
            {
                Wander("Grey_Slime_Idle", "Grey_Slime_Default");
            }
        }

        if (dist.magnitude > vanish_dist)
        {
            if(invisible)
            {
                anim.Play("Grey_Slime_Appear");
            }
            invisible = false;
        }

        


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
