using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Programmer: Noah Schwartz

public class Slime_Basic : Enemy_Parent {

    private float health;
    private float attack_power;
    private float defence;

    private bool damaging = false;
    private SpriteRenderer sprites;
    private float recover_time = 0;
    private float flashing = 0;

    private Vector2 direction = new Vector2(0, 0);
    private float walking_speed = 0.7f;
    private int direction_timer = 0;
    private int direction_interval = 200;

    Rigidbody2D body;

    public Slime_Basic() : this(3, 1f, 1)
    {

    }

    public Slime_Basic(float health, float attack_power, float defence)
    {
        this.health = health;
        this.attack_power = attack_power;
        this.defence = defence;
        
    }

    public override float GetHealth()
    {
        return health;
    }

    public override void setHealth(float health)
    {
        this.health = health;
    }

    public override float GetAttackPower()
    {
        return attack_power;
    }

    public override void setAttackPower(float attack_power)
    {
        this.attack_power = attack_power;
    }

    public override float GetDefence()
    {
        return defence;
    }

    public override void setDefence(float defence)
    {
        this.defence = defence;
    }

    public void Damage(float a_p)
    {
            health -= a_p * (1 / defence);
            damaging = true;
        
    }

    public void Damaging()
    {
        if(damaging)
        {
            if (recover_time < 100)
            {
                if (flashing <= 100 / 5)
                {
                    sprites.material.color = new Color(1, 0, 0);
                }
                else if (flashing < (100 / 5) * 2)
                {
                    sprites.material.color = new Color(0, 0, 0, 0);
                }
                else if (flashing >= (100 / 5) * 2)
                {
                    flashing = 0;
                }
                flashing += 1;
            }
            else
            {
                sprites.material.color = new Color(1, 1, 1, 1);// 255, 255, 255);
            }
            if (recover_time >= 100)
            {
                recover_time = 0;
                damaging = false;
                flashing = 0;
                //sprites.color = new Color(255, 255, 255);
            }
            recover_time += 1;
        }
    }

    public void Wander(string wander_state, string stay_state)
    {
        if (direction_timer >= direction_interval)
        {
            direction = new Vector2(0, 0);
            GetComponent<Animator>().Play(stay_state);
        }
        else
        {
            GetComponent<Animator>().Play(wander_state);
        }
        if (direction_timer >= direction_interval + (direction_interval / 3))
        {
            direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            if (direction != new Vector2(0, 0))
            {
                direction.Normalize();
            }
            direction *= walking_speed;
            direction_timer = 0;
        }

        direction_timer += 1;
    }

    public void Move()
    {
        body.velocity = direction;
    }

    public void DropItems(Collectables[] collect_objects, int drop_number)
    {
        double sum = 0;
        for (int i = 0; i < collect_objects.Length; i += 1)
        {
            sum += collect_objects[i].GetProbability();
        }
        if (sum >= 0f && sum <= 1f)
        {
            for (int i = 0; i < drop_number; i += 1)
            {
                sum = 0;
                double random = Random.Range(0f, 1f);
                GameObject collectable = null;
                for (int j = 0; j < collect_objects.Length; j += 1)
                {
                    if (j == 0)
                    {
                        if (random <= collect_objects[0].GetProbability())
                        {
                            collectable = collect_objects[0].GetCollect();
                        }
                    }
                    else
                    {
                        if (random > sum && random <= sum + collect_objects[j].GetProbability())
                        {
                            collectable = collect_objects[j].GetCollect();
                        }
                    }
                    sum += collect_objects[j].GetProbability();
                }

                if (collectable != null)
                {

                    GameObject collect = Instantiate(collectable, transform.position, transform.rotation);
                    collect.transform.position = transform.position + transform.right * GetComponent<BoxCollider2D>().bounds.extents.x * Random.Range(-1f, 1f) +
                                                 transform.up * GetComponent<BoxCollider2D>().bounds.extents.y * Random.Range(-1f, 1f);
                }

            }
        }
        else
        {
            Debug.Log("PLEASE ENTER A PROBABILITY SUM LESS THAN 1");
        }
    }

    void Start () {
        
    }

    void Awake ()
    {
        sprites = GetComponent<SpriteRenderer>();

        direction_timer = direction_interval;

        body = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
