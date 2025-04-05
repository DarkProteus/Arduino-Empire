using UnityEngine;
public class CubeRandomizer : MonoBehaviour
{
    [SerializeField] private AudioClip whoosh;
    public AudioClip dice;
    public AudioSource audioSource;

    public Rigidbody rb;
    public static Vector3 diceVel;
    private Vector3 _startPos;
    public bool canBeRolled;
    private DiceResult _dr;
    public GameObject player;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        canBeRolled = true;
        _dr = GameObject.Find("Checker").GetComponent<DiceResult>();
        _startPos = transform.position;
    }
    private void Update()
    {
        diceVel = rb.velocity;
    }
    public void RollDice(GameObject obj)
    {
        if (!canBeRolled) return;
        player = obj;
        transform.position = _startPos;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.AddForce(Vector3.up * 5, ForceMode.Impulse);
        rb.AddTorque(Random.insideUnitSphere * 10, ForceMode.Impulse);
        _dr.readNum = 0;
        canBeRolled = false;
    }
}
