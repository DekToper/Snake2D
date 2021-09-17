using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public int[,] cell = new int[12, 12];
    public GameObject Snake;
    public float Move_speed = 1;

    public int Move_side = 4;

    public int Apple_count = 7;

    public Text Txt;
    public Text Notification;
    public Button Restart_button;

    public GameObject Head;
    public GameObject Part;

    private int Score = 1;
    private GameObject head;

    public Vector2 Head_pos = new Vector2(0, 0);

        //    1
        //  4   2
        //    3

        // -3 - Empty cell
        // -2 - Apple
        // -1 - Head
        // 0 - Need to delete part
        // >0 - Part of Snake


    // Start is called before the first frame update
    void Start()
    {
        Restart_button.gameObject.SetActive(false);
        for(int i = 0; i <12;i++)
        {
            for(int j = 0; j < 12; j++)
            {
                cell[i, j] = -3;
            }
        }

        cell[System.Convert.ToInt32(Head_pos.x), System.Convert.ToInt32(Head_pos.y)] = -1; // head
        cell[System.Convert.ToInt32(Head_pos.x+1), System.Convert.ToInt32(Head_pos.y)] = 1; // first part

        InvokeRepeating("SnakeMove", 0, Move_speed);
    }

    void ChangeSnake()
    {
        for (int i = 0; i < 12; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                if(cell[i, j] > 0)
                {
                    cell[i, j]--;
                }
            }
        }
    }

    void SnakeMove()
    {
        ChangeSnake();
        cell[System.Convert.ToInt32(Head_pos.x), System.Convert.ToInt32(Head_pos.y)] = Score;
        try
        {
            if (Move_side == 1)
            {
                Head_pos.y -= 1;
                Debug.Log(Head_pos.y.ToString());
                cell[System.Convert.ToInt32(Head_pos.x), System.Convert.ToInt32(Head_pos.y)] = -1;
            }
            else if (Move_side == 4)
            {
                Head_pos.x -= 1;
                cell[System.Convert.ToInt32(Head_pos.x), System.Convert.ToInt32(Head_pos.y)] = -1;
            }
            else if (Move_side == 3)
            {
                Head_pos.y += 1;
                cell[System.Convert.ToInt32(Head_pos.x), System.Convert.ToInt32(Head_pos.y)] = -1;
            }
            else if (Move_side == 2)
            {
                Head_pos.x += 1;
                cell[System.Convert.ToInt32(Head_pos.x), System.Convert.ToInt32(Head_pos.y)] = -1;
            }
        }
        catch
        {
            EndGame("Game Over!");
        }

        CheckApple();
        CheckCell(System.Convert.ToInt32(Head_pos.x), System.Convert.ToInt32(Head_pos.y));
        PaintSnake();


        if(Score == Apple_count+1)
        {
            EndGame("You Win!");
        }
    }

    void CheckCell(int x, int y)
    {
        GameObject part = GameObject.Find("Part " + x.ToString() + ":" + y.ToString());
        if(part != null)
        {
            EndGame("Game Over!");
        }
    }

    void EndGame(string not)
    {
        Notification.text = not;
        CancelInvoke();
        Restart_button.gameObject.SetActive(true);
        Restart_button.onClick.AddListener(RestartGame);
    }


    void RestartGame()
    {
        Application.LoadLevel(Application.loadedLevel);
    }


    void CheckApple()
    {
        GameObject apple = GameObject.Find("Apple " + Head_pos.x.ToString() + ":" + Head_pos.y.ToString());
        if (apple != null)
        {
            Destroy(apple);
            Score++;
            Txt.text = "Score: " + (Score - 1).ToString();
            AddLenght();
        }
    }

    void AddLenght()
    {
        for (int i = 0; i < 12; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                if (cell[i, j] > 0 && cell[i,j] != Score)
                {
                    cell[i, j]++;
                }
            }
        }
    }

    void PaintSnake()
    {
        Destroy(GameObject.Find("Head"));
        head = Instantiate(Head, new Vector2(-6.5f + Head_pos.x + 1, 6.5f - Head_pos.y - 1), Quaternion.identity);
        head.name = "Head";
        if(Move_side == 2)
        {
            head.transform.Rotate(0, 0, -90);
        }
        else if(Move_side == 3)
        {
            head.transform.Rotate(0, 0, -180);
        }
        else if (Move_side == 4)
        {
            head.transform.Rotate(0, 0, 90);
        }

        for (int i = 0; i < 12; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                if (cell[i, j] > 0)
                {
                    if (GameObject.Find("Part " + i.ToString() + ":" + j.ToString()) == null)
                    {
                        GameObject prt = Instantiate(Part, new Vector2(-6.5f + i + 1, 6.5f - j - 1), Quaternion.identity);
                        prt.name = "Part " + i.ToString() + ":" + j.ToString();
                        prt.transform.parent = Snake.transform;
                    }
                }
                else if (cell[i, j] == 0)
                {
                    Destroy(GameObject.Find("Part " + i.ToString() + ":" + j.ToString()));
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move_side = 2;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Move_side = 3;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move_side = 4;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Move_side = 1;

        }
    }
}
