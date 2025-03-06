using UnityEngine;

public class CubeRandomizer : MonoBehaviour
{
    public Rigidbody rb;
    public static Vector3 diceVel;
    private Vector3 _startPos;
    public bool canBeRolled;
    private DiceResult _dr;
    private void Start()
    {
        RollDice();
        _dr = GameObject.Find("Checker").GetComponent<DiceResult>();
        _startPos = transform.position;
    }
    private void Update()
    {
        diceVel = rb.velocity;
        if (Input.GetMouseButton(0)&& canBeRolled)
        {
            transform.position = _startPos;
            RollDice();
        }
    }
    public void RollDice()
    {
        _dr.readNum = 0;
        canBeRolled = false;
        rb.velocity = Vector3.zero; 
        rb.angularVelocity = Vector3.zero; 
    //    rb.AddForce(Vector3.up * 5, ForceMode.Impulse); 
        rb.AddTorque(Random.insideUnitSphere * 10, ForceMode.Impulse); 
    }
}
