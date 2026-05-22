using UnityEngine;

public class BridgeTrigger : MonoBehaviour
{
    public Vector3 targetEulerAngles;
    public float rotationDurationUp = 5.0f;
    public float rotationDurationDown = 15.0f;
    public float rotationWaitTime = 3.0f;
    public GameObject bridgePivot;

    private Vector3 baseEulerAngles;
    private Quaternion startRotation;
    private Quaternion targetRotation;
    private float elapsedTime = 0f;
    private float elapsedWaitTime = 0f;
    private bool isRotating = false;
    private bool isLowering = false;

    private void Start()
    {
        startRotation = bridgePivot.transform.rotation;
        baseEulerAngles = bridgePivot.transform.eulerAngles;
    }

    public void ResetBridge()
    {
        elapsedTime = 0f;
        elapsedWaitTime = 0f;
        isRotating = false;
        isLowering = false;
        bridgePivot.transform.eulerAngles = baseEulerAngles;
    }

    private void Update()
    {
        if (!isRotating) return;
        
        if (rotationWaitTime > 0f)
        {
            elapsedWaitTime += Time.deltaTime;
            if (elapsedWaitTime < rotationWaitTime) return;
        }
        elapsedTime += Time.deltaTime;
        float percentage = !isLowering ? Mathf.Clamp01(elapsedTime / rotationDurationUp) : Mathf.Clamp01(elapsedTime / rotationDurationDown);
        bridgePivot.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, percentage);
        if (percentage >= 1f)
        {
            isRotating = false; 
            isLowering = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") && !other.CompareTag("Weighted")) return;
        targetRotation = Quaternion.Euler(targetEulerAngles);
        startRotation = bridgePivot.transform.rotation;
        elapsedTime = 0f;
        elapsedWaitTime = 0f;
        isRotating = true;
        isLowering = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player") && !other.CompareTag("Weighted")) return;
        targetRotation = Quaternion.Euler(baseEulerAngles);
        startRotation = bridgePivot.transform.rotation;
        elapsedTime = 0f;
        elapsedWaitTime = 0f;
        isRotating = true;
    }
}
