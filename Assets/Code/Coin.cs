using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Programmer: Noah Schwartz

public class Coin : MonoBehaviour {

    GameObject player;

    Collider2D touch;

    double coin = 1;

    LayerMask player_layer;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        player_layer = 1 << LayerMask.NameToLayer("Player");
    }
	
	// Update is called once per frame
	void Update () {
        touch = Physics2D.OverlapBox(transform.position, GetComponent<BoxCollider2D>().size, 0, player_layer);
        if(touch != null)
        {
                player.GetComponent<Player>().AddCoins(coin);
                Destroy(gameObject);
        }
        
	}
}
