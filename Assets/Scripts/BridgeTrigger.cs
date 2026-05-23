using UnityEngine;

public class BridgeTrigger : MonoBehaviour
{
    public Vector3 targetEulerAngles;
    public float rotationDurationUp = 5.0f;
    public float rotationDurationDown = 15.0f;
    public float waitTime = 3.0f;
    public GameObject bridgePivot;

    private Vector3 baseEulerAngles;
    private Quaternion startRotation;
    private Quaternion targetRotation;
    private float elapsedRotationTime = 0f;
    private float elapsedWaitTime = 0f;
    private bool isRotating = false;
    private bool isLowering = false;
    private float rotationWaitTime = 0f;

    private void Start()
    {
        startRotation = bridgePivot.transform.rotation;
        baseEulerAngles = bridgePivot.transform.eulerAngles;
    }

    public void ResetBridge()
    {
        elapsedRotationTime = 0f;
        elapsedWaitTime = 0f;
        isRotating = false;
        isLowering = false;
        bridgePivot.transform.eulerAngles = baseEulerAngles;
    }

    private void Update()
    {
        if (!isRotating) return;

        if (isLowering) rotationWaitTime = 0;
        else rotationWaitTime = waitTime;
        
        if (rotationWaitTime > 0f) //if you are supposed to have a wait time, check how much time has passed
        {
            elapsedWaitTime += Time.deltaTime;
            if (elapsedWaitTime < rotationWaitTime) return;
        }
        elapsedRotationTime += Time.deltaTime;
        float percentage = !isLowering ? Mathf.Clamp01(elapsedRotationTime / rotationDurationUp) : Mathf.Clamp01(elapsedRotationTime / rotationDurationDown);
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
        elapsedRotationTime = 0f;
        elapsedWaitTime = 0f;
        isRotating = true;
        isLowering = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player") && !other.CompareTag("Weighted")) return;
        targetRotation = Quaternion.Euler(baseEulerAngles);
        startRotation = bridgePivot.transform.rotation;
        elapsedRotationTime = 0f;
        elapsedWaitTime = 0f;
        isRotating = true;
        isLowering = false;
    }
}
