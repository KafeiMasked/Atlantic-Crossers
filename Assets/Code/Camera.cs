using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Programmer: Noah Schwartz

public class Camera : MonoBehaviour
{

    GameObject player;
    Vector3 position;
    Grid grid_parent;
    Camera cam;
    float width = 10.3f * 2;
    float height = 3f * 2;

    void Start()
    {
        grid_parent = GetComponent<Grid>();
    }

    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        if (player != null)
        {
            position = new Vector3(player.transform.position.x + player.GetComponent<BoxCollider2D>().offset.x, player.transform.position.y + player.GetComponent<BoxCollider2D>().offset.y, -1);// 0.3f);
            if (position.x - (width / 1.55) > -grid_parent.GetSquareSize() &&
                position.x + (width / 1.55) < (grid_parent.GetWidth() * grid_parent.GetSquareSize()))
            {
                transform.position = new Vector3(position.x, transform.position.y, position.z);
            }
            if(position.y - (height / 1.5) > -(grid_parent.GetHeight() / 2) * grid_parent.GetSquareSize() &&
               position.y + (height / 1.5) < (grid_parent.GetHeight() / 2) * grid_parent.GetSquareSize())
            {
                transform.position = new Vector3(transform.position.x, position.y, position.z);
            }
                
        }
    }
}
