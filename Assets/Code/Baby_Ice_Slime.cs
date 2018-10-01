using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Programmer: Noah Schwartz

public class Baby_Ice_Slime : Slime_Basic
{

    float health = 1;
    float attack_power = 0.25f;
    float defence = 1;

    Rigidbody2D body;

    float anim_time_ratio = 32f;//53.8983f;

    GameObject player;
    Player player_script;

    Enemy_Parent parent_script;

    Animator anim;

    public GameObject collectables;

    public Baby_Ice_Slime() : base()
    {

    }

    public Baby_Ice_Slime(float health, float attack_power, float defence) : base(health, attack_power, defence)
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

        player_script.AddStrikeEnemy(gameObject);


        body = GetComponent<Rigidbody2D>();
    }



    void Die()
    {
        DropItems(new Collectables[] { new Collectables("Coin", 0.75f) }, 1);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

        Damaging();

        Wander("Baby_Ice_Slime_Move", "Baby_Ice_Slime_Idle");

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
