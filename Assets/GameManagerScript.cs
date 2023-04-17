using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    //配列の宣言
    int[] map;

    void PrintArray()
    {
        string debugText = "";
        for (int i = 0; i < map.Length; i++)
        {
            debugText += map[i].ToString() + ".";

            //要素数を一つずつ出力
        }
        Debug.Log(debugText);
    }

    int GetPlayerIndex()
    {
        for(int i = 0; i < map.Length; i++)
        {
            if (map[i] == 1)
            {
                return i;
            }
        }
        return -1;
    }

    bool MoveNumber(int number, int moveFrom, int moveTo)
    {
        if(moveTo < 0 ||  moveTo >= map.Length)
        {
            return false;
        }

        if (map[moveTo] == 2)
        {
            int velosity = moveTo - moveFrom;
            bool success = MoveNumber(2, moveTo, moveTo + velosity);

            if(!success) 
            {
                return false;
            }
        }

        map[moveTo] = number;
        map[moveFrom] = 0;
        return true;
    }


    // Start is called before the first frame update
    void Start()
    {
        //配列の実績の作成と初期化
        map = new int[] { 0, 0, 2, 1, 0, 2, 2, 0, 0 };
        //
        PrintArray();

    }

    // Update is called once per frame
    void Update()
    { 
        if (Input.GetKeyDown(KeyCode.A)) {
            int playerIndex = GetPlayerIndex();

            MoveNumber(1, playerIndex, playerIndex - 1);


            PrintArray();

        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            int playerIndex = GetPlayerIndex();
            MoveNumber(1, playerIndex, playerIndex + 1);

            PrintArray();

        }


    }
}
