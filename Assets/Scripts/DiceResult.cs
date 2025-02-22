using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceResult : MonoBehaviour
{
    private Vector3 diceVel;
    private int timesPrint;
    private int diceNum;

    private void Update()
    {
        diceVel = CubeRandomizer.diceVel;
    }
    void OnTriggerStay(Collider col)
    {
        if (diceVel.x == 0f && diceVel.y == 0f && diceVel.z == 0f && timesPrint==0)
        {
            switch (col.gameObject.name
            )
            {
                case "1":
                    diceNum = 6;
                    break;
                case "2":
                    diceNum = 4;
                    break;
                case "3":
                    diceNum = 5;
                    break;
                case "4":
                    diceNum = 2;
                    break;
                case "5":
                    diceNum = 3;
                    break;
                case "6":
                    diceNum = 1;
                    break;
            }
            print(diceNum);
            timesPrint = 1;
        }
    }
}
