using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using System.Collections;
public class DiceResult : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 _diceVel;
    public int _diceNum;
    public int readNum;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private GameObject _idlePos;
    [SerializeField] private GameObject _dicePos;
    [SerializeField] private GameObject _zoomPos;
    private GameObject dice;
    private CubeRandomizer _cr;
    private MoveController _mc;
    private Vector3 _lastCamPos;
    private Vector3 _lastCamRot;
    private HashSet<Collider> _triggeredObjects = new HashSet<Collider>();
    private void Start()
    {
        dice = GameObject.Find("Dice");
        readNum = 1;
        _mc = FindObjectOfType<MoveController>();
        _cr = FindObjectOfType<CubeRandomizer>();
        _zoomPos = dice;
    }
    private void Update()
    {
        _diceVel = CubeRandomizer.diceVel;
        _lastCamPos = _mc.lastCamPos;
        _lastCamRot = _mc.lastCamRot;
    }
    
    private void OnTriggerStay(Collider col)
    {
        
        if (_diceVel.x == 0f && _diceVel.y == 0f && _diceVel.z == 0f && readNum==0)
        {
            if (!_triggeredObjects.Contains(col))
            {
                _triggeredObjects.Add(col);

                AudioSource audioSource = col.transform.parent.GetComponent<AudioSource>();
                if (audioSource != null)
                {
                    audioSource.Stop();
                    audioSource.clip = col.transform.parent.GetComponent<CubeRandomizer>()?.dice;
                    audioSource.Play();
                }
            }
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
            _cr.canBeRolled = true;
            Dice();
        }
    }
    private void OnTriggerExit(Collider col)
    {
        StartCoroutine(StopRollingMusic(col));
    }

    private IEnumerator StopRollingMusic(Collider col)
    {
        yield return new WaitForSeconds(2f);
        _triggeredObjects.Remove(col);
    }
    private void Dice()
    {
        _mainCamera.transform.DOMove(new Vector3(_dicePos.transform.position.x,
                                                 _dicePos.transform.position.y,
                                                 _dicePos.transform.position.z), 1f);
        _mainCamera.transform.DORotate(new Vector3(80f, 0f, 0f), 1f).OnComplete(Zoom);
    }
    private void DiceNoZoom()
    {
        _mainCamera.transform.DOMove(new Vector3(_dicePos.transform.position.x,
                                                 _dicePos.transform.position.y,
                                                 _dicePos.transform.position.z), 1f);
        _mainCamera.transform.DORotate(new Vector3(80f, 0f, 0f), 1f).OnComplete(Idle);
    }
    private void Zoom()
    {
        _mainCamera.transform.DOMove(new Vector3(_zoomPos.transform.position.x,
                                                _zoomPos.transform.position.y+2f,
                                                _zoomPos.transform.position.z-0.4f), 3f).OnComplete(DiceNoZoom);
    }
    private void Idle()
    {
        _mainCamera.transform.DOMove(_lastCamPos, 1f);
        _mainCamera.transform.DORotate(_lastCamRot, 1f).OnComplete(
            ()=>_mc.Move(_cr.player));
    }
}
