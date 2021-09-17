using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour
{
    // Light green Square
    public GameObject Square_Light;

    // Dark green Square
    public GameObject Square_Dark;

    // Upper left corner
    public Vector2 Start_pos;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 currentPos_Light = Start_pos;
        Vector2 currentPos_Dark = Start_pos;

        for (int j = 1; j < 13; j++)
        {
            currentPos_Light.x = Start_pos.x;
            currentPos_Dark.x = Start_pos.x;
            if (j%2==0)
                currentPos_Dark.x += 1;
            else
                currentPos_Light.x += 1;

            for (int i = 0; i < 6; i++)
            {
                GameObject obj1 = Instantiate(Square_Light, currentPos_Light, Quaternion.identity);

                GameObject obj2 = Instantiate(Square_Dark, currentPos_Dark, Quaternion.identity);

                obj1.transform.parent = gameObject.transform;
                obj2.transform.parent = gameObject.transform;

                currentPos_Light.x += 2;
                currentPos_Dark.x += 2;
            }
            currentPos_Light.y -= 1;
            currentPos_Dark.y -= 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
