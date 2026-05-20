using UnityEngine;

public class BridgeTrigger : MonoBehaviour
{
    public GameObject bridgePivot;
    private bool isTriggered;

    private void Start()
    {
        isTriggered = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isTriggered)  return;
        if (other.CompareTag("Weighted"))
        {
            bridgePivot.transform.Rotate(Vector3.right, 90);
            isTriggered = true;
        }
    }
}
