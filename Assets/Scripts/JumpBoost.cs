using UnityEngine;

public class JumpBoost : MonoBehaviour
{
    public float jumpForce = 10f;
    public PlayerController player;

    private AudioSource boingSound;

    private void Start()
    {
        boingSound = GetComponent<AudioSource>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.ToggleJump(true, jumpForce);
            boingSound.Play();
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
