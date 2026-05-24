using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public PlayerController player;
    public BridgeTrigger bridgePuzzle;
    public AvatarManager avatarManager;
    public string levelStartSoundCaption = "";

    private AudioSource levelStartSound;
    private bool isLevelStarted = false;
    private float startSoundDuration;
    private float audioTimeElapsed = 0f;
    private bool isSoundPlayed = false;

    private void Start()
    {
        levelStartSound = GetComponent<AudioSource>();
        startSoundDuration =  levelStartSound.clip.length;
    }

    private void Update()
    {
        if (levelStartSound.isPlaying && isLevelStarted)
        {
            audioTimeElapsed += Time.deltaTime;
            if (!isSoundPlayed && audioTimeElapsed >= startSoundDuration)
            {
                avatarManager.ToggleCaptions(false);
            }
        }
    }

    public void RestartLevel()
    {
        if (player) player.Respawn();
        if (bridgePuzzle) bridgePuzzle.ResetBridge();
    }

    public void StartLevel()
    {
        if (isLevelStarted) return;
        if (levelStartSound && !levelStartSound.isPlaying)
        {
            levelStartSound.Play();
            avatarManager.ToggleCaptions(true, levelStartSoundCaption);
        }
        isLevelStarted = true;
    }
    
    public bool GetIsLevelStarted()
    {
        return isLevelStarted;
    }
}
