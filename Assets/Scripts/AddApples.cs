using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddApples : MonoBehaviour
{
    public StartGame game;
    public GameObject apple;
    // Start is called before the first frame update
    void Start()
    {
        System.Random rnd = new System.Random();
        for(int i = 0; i < game.Apple_count; i++)
        {
            int X = rnd.Next(0, 12);
            int Y = rnd.Next(0, 12);
            if (game.cell[X, Y] == -3)
            {
                GameObject apple_ = Instantiate(apple, new Vector3(-5.5f + X, 5.5f - Y, -1), Quaternion.identity);
                apple_.name = "Apple " + X.ToString() + ":" + Y.ToString();
                apple_.transform.parent = gameObject.transform;
                game.cell[X, Y] = -2;
            }
            else
                i--;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
