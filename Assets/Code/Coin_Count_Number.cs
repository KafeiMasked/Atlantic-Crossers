using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Programmer: Noah Schwartz

public class Coin_Count_Number : MonoBehaviour {

    Text text;
    GameObject player;
	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        else
        {
            text.text = "" + player.GetComponent<Player>().GetCoins();
        }
       
	}
}
