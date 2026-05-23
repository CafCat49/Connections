using UnityEngine;

public class JumpBoost : MonoBehaviour
{
    public float jumpForce = 10f;
    public PlayerController player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.ToggleJump(true, jumpForce);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.ToggleJump(false);
        }
    }
}
