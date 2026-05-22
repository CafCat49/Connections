using UnityEngine;

public class BridgeTrigger : MonoBehaviour
{
    public Vector3 targetEulerAngles;
    public float rotationDuration = 5.0f;
    public GameObject bridgePivot;

    private Vector3 baseEulerAngles;
    private Quaternion startRotation;
    private Quaternion targetRotation;
    private float elapsedTime = 0f;
    private bool isRotating = false;

    private void Start()
    {
        startRotation = bridgePivot.transform.rotation;
        baseEulerAngles = bridgePivot.transform.eulerAngles;
    }

    private void Update()
    {
        if (isRotating)
        {
            elapsedTime += Time.deltaTime;
            float percentage = Mathf.Clamp01(elapsedTime / rotationDuration);
            bridgePivot.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, percentage);
            if (percentage >= 1f) isRotating = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            targetRotation = Quaternion.Euler(targetEulerAngles);
            startRotation = bridgePivot.transform.rotation;
            elapsedTime = 0f;
            isRotating = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            targetRotation = Quaternion.Euler(baseEulerAngles);
            startRotation = bridgePivot.transform.rotation;
            elapsedTime = 0f;
            isRotating = true;
        }
    }
}
