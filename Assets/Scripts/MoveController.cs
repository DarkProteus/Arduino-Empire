
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveController : MonoBehaviour
{
    [SerializeField] private GameObject[] _basicListOfPos = new GameObject[28];
    private Vector3[] _listOfPos;
    private Vector3[] _firstPlayerListOfPos = new Vector3[28];
    private Vector3[] _secondPlayerListOfPos = new Vector3[28];
    private int _lastIndex=0;
    private int _diceNum;
    private CubeRandomizer _cr;
    private DiceResult _dr;
    private UIController _uc;
    private Rigidbody _rb;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private GameObject _firstPos;
    [SerializeField] private GameObject _secondPos;
    [SerializeField] private GameObject _thirdPos;
    [SerializeField] private GameObject _fourthPos;
    public Vector3 lastCamPos = new Vector3(5.2f, 4.2f, 0f);
    public Vector3 lastCamRot = new Vector3(45, 270, 0f);
    private GameObject _player;
    private void Start()
    {
        for (int i = 0; i < _basicListOfPos.Length; i++)
        {
            if(i>=0 && i <= 7)
            {
                _firstPlayerListOfPos[i] = new Vector3(_basicListOfPos[i].transform.position.x, 
                                                       _basicListOfPos[i].transform.position.y,
                                                       _basicListOfPos[i].transform.position.z +0.2f);
                _secondPlayerListOfPos[i] = new Vector3(_basicListOfPos[i].transform.position.x,
                                                       _basicListOfPos[i].transform.position.y,
                                                       _basicListOfPos[i].transform.position.z - 0.2f);
            }else if (i >= 8 && i <= 14)
            {
                _firstPlayerListOfPos[i] = new Vector3(_basicListOfPos[i].transform.position.x + 0.2f,
                                                       _basicListOfPos[i].transform.position.y,
                                                       _basicListOfPos[i].transform.position.z);
                _secondPlayerListOfPos[i] = new Vector3(_basicListOfPos[i].transform.position.x - 0.2f,
                                                       _basicListOfPos[i].transform.position.y,
                                                       _basicListOfPos[i].transform.position.z);
            }
            else if (i >= 15 && i <= 21)
            {
                _firstPlayerListOfPos[i] = new Vector3(_basicListOfPos[i].transform.position.x,
                                                       _basicListOfPos[i].transform.position.y,
                                                       _basicListOfPos[i].transform.position.z - 0.2f);
                _secondPlayerListOfPos[i] = new Vector3(_basicListOfPos[i].transform.position.x,
                                                       _basicListOfPos[i].transform.position.y,
                                                       _basicListOfPos[i].transform.position.z - 0.2f);
            }
            else if (i >= 8 && i <= 27)
            {
                _firstPlayerListOfPos[i] = new Vector3(_basicListOfPos[i].transform.position.x,
                                                       _basicListOfPos[i].transform.position.y,
                                                       _basicListOfPos[i].transform.position.z-0.2f);
                _secondPlayerListOfPos[i] = new Vector3(_basicListOfPos[i].transform.position.x,
                                                       _basicListOfPos[i].transform.position.y,
                                                       _basicListOfPos[i].transform.position.z+0.2f);
            }
        }
    }
    public void Move(GameObject obj)
    {
        
        _player = obj;
        switch (_player.name)
        {
            case "PLAYER1":
                _listOfPos = _firstPlayerListOfPos;
                break;

            case "PLAYER2":
                _listOfPos = _secondPlayerListOfPos;
                break;
        }
        _rb = _player.GetComponent<Rigidbody>();
        _lastIndex = obj.GetComponent<PlayerController>().lastIndex;
        _dr = GameObject.Find("Checker").GetComponent<DiceResult>();
        _cr = GameObject.Find("Dice").GetComponent<CubeRandomizer>();
        _uc = GameObject.Find("GameController").GetComponent<UIController>();
        _diceNum = _dr._diceNum;
        if (!_cr.canBeRolled) return;
        if (_lastIndex == 0)
        {
            DefaultMove();
            print($"S Last Index:{_lastIndex} Dice Number: {_diceNum}, {_player.name}");
        }
        else if (_diceNum+_lastIndex > 6 & _lastIndex < 7)
        {
            lastCamPos = new Vector3(_secondPos.transform.position.x,
                                                     _secondPos.transform.position.y,
                                                     _secondPos.transform.position.z);
            lastCamRot= new Vector3(45f, 0f, 0f);
            _mainCamera.transform.DOMove(lastCamPos, 4f);
            _mainCamera.transform.DORotate(lastCamRot, 4f);
            if (_diceNum + _lastIndex == 7)
            {
                _player.transform.DOMove(
                                _listOfPos[7],
                                _diceNum)
                                .OnComplete(() => {
                                    GetTile();
                                });
            }
            else
            {
                _player.transform.DOMove(
                                _listOfPos[7],
                                _diceNum)
                                .OnComplete(NotDefaultMove);
            }
            print($"A Last Index:{_lastIndex} Dice Number: {_diceNum}");
        }
        else if (_diceNum + _lastIndex > 13 & _lastIndex < 14)
        {
            lastCamPos = new Vector3(_thirdPos.transform.position.x,
                                                     _thirdPos.transform.position.y,
                                                     _thirdPos.transform.position.z);
            lastCamRot = new Vector3(45f, 90f, 0f);
            _mainCamera.transform.DOMove(lastCamPos, 4f);
            _mainCamera.transform.DORotate(lastCamRot, 4f);
            if (_diceNum + _lastIndex == 14) {
                _player.transform.DOMove(_listOfPos[14],
                                         _diceNum)
                                         .OnComplete(() => {
                                            GetTile();
                                         });
            }
            else
            {
                _player.transform.DOMove(_listOfPos[14],
                                         _diceNum).OnComplete(NotDefaultMove);
            }
            print($"A Last Index:{_lastIndex} Dice Number: {_diceNum}");
        }
        else if (_diceNum + _lastIndex > 20 & _lastIndex < 21)
        {
            lastCamPos = new Vector3(_fourthPos.transform.position.x,
                                                     _fourthPos.transform.position.y,
                                                     _fourthPos.transform.position.z);
            lastCamRot = new Vector3(45f, 180f, 0f);
            _mainCamera.transform.DOMove(lastCamPos, 4f);
            _mainCamera.transform.DORotate(lastCamRot, 4f);
            if (_diceNum + _lastIndex == 21)
            {
                _player.transform.DOMove(_listOfPos[21],
                                         _diceNum)
                                         .OnComplete(() => {
                                            GetTile();
                                         });
            }
            else
            {
                _player.transform.DOMove(_listOfPos[21],
                                         _diceNum).OnComplete(NotDefaultMove);
            }
            print($"A Last Index:{_lastIndex} Dice Number: {_diceNum}");
        }
        else if (_diceNum + _lastIndex > 27 & _lastIndex < 28)
        {
            lastCamPos = new Vector3(_firstPos.transform.position.x,
                                                     _firstPos.transform.position.y,
                                                     _firstPos.transform.position.z);
            lastCamRot = new Vector3(45f, 270f, 0f);
            _mainCamera.transform.DOMove(lastCamPos, 4f);
            _mainCamera.transform.DORotate(lastCamRot, 4f);
            if (_diceNum + _lastIndex == 28)
            {
                _player.transform.DOMove(_listOfPos[0],
                                         _diceNum)
                                         .OnComplete(() => {
                                            GetTile();
                                         });
            }
            else
            {
                _player.transform.DOMove(_listOfPos[0],
                                         _diceNum).OnComplete(NotDefaultMove);
            }
            print($"A Last Index:{_lastIndex} Dice Number: {_diceNum}");
        }
        else
        {
            print($"E Last Index:{_lastIndex} Dice Number: {_diceNum}");
            DefaultMove();
        }
        _lastIndex = _diceNum + _lastIndex;
        obj.GetComponent<PlayerController>().lastIndex = _lastIndex;
    }
    private void DefaultMove()
    {
        if (_diceNum + _lastIndex == 28)
        {
            _player.transform.DOMove(_listOfPos[0],
                                     _diceNum * 1f)
                                     .OnComplete(() => {
                                         GetTile();
                                     });
            _lastIndex = 0;
        }else if (_diceNum+_lastIndex>28)
        {

            _player.transform.DOMove(_listOfPos[_diceNum + _lastIndex - 28],
                                     _diceNum * 1f)
                                     .OnComplete(() => {
                                         GetTile();
                                     });
        }
        else
        {
            _player.transform.DOMove(_listOfPos[_diceNum + _lastIndex],
                                     _diceNum * 1f)
                                     .OnComplete(() => {
                                         GetTile();
                                     });
        }
    }
    private void NotDefaultMove()
    {
        if(_diceNum + _lastIndex -_diceNum > 28)
        {
            _player.transform.DOMove(_listOfPos[_diceNum + _lastIndex - _diceNum - 28],
                                     _diceNum * 1f)
                                     .OnComplete(() => {
                                         GetTile(); 
                                     });
            _lastIndex -= 28;
        }
        else
        {
            _player.transform.DOMove(_listOfPos[_diceNum + _lastIndex - _diceNum],
                                     _diceNum * 1f)
                                     .OnComplete(() => {
                                         GetTile();
                                     });
        }
    }
    private void GetTile()
    {
        RaycastHit hit;
        Vector3 downDirection = Vector3.down;
        Physics.Raycast(_player.transform.position, downDirection, out hit, Mathf.Infinity);
        _uc.CallPanel(hit.collider.gameObject);
    }
}
