using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Programmer: Noah Schwartz

public class Grid : MonoBehaviour {

    // Use this for initialization
    public GameObject player;
    public GameObject walls;
    public GameObject enemy_I;
    public GameObject slime_2;
    public GameObject ice_slime;
    public GameObject[] background_tile;
    int number_of_backgrounds;
    float square_size = 2f;
    string[,] grid_string;
    GameObject[,] grid;
    GameObject objects;
    void Start () {


        //ESI = Enemy Slime Ice
        //ES2 = Enemy Slime Level 2
        //E = Enemy
        //P = Player
        //W = Wall
        // = Enmpty Space

        grid_string = new string[,]{
                { "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W" },
                { "W", " ", " ", "W", "W", " ", " ", " ", " ", " ", " ", " ", " ", "W" },
                { "W", "W", "W", " ", "W", " ", " ", "ESI", " ", " ", " ", "ES2", " ", "W" },
                { "W", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", "W" },
                { "W", " ", " ", " ", " ", "E", " ", "P", " ", " ", " ", " ", " ", "W" },
                { "W", " ", "W", "W", "W", " ", " ", " ", " ", " ", " ", " ", " ", "W" },
                { "W", " ", "W", "W", "W", " ", "E", " ", "E", " ", " ", " ", " ", "W" },
                { "W", " ", "W", "W", "W", " ", " ", " ", " ", " ", " ", " ", " ", "W" },
                { "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W", "W" } };


        grid = new GameObject[grid_string.GetLength(0), grid_string.GetLength(1)];

        for(int i = 0; i < grid_string.GetLength(0); i += 1)
        {
            for(int j = 0; j < grid_string.GetLength(1); j += 1)
            {
                if (grid_string[i, j] == " ")
                {
                    grid[i, j] = null;
                }
                if (grid_string[i, j] == "P")
                {
                    grid[i, j] = player;
                }
                if (grid_string[i, j] == "W")
                {
                    grid[i, j] = walls;
                }
                if (grid_string[i, j] == "E")
                {
                    grid[i, j] = enemy_I;
                }
                if(grid_string[i, j] == "ES2")
                {
                    grid[i, j] = slime_2;
                }
                if(grid_string[i, j] == "ESI")
                {
                    grid[i, j] = ice_slime;
                }
            }
            
        }


        for (int i = 0; i < grid.GetLength(0); i += 1)
        {
            for (int j = 0; j < grid.GetLength(1); j += 1)
            {

                if (grid[i, j] != null)
                {
                     objects = Instantiate(grid[i, j], new Vector2(j * square_size, (4 - i) * square_size), Quaternion.identity);
                }
            }
        }


        number_of_backgrounds = 2;

        for (int i = 0; i < grid.GetLength(0); i += 1)
        {
            for (int j = 0; j < grid.GetLength(1); j += 1)
            {
                int random_background = (int)Mathf.Ceil(Random.Range(0f, number_of_backgrounds));
                for(int k = 0; k < number_of_backgrounds; k += 1)
                {
                    if (random_background == k + 1)
                    {
                        objects = Instantiate(background_tile[k], new Vector2(j * square_size, (4 - i) * square_size), Quaternion.identity);
                        objects.transform.localScale = new Vector3(square_size, square_size, 1);
                        objects.GetComponent<SpriteRenderer>().sortingOrder = -100;
                    }
                }
                
                    
                
            }
        }
    }

    public int GetWidth()
    {
        return grid.GetLength(1);
    }

    public int GetHeight()
    {
        return grid.GetLength(0);
    }

    public float GetSquareSize()
    {
        return square_size;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
