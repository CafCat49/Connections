using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public PlayerController player;
    public BridgeTrigger bridgePuzzle;

    public void RestartLevel()
    {
        if (player) player.Respawn();
        if (bridgePuzzle) bridgePuzzle.ResetBridge();
    }
}
