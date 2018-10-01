using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

//Programmer: Noah Schwartz

public class Player : MonoBehaviour
{
    float health = 3;
    float attack_power = 1;
    float defense = 1;

    int heart_max = 3;

    Rigidbody2D body;
    float running_speed = 2f;//4;
    float square_size = 2;
    float x = 0;
    float y = 0;
    int test = 1;
    bool sword_strike = false;
    public GameObject sword;
    float sword_timer = 0;
    bool is_sword_1 = true;
    Animator anim;
    Animation animations;
    BoxCollider2D box_collider;
    string weapon;
    Vector2 direction = new Vector2(0, 0);
    Vector2 pre_direction;
    int flip = 1;
    string walk_direction;
    bool weapon_strike = false;
    List<GameObject> strike_enemy = new List<GameObject>();

    SpriteRenderer sprite;
    float recover_time = 1000;
    float flashing = 0;
    float hearts_health;
    public GameObject hearts;

    int instant_time = 0;

    GameObject[] enemies;

    GameObject camera;

    double coins = 0;

    Vector3 scale;

    GameObject canvas;

    GameObject heart_icon;

    string play_anim;
    string pre_play_anim;
    float anim_time = 0;

    GameObject body_box;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        animations = GetComponent<Animation>();
        box_collider = GetComponent<BoxCollider2D>();

        canvas = GameObject.Find("Canvas");
        heart_icon = canvas.transform.Find("Heart_Parent").gameObject;

        scale = new Vector3(2, 2, 1);

        weapon = "Sword";

        sprite = GetComponent<SpriteRenderer>();

        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        camera = GameObject.Find("Main Camera");

        pre_direction = direction;
    }

    public float GetAttackPower()
    {
        return attack_power;
    }

    public string GetWeapon()
    {
        return weapon;
    }

    public bool GetWeaponStrike()
    {
        return weapon_strike;
    }

    public void SetWeaponStrike(bool weapon_strike)
    {
        this.weapon_strike = weapon_strike;
    }

    public void AddCoins(double coins)
    {
        this.coins += coins;
    }

    public double GetCoins()
    {
        return coins;
    }

    public void AddHealth(float hearts)
    {
        this.health += hearts;
    }

    int NextInt(float num, int sign)
    {
        return ((int)num) + 1 * sign;
    }

    public void AddStrikeEnemy(GameObject enemy)
    {
        strike_enemy.Add(enemy);
    }

    float Round(float num, float max)
    {
        int sign = 1;
        if (num != 0)
        {
            sign = (int)(Mathf.Abs(num) / num);
        }
        float max_standard_pos = num / max;
        float max_pos = NextInt(max_standard_pos, sign) * max;


        if (Mathf.Abs(num) >= Mathf.Abs(max_pos) - Mathf.Abs(max / 2))
        {
            num = max_pos;
        }
        else if (Mathf.Abs(num) < Mathf.Abs(max_pos) - Mathf.Abs((max / 2)))
        {
            num = max_pos - max * sign;
        }

        return num;
    }

    void Move()
    {
        if (test == 1)
        {
            x = 0;
            y = 0;
        }

        if (Input.GetKey("w"))
        {
            if (test == 0)
                y += running_speed;
            else
                y = running_speed;
        }
        if (Input.GetKey("a"))
        {
            if (test == 0)
                x -= running_speed;
            else
                x = -running_speed;
        }
        if (Input.GetKey("d"))
        {
            if (test == 0)
                x += running_speed;
            else
                x = running_speed;
        }
        if (Input.GetKey("s"))
        {
            if (test == 0)
                y -= running_speed;
            else
                y = -running_speed;
        }

        if (test == 1)
            body.velocity = new Vector3(x, y);
        else
            transform.position = new Vector2(Round(x, square_size), Round(y, square_size));
    }

    void ChangeHearts()
    {
        if(health > heart_max)
        {
            health = (float)heart_max;
        }


        GameObject[] all_hearts = GameObject.FindGameObjectsWithTag("Hearts");

        if (all_hearts.Length != 0)
        {
            for (int i = 0; i < all_hearts.Length; i += 1)
            {
                Destroy(all_hearts[i]);
            }
        }

        if (health > 0)
        {
            int number_full_hearts = (int)Mathf.Ceil(health);

            for (int i = 0; i < number_full_hearts; i += 1)
            {
                GameObject heart = Instantiate(hearts, new Vector2(0, 0), canvas.transform.rotation);
                heart.transform.SetParent(heart_icon.transform);
                heart.transform.localScale = new Vector3(1, 1, 1);
                heart.transform.position = heart_icon.transform.position +
                                            heart_icon.transform.forward/* * 0.5f*/ +
                                            heart_icon.transform.right * i * heart_icon.transform.localScale.x * 120 +
                                            heart_icon.transform.up;// * 3.5f;
                if (i == number_full_hearts - 1)
                {
                    float fraction = health - Mathf.Floor(health);
                    if (fraction == 0.25f)
                    {
                        heart.GetComponent<Animator>().Play("Hearts_anim");
                    }
                    if (fraction == 0.5f)
                    {
                        heart.GetComponent<Animator>().Play("Hearts_2_anim");
                    }
                    if (fraction == 0.75f)
                    {
                        heart.GetComponent<Animator>().Play("Hearts_3_anim");
                    }
                }
            }
            if(number_full_hearts < heart_max)
            {
                for(int i = number_full_hearts; i < heart_max; i += 1)
                {
                    GameObject heart = Instantiate(hearts, new Vector2(0, 0), canvas.transform.rotation);
                    heart.transform.SetParent(heart_icon.transform);
                    heart.transform.localScale = new Vector3(1, 1, 1);
                    heart.transform.position = heart_icon.transform.position +
                                                heart_icon.transform.forward/* * 0.5f*/ +
                                                heart_icon.transform.right * i * heart_icon.transform.localScale.x * 120 +
                                                heart_icon.transform.up;// * 3.5f;
                    heart.GetComponent<Animator>().Play("Hearts_0_Anim");
                }
            }
        }

        

        hearts_health = health;
    }

    void Damage(float attack_power)
    {
        health -= attack_power;
    }

    void Damaging()
    {
                if(body_box != null)
                {
                    Vector3 body_box_pos = new Vector3((body_box.transform.position.x + body_box.GetComponent<BoxCollider2D>().offset.x * square_size * flip),
                                   body_box.transform.position.y + body_box.GetComponent<BoxCollider2D>().offset.y * square_size,
                                   0);
                    Collider2D touch = Physics2D.OverlapBox(body_box_pos, body_box.GetComponent<BoxCollider2D>().size * square_size, 0, 1 << LayerMask.NameToLayer("Enemy"));

                    if (touch != null && recover_time >= 100)
                    {
                        Damage(touch.gameObject.GetComponent<Enemy_Parent>().GetAttackPower());
                        body.AddForce(body.velocity * -1 * 2);
                        recover_time = 0;
                    }
                }
                



        /////////// New Life System //////////////
        

        if (recover_time < 100)
        {
            if (flashing <= 100 / 5)
            {
                sprite.color = new Color(255, 0, 0);
            }
            else if (flashing <= (100 / 5) * 2)
            {
                sprite.color = new Color(0, 0, 0, 0);
            }
            else if (flashing > (100 / 5) * 2)
            {
                flashing = 0;
            }
            flashing += 1;

            recover_time += 1;
        }
        else
        {
            sprite.color = new Color(255, 255, 255);
        }
    }

    void Walk_Back()
    {
        scale = new Vector3(square_size, square_size, 1);
        play_anim = "Player Walking Forward Anim";
    }

    void Walk_Forward()
    {
        scale = new Vector3(square_size, square_size, 1);
        play_anim = "Player Walking Backward Anim";
    }

    void Walk_Sideways()
    {
        scale = new Vector3(direction.x * square_size, square_size, 1);

        play_anim = "Player Walking Sideways Anim";
    }

    void Walk(string walk_direction)
    {
        if(walk_direction == "back")
        {
            Walk_Back();
        }
        if(walk_direction == "forward")
        {
            Walk_Forward();
        }
        if(walk_direction == "sideways")
        {
            Walk_Sideways();
        }
    }

    void Move_Animation()
    {
        anim.speed = 1;
        if (Input.GetKey("s"))
        {
            walk_direction = "back";
            direction = new Vector2(0, -1);
        }
        else if (Input.GetKey("w"))
        {
            walk_direction = "forward";
            direction = new Vector2(0, 1);
        }
        else if(Input.GetKey("d"))
        {
            walk_direction = "sideways";
            direction = new Vector2(1, 0);
        }
        else if(Input.GetKey("a"))
        {
            walk_direction = "sideways";
            direction = new Vector2(-1, 0);
        }
        else
        {
            anim.speed = 0;
        }

        Walk(walk_direction);
    }




        void Update()
    {
        body_box = GameObject.FindGameObjectWithTag("Body_Box");


        Move();
        Move_Animation();

        if(direction.x == -1)
        {
            flip = -1;
        }
        else
        {
            flip = 1;
        }

        if(Input.GetMouseButtonDown(0) && weapon_strike == false)
        {
            weapon_strike = true;
            strike_enemy.Clear();
        }

        if (weapon_strike)
        {
            anim.speed = 1;
            if (weapon == "Sword")
            {
                if (direction == new Vector2(0, -1))
                {
                    if (pre_play_anim == "Player_Sword_Slash" && !anim.GetCurrentAnimatorStateInfo(0).IsName("Player_Sword_Slash"))
                    {
                        weapon_strike = false;
                    }
                    else
                    {
                        play_anim = "Player_Sword_Slash";


                        if (pre_direction != direction)
                        {
                            anim_time = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
                        }
                    }

                }
                if (direction == new Vector2(0, 1))
                {
                    if (pre_play_anim == "B_Player_Sword_Slash" && !anim.GetCurrentAnimatorStateInfo(0).IsName("B_Player_Sword_Slash"))
                    {
                        weapon_strike = false;
                    }
                    else
                    {
                        play_anim = "B_Player_Sword_Slash";
                    }
                    if (pre_direction != direction)
                    {
                        anim_time = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
                    }

                }

                if (direction == new Vector2(1, 0) || direction == new Vector2(-1, 0))
                {
                    if (pre_play_anim == "S_Player_Sword_Slash" && !anim.GetCurrentAnimatorStateInfo(0).IsName("S_Player_Sword_Slash"))
                    {
                        weapon_strike = false;
                    }
                    else
                    {
                        play_anim = "S_Player_Sword_Slash";
                    }
                    if (pre_direction != direction)
                    {
                        anim_time = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
                    }

                }
            }
        }


        
            GameObject sword_box = GameObject.FindGameObjectWithTag("Sword_Box");
            if (sword_box != null)
            {


                Collider2D touch = Physics2D.OverlapBox(new Vector3((sword_box.transform.position.x + sword_box.GetComponent<BoxCollider>().center.x * square_size * flip) ,
                                                           sword_box.transform.position.y + sword_box.GetComponent<BoxCollider>().center.y * square_size,
                                                           sword_box.transform.position.z),
                                                           sword_box.GetComponent<BoxCollider>().size * square_size, 0, 1 << LayerMask.NameToLayer("Enemy"));
                if (touch != null)
                {
                if (!strike_enemy.Contains(touch.gameObject))
                {
                    touch.gameObject.GetComponent<Slime_Basic>().Damage(1);
                   // touch.gameObject.SendMessageUpwards("Damage", 1);
                    strike_enemy.Add(touch.gameObject);
                }
            }
        }
        
    



        Damaging();

        if (hearts_health != health)
        {
            ChangeHearts();
        }

        transform.localScale = scale;

        if(play_anim != null)
        {
            if(pre_play_anim != play_anim)
            {
                anim.Play(play_anim, 0, anim_time);
                anim_time = 0;
                pre_play_anim = play_anim;
                
            }
            
        }
        if(pre_direction != direction)
        {
            pre_direction = direction;
        }


        if(health <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }

    }
    void LateUpdate()
    {
        
    }
}
