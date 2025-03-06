using UnityEngine;
using DG.Tweening;

public class DiceResult : MonoBehaviour
{
    private Vector3 _diceVel;
    public int _diceNum;
    public int readNum;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private GameObject _idlePos;
    [SerializeField] private GameObject _dicePos;
    [SerializeField] private GameObject _zoomPos;
    private CubeRandomizer _cb;
    private MoveController _mc;
    private Vector3 _lastCamPos;
    private Vector3 _lastCamRot;
    private void Start()
    {
        _mc = GameObject.Find("Player").GetComponent<MoveController>();
        _cb = GameObject.Find("Dice").GetComponent<CubeRandomizer>();
    }
    private void Update()
    {
        _diceVel = CubeRandomizer.diceVel;
        _lastCamPos = _mc.lastCamPos;
        _lastCamRot = _mc.lastCamRot;
    }
    
    void OnTriggerStay(Collider col)
    {
        if (_diceVel.x == 0f && _diceVel.y == 0f && _diceVel.z == 0f && readNum==0)
        {
            switch (col.gameObject.name)
            {
                case "1":
                    _diceNum = 6;
                    break;
                case "2":
                    _diceNum = 4;
                    break;
                case "3":
                    _diceNum = 5;
                    break;
                case "4":
                    _diceNum = 2;
                    break;
                case "5":
                    _diceNum = 3;
                    break;
                case "6":
                    _diceNum = 1;
                    break;
            }
            readNum++;
            _cb.canBeRolled = true;
            Dice();
        }
    }
    private void Dice()
    {
        _mainCamera.transform.DOMove(new Vector3(_dicePos.transform.position.x,
                                                 _dicePos.transform.position.y,
                                                 _dicePos.transform.position.z), 4f);
        _mainCamera.transform.DORotate(new Vector3(80f, 0f, 0f), 4f).OnComplete(Zoom);
    }
    private void DiceNoZoom()
    {
        _mainCamera.transform.DOMove(new Vector3(_dicePos.transform.position.x,
                                                 _dicePos.transform.position.y,
                                                 _dicePos.transform.position.z), 4f);
        _mainCamera.transform.DORotate(new Vector3(80f, 0f, 0f), 4f).OnComplete(Idle);
    }
    private void Zoom()
    {
        _mainCamera.transform.DOMove(new Vector3(_zoomPos.transform.position.x,
                                                _zoomPos.transform.position.y,
                                                _zoomPos.transform.position.z), 3f).OnComplete(DiceNoZoom);
    }
    private void Idle()
    {
        _mainCamera.transform.DOMove(_lastCamPos, 3f);
        _mainCamera.transform.DORotate(_lastCamRot, 3f).OnComplete(_mc.Move);
    }
}
