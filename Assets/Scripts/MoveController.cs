
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveController : MonoBehaviour
{
    [SerializeField] private GameObject[] _listOfPos;
    private int _lastIndex=0;
    private int _diceNum;
    private CubeRandomizer _cr;
    private DiceResult _dr;
    public void Move()
    {
        _dr = GameObject.Find("Checker").GetComponent<DiceResult>();
        _cr = GameObject.Find("Dice").GetComponent<CubeRandomizer>();
        _diceNum = _dr._diceNum;
        if (!_cr.canBeRolled) return;
        if (_lastIndex == 0)
        {
            DefaultMove();
            print($"S Last Index:{_lastIndex} Dice Number: {_diceNum}");
        }
        else if (_diceNum+_lastIndex > 6 & _lastIndex < 7 && _diceNum + _lastIndex != 7)
        {
            gameObject.transform.DOMove(
                new Vector3(_listOfPos[7].transform.position.x,
                            _listOfPos[7].transform.position.y, 
                            _listOfPos[7].transform.position.z),
                _diceNum).OnComplete(NotDefaultMove);
            print($"A Last Index:{_lastIndex} Dice Number: {_diceNum}");
        }
        else if (_diceNum + _lastIndex > 13 & _lastIndex < 14 && _diceNum + _lastIndex != 14)
        {
            gameObject.transform.DOMove(
                new Vector3(_listOfPos[14].transform.position.x,
                            _listOfPos[14].transform.position.y,
                            _listOfPos[14].transform.position.z),
                _diceNum).OnComplete(NotDefaultMove);
            print($"A Last Index:{_lastIndex} Dice Number: {_diceNum}");
        }
        else if (_diceNum + _lastIndex > 20 & _lastIndex < 21 && _diceNum + _lastIndex != 21)
        {
            gameObject.transform.DOMove(
                new Vector3(_listOfPos[21].transform.position.x,
                            _listOfPos[21].transform.position.y,
                            _listOfPos[21].transform.position.z),
                _diceNum).OnComplete(NotDefaultMove);
            print($"A Last Index:{_lastIndex} Dice Number: {_diceNum}");
        }
        else if (_diceNum + _lastIndex > 27 & _lastIndex < 28 && _diceNum + _lastIndex != 28)
        {
            gameObject.transform.DOMove(
                new Vector3(_listOfPos[0].transform.position.x,
                            _listOfPos[0].transform.position.y,
                            _listOfPos[0].transform.position.z),
                _diceNum).OnComplete(NotDefaultMove);
            print($"A Last Index:{_lastIndex} Dice Number: {_diceNum}");
        }
        else
        {
            print($"E Last Index:{_lastIndex} Dice Number: {_diceNum}");
            DefaultMove();
        }
        _lastIndex = _diceNum + _lastIndex;
    }
    private void DefaultMove()
    {
        gameObject.transform.DOMove(
            new Vector3(_listOfPos[_diceNum + _lastIndex].transform.position.x,
                        _listOfPos[_diceNum + _lastIndex].transform.position.y,
                        _listOfPos[_diceNum + _lastIndex].transform.position.z),
             _diceNum * 1f);
    }
    private void NotDefaultMove()
    {
        if(_diceNum + _lastIndex -_diceNum > 28)
        {
            gameObject.transform.DOMove(
            new Vector3(_listOfPos[_diceNum + _lastIndex - _diceNum - 28].transform.position.x,
                        _listOfPos[_diceNum + _lastIndex - _diceNum - 28].transform.position.y,
                        _listOfPos[_diceNum + _lastIndex - _diceNum - 28].transform.position.z),
             (_diceNum + _lastIndex) * 1f);
            _lastIndex -= 28;
        }
        else
        {
            gameObject.transform.DOMove(
            new Vector3(_listOfPos[_diceNum + _lastIndex - _diceNum].transform.position.x,
                        _listOfPos[_diceNum + _lastIndex - _diceNum].transform.position.y,
                        _listOfPos[_diceNum + _lastIndex - _diceNum].transform.position.z),
             (_diceNum + _lastIndex) * 1f);
        }
    }
}
