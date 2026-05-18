using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    
    private Rigidbody rb;
    private float moveX, moveY;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
