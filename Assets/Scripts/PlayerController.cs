using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public float shiftOffset = 1;
    public GameObject ShiftPoint;

    [SerializeField] private InputAction interactAction;
    private Rigidbody rb;
    private float moveX, moveY;
    private bool isShiftPlaced;
    
    private void OnEnable() => interactAction.Enable();
    private void OnDisable() => interactAction.Disable();
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isShiftPlaced = false;
    }

    void Update()
    {
        if (interactAction.triggered)
        {
            if (!ShiftPoint) return;
            if (!isShiftPlaced)
            {
                Vector3 shiftPos = new Vector3(
                    transform.position.x, 
                    transform.position.y + shiftOffset, 
                    transform.position.z
                );
                
                ShiftPoint.transform.position = shiftPos;
                isShiftPlaced = true;
            }
            else
            {
                transform.position = ShiftPoint.transform.position;
                isShiftPlaced = false;
            }
        }
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
