using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{

    public GameObject playerPrefab;
    public GameObject boxPrefab;

    //配列の宣言
    int[,] map;
    GameObject[,] field;

    //void PrintArray()
    //{
    //    string debugText = "";
    //    for (int i = 0; i < map.Length; i++)
    //    {
    //        debugText += map[i].ToString() + ".";

    //        //要素数を一つずつ出力
    //    }
    //    Debug.Log(debugText);
    //}

    Vector2Int GetPlayerIndex()
    {
        for (int y = 0; y < field.GetLength(0); y++)
        {
            for (int x = 0; x < field.GetLength(1); x++)
            {
               if (field[y, x] != null && field[y, x].tag == "Player")
               {

                   return new Vector2Int(x, y);
               }

            }
        }
        return new Vector2Int(-1, -1);
    }

    bool MoveObject(string tag, Vector2Int moveFrom, Vector2Int moveTo)
    {

        if (moveTo.y < 0 || moveTo.y >= field.GetLength(0))
        {
            return false;
        }

        if (moveTo.x < 0 || moveTo.x >= field.GetLength(1))
        {
            return false;
        }

        if (field[moveTo.y, moveTo.x] != null && field[moveTo.y, moveTo.x].tag == "Box")
        {
            Vector2Int velosity = moveTo - moveFrom;
            bool success = MoveObject(tag, moveTo, moveTo + velosity);
            if(!success) { return false; }
        }



        field[moveFrom.y, moveFrom.x].transform.position = new Vector3(moveTo.x, field.GetLength(0) - moveTo.y, 0);

        field[moveTo.y, moveTo.x] = field[moveFrom.y, moveFrom.x];

        field[moveFrom.y, moveFrom.x] = null;



        return true;
    }


    // Start is called before the first frame update
    void Start()
    {
        //配列の実績の作成と初期化
        map = new int[,] {
            { 0, 0, 0, 0, 0 },
            { 0, 2, 1, 2, 0 },
            { 0, 0, 0, 0, 0 },
        };
        field = new GameObject
        [
        map.GetLength(0),
        map.GetLength(1)
        ];

        string debugText = "";
        //
        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                debugText += map[y, x].ToString() + ". ";
                if (map[y, x] == 1)
                {
                    field[y, x] = Instantiate(
                        playerPrefab,
                        new Vector3(x, map.GetLength(0) - y, 0),
                        Quaternion.identity);
                }
                if (map[y, x] == 2)
                {
                    field[y, x] = Instantiate(
                        boxPrefab,
                        new Vector3(x, map.GetLength(0) - y, 0),
                        Quaternion.identity);
                }
            }
            debugText += "\n";
        }
        Debug.Log(debugText);
    }

    //  Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {

            Vector2Int index = GetPlayerIndex();
            MoveObject("Player", index, new Vector2Int(index.x - 1, index.y));

        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveObject("Player", GetPlayerIndex(), new Vector2Int(GetPlayerIndex().x + 1, GetPlayerIndex().y));

        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveObject("Player", GetPlayerIndex(), new Vector2Int(GetPlayerIndex().x, GetPlayerIndex().y - 1));

        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveObject("Player", GetPlayerIndex(), new Vector2Int(GetPlayerIndex().x, GetPlayerIndex().y + 1));

        }

    }
}
