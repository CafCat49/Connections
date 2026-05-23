using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public float shiftOffset = 1;
    public GameObject shiftPoint;
    public GameObject pauseWarningMsg;
    public AvatarManager avatarWindow;

    [SerializeField] private InputAction planarShiftAction;
    [SerializeField] private InputAction respawnAction;
    [SerializeField] private InputAction pauseAction;
    
    private Rigidbody rb;
    private float moveX, moveY;
    private bool isShiftPlaced;
    private Vector3 checkpoint, spawnpoint;
    private bool disableManualPause = false;
    private bool isPaused;

    private void OnEnable()
    {
        planarShiftAction.Enable();
        respawnAction.Enable();
        pauseAction.Enable();
    }

    private void OnDisable()
    {
        planarShiftAction.Disable();
        respawnAction.Disable();
        pauseAction.Disable();
    }
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isShiftPlaced = false;
        shiftPoint.SetActive(false);
        pauseWarningMsg.SetActive(false);
        checkpoint = Vector3.zero;
        spawnpoint = transform.position; //TODO: update when starting a new level
    }

    void Update()
    {
        if (pauseAction.triggered && !disableManualPause) Pause();
        
        if (isPaused) return; //prevents usage of abilities if game is paused
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
            shiftPoint.SetActive(true);
            shiftPoint.transform.position = shiftPos;
            isShiftPlaced = true;
        }
        else
        {
            shiftPoint.SetActive(false);
            transform.position = checkpoint;
            isShiftPlaced = false;
        }
    }

    public void Respawn()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = spawnpoint;
        isShiftPlaced = false;
        shiftPoint.SetActive(false);
    }

    public void Pause(bool forcePause = false, bool forceStart = false)
    {
        if (forcePause)
        {
            Time.timeScale = 0;
            disableManualPause = true;
            isPaused = true;
            return;
        }
        
        if (forceStart)
        {
            Time.timeScale = 1;
            disableManualPause = false;
            isPaused = false;
            return;
        }
        
        if (!isPaused)
        {
            pauseWarningMsg.SetActive(true);
            Time.timeScale = 0;
            isPaused = true;
        }
        else
        {
            pauseWarningMsg.SetActive(false);
            Time.timeScale = 1;
            isPaused = false;
        }
    }

    void FixedUpdate()
    {
        if (isPaused) return;
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
