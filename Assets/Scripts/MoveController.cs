
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
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private GameObject _firstPos;
    [SerializeField] private GameObject _secondPos;
    [SerializeField] private GameObject _thirdPos;
    [SerializeField] private GameObject _fourthPos;
    public Vector3 lastCamPos = new Vector3(5.2f, 4.2f, 0f);
    public Vector3 lastCamRot = new Vector3(45, 270, 0f);
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
            lastCamPos = new Vector3(_secondPos.transform.position.x,
                                                     _secondPos.transform.position.y,
                                                     _secondPos.transform.position.z);
            lastCamRot= new Vector3(45f, 0f, 0f);
            _mainCamera.transform.DOMove(lastCamPos, 4f);
            _mainCamera.transform.DORotate(lastCamRot, 4f);
            gameObject.transform.DOMove(
                new Vector3(_listOfPos[7].transform.position.x,
                            _listOfPos[7].transform.position.y, 
                            _listOfPos[7].transform.position.z),
                _diceNum).OnComplete(NotDefaultMove);
            print($"A Last Index:{_lastIndex} Dice Number: {_diceNum}");
        }
        else if (_diceNum + _lastIndex > 13 & _lastIndex < 14 && _diceNum + _lastIndex != 14)
        {
            lastCamPos = new Vector3(_thirdPos.transform.position.x,
                                                     _thirdPos.transform.position.y,
                                                     _thirdPos.transform.position.z);
            lastCamRot = new Vector3(45f, 90f, 0f);
            _mainCamera.transform.DOMove(lastCamPos, 4f);
            _mainCamera.transform.DORotate(lastCamRot, 4f);
            gameObject.transform.DOMove(
                new Vector3(_listOfPos[14].transform.position.x,
                            _listOfPos[14].transform.position.y,
                            _listOfPos[14].transform.position.z),
                _diceNum).OnComplete(NotDefaultMove);
            print($"A Last Index:{_lastIndex} Dice Number: {_diceNum}");
        }
        else if (_diceNum + _lastIndex > 20 & _lastIndex < 21 && _diceNum + _lastIndex != 21)
        {
            lastCamPos = new Vector3(_fourthPos.transform.position.x,
                                                     _fourthPos.transform.position.y,
                                                     _fourthPos.transform.position.z);
            lastCamRot = new Vector3(45f, 180f, 0f);
            _mainCamera.transform.DOMove(lastCamPos, 4f);
            _mainCamera.transform.DORotate(lastCamRot, 4f);
            gameObject.transform.DOMove(
                new Vector3(_listOfPos[21].transform.position.x,
                            _listOfPos[21].transform.position.y,
                            _listOfPos[21].transform.position.z),
                _diceNum).OnComplete(NotDefaultMove);
            print($"A Last Index:{_lastIndex} Dice Number: {_diceNum}");
        }
        else if (_diceNum + _lastIndex > 27 & _lastIndex < 28 && _diceNum + _lastIndex != 28)
        {
            lastCamPos = new Vector3(_firstPos.transform.position.x,
                                                     _firstPos.transform.position.y,
                                                     _firstPos.transform.position.z);
            lastCamRot = new Vector3(45f, 270f, 0f);
            _mainCamera.transform.DOMove(lastCamPos, 4f);
            _mainCamera.transform.DORotate(lastCamRot, 4f);
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
        if (_diceNum + _lastIndex == 28)
        {
            gameObject.transform.DOMove(
                new Vector3(_listOfPos[0].transform.position.x,
                            _listOfPos[0].transform.position.y,
                            _listOfPos[0].transform.position.z),
                 _diceNum * 1f);
            _lastIndex = 0;
        }
        else
        {
            gameObject.transform.DOMove(
                new Vector3(_listOfPos[_diceNum + _lastIndex].transform.position.x,
                            _listOfPos[_diceNum + _lastIndex].transform.position.y,
                            _listOfPos[_diceNum + _lastIndex].transform.position.z),
                 _diceNum * 1f);
        }
    }
    private void NotDefaultMove()
    {
        if(_diceNum + _lastIndex -_diceNum > 28)
        {
            gameObject.transform.DOMove(
            new Vector3(_listOfPos[_diceNum + _lastIndex - _diceNum - 28].transform.position.x,
                        _listOfPos[_diceNum + _lastIndex - _diceNum - 28].transform.position.y,
                        _listOfPos[_diceNum + _lastIndex - _diceNum - 28].transform.position.z),
            _diceNum * 1f);
            _lastIndex -= 28;
        }
        else
        {
            gameObject.transform.DOMove(
            new Vector3(_listOfPos[_diceNum + _lastIndex - _diceNum].transform.position.x,
                        _listOfPos[_diceNum + _lastIndex - _diceNum].transform.position.y,
                        _listOfPos[_diceNum + _lastIndex - _diceNum].transform.position.z),
             _diceNum * 1f);
        }
    }
}
