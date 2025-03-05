using UnityEngine;

public class DiceResult : MonoBehaviour
{
    private Vector3 _diceVel;
    private int _diceNum;

    private void Update()
    {
        _diceVel = CubeRandomizer.diceVel;
    }
    
    void OnTriggerStay(Collider col)
    {
        if (_diceVel.x == 0f && _diceVel.y == 0f && _diceVel.z == 0f)
        {
            switch (col.gameObject.name)
            {
                case "1":
                    _diceNum = 6;
                    break;
                case "2":
                    _diceNum = 5;
                    break;
                case "3":
                    _diceNum = 4;
                    break;
                case "4":
                    _diceNum = 3;
                    break;
                case "5":
                    _diceNum = 2;
                    break;
                case "6":
                    _diceNum = 1;
                    break;
            }
            print(_diceNum);

        }
    }
}
