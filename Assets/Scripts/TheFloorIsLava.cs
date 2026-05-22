using UnityEngine;

public class TheFloorIsLava : MonoBehaviour
{
    public LevelManager level;
    public PlayerController player;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            level.RestartLevel();
        }
    }
}
