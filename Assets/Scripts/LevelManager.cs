using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public PlayerController player;
    public BridgeTrigger bridgePuzzle;

    private AudioSource levelStartSound;
    private bool isLevelStarted = false;

    void Start()
    {
        levelStartSound = GetComponent<AudioSource>();
    }
    
    public void RestartLevel()
    {
        if (player) player.Respawn();
        if (bridgePuzzle) bridgePuzzle.ResetBridge();
    }

    public void StartLevel()
    {
        if (isLevelStarted) return;
        if (levelStartSound && !levelStartSound.isPlaying) levelStartSound.Play();
        isLevelStarted = true;
    }
    
    public bool GetIsLevelStarted()
    {
        return isLevelStarted;
    }

    public void SetIsLevelStarted(bool value)
    {
        isLevelStarted = value;
    }
}
