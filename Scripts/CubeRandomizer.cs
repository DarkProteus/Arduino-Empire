using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRandomizer : MonoBehaviour
{
    public Rigidbody rb;
    public static Vector3 diceVel;
    private void Start()
    {
        RollDice();
    }
    private void Update()
    {
        diceVel = rb.velocity;
    }
    public void RollDice()
    {
        rb.velocity = Vector3.zero; 
        rb.angularVelocity = Vector3.zero; 
        rb.AddForce(Vector3.up * 5, ForceMode.Impulse); 
        rb.AddTorque(Random.insideUnitSphere * 10, ForceMode.Impulse); 
    }
}
