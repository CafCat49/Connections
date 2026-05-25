using UnityEngine;

public class BridgeTrigger : MonoBehaviour
{
    public float targetXAngle = 45.0f;
    public float rotationDurationUp = 5.0f;
    public float rotationDurationDown = 15.0f;
    public float waitTime = 3.0f;
    public GameObject bridgePivot;

    private Quaternion baseLocalRotation;
    private Quaternion startLocalRotation;
    private Quaternion targetLocalRotation;
    
    private float elapsedRotationTime = 0f;
    private float elapsedWaitTime = 0f;
    private bool isRotating = false;
    private bool isLowering = false;
    private bool isLowered = false;
    private float rotationWaitTime = 0f;
    private AudioSource triggerSound;

    private void Start()
    {
        baseLocalRotation = bridgePivot.transform.localRotation;
        triggerSound = GetComponent<AudioSource>();
    }

    public void ResetBridge()
    {
        elapsedRotationTime = 0f;
        elapsedWaitTime = 0f;
        isRotating = false;
        isLowering = false;
        bridgePivot.transform.localRotation = baseLocalRotation;
    }

    private void Update()
    {
        if (!isRotating) return;

        if (isLowering) rotationWaitTime = 0;
        else rotationWaitTime = waitTime;
        
        if (rotationWaitTime > 0f) //if you are supposed to have a wait time, check how much time has passed
        {
            elapsedWaitTime += Time.deltaTime;
            if (elapsedWaitTime < rotationWaitTime) return; //return if not enough time passed
        }
        elapsedRotationTime += Time.deltaTime;
        
        //set rotation speed depending on direction, and rotate
        float duration = isLowering ? rotationDurationDown : rotationDurationUp;
        float percentage = Mathf.Clamp01(elapsedRotationTime / duration);
        
        bridgePivot.transform.localRotation = Quaternion.Lerp(startLocalRotation, targetLocalRotation, percentage);
        
        
        if (triggerSound && !triggerSound.isPlaying)
        {
            triggerSound.Play();
        }
        if (percentage >= 1f)
        {
            isRotating = false; 
            //isLowering = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") && !other.CompareTag("Weighted")) return;
        targetLocalRotation = baseLocalRotation * Quaternion.AngleAxis(targetXAngle, Vector3.right);
        startLocalRotation = bridgePivot.transform.localRotation;
        elapsedRotationTime = 0f;
        elapsedWaitTime = 0f;
        isRotating = true;
        isLowering = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player") && !other.CompareTag("Weighted")) return;
        targetLocalRotation = baseLocalRotation;
        startLocalRotation = bridgePivot.transform.localRotation;
        elapsedRotationTime = 0f;
        elapsedWaitTime = 0f;
        isRotating = true;
        isLowering = false;
    }
}
