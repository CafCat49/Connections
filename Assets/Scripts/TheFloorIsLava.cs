using UnityEngine;

public class TheFloorIsLava : MonoBehaviour
{
    public PlayerController player;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.Respawn();
        }
    }
}
