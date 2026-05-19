using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public float shiftOffset = 1;
    public GameObject shiftPoint;

    [SerializeField] private InputAction planarShiftAction;
    [SerializeField] private InputAction respawnAction;
    
    private Rigidbody rb;
    private float moveX, moveY;
    private bool isShiftPlaced;
    private Vector3 checkpoint, spawnpoint;

    private void OnEnable()
    {
        planarShiftAction.Enable();
        respawnAction.Enable();
    }

    private void OnDisable()
    {
        planarShiftAction.Disable();
        respawnAction.Disable();
    }
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isShiftPlaced = false;
        checkpoint = Vector3.zero;
        spawnpoint = transform.position; //TODO: update when starting a new level
    }

    void Update()
    {
        if (Time.timeScale == 0) return; //prevents usage of abilities if game is paused
        if (planarShiftAction.triggered) PlanarShift();
        if (respawnAction.triggered) Respawn();
    }

    void PlanarShift()
    {
        if (!shiftPoint) return;
        /*
        Checks whether a planar shift point already was placed.
        If no: place one at player's location.
        Otherwise: teleport player to the planar shift point.
        */
        if (!isShiftPlaced)
        {
            checkpoint = transform.position;
            Vector3 shiftPos = new Vector3(checkpoint.x, checkpoint.y + shiftOffset, checkpoint.z);
            shiftPoint.transform.position = shiftPos;
            isShiftPlaced = true;
        }
        else
        {
            transform.position = checkpoint;
            isShiftPlaced = false;
        }
    }

    void Respawn()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = spawnpoint;
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(moveX, 0.0f, moveY);
        rb.AddForce(movement * speed);
    }

    void OnMove(InputValue moveValue)
    {
        Vector2 moveVec = moveValue.Get<Vector2>();
        moveX = moveVec.x;
        moveY = moveVec.y;
    }
    
    
}
