using UnityEngine;
using UnityEngine.InputSystem;

public class CamControl : MonoBehaviour
{
    public float sensitivity = 1.0f;
    public PlayerController player;

    [SerializeField] private InputAction lookX;
    [SerializeField] private InputAction lookY;
   
    private Vector3 offset;
    private float yaw = 0.0f, pitch = 0.0f;
    private float distance;
    private bool isDistanceSet = false;
    
    private void OnEnable()
    {
        lookX.Enable();
        lookY.Enable();
    }

    private void OnDisable()
    {
        lookX.Disable();
        lookY.Disable();
    }
    
    void Start()
    {
        offset = transform.position - player.transform.position;
    }
    
    void LateUpdate()
    {
        if (!player) return;
        
        transform.position = player.transform.position + offset;
        if (!isDistanceSet)
        {
            distance = offset.magnitude;
            isDistanceSet = true;
        }

        if (player.GetPaused()) return;
        yaw += lookX.ReadValue<float>() * sensitivity;
        pitch -= lookY.ReadValue<float>() * sensitivity;
        pitch = Mathf.Clamp(pitch, 15.0f, 90.0f);
        
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 negativeDistance = new Vector3(0.0f, 0.0f, -distance);
        Vector3 position = rotation * negativeDistance + player.transform.position;
        
        transform.position = position;
        transform.LookAt(player.transform);
    }
}
