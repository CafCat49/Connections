using System;
using UnityEngine;

public class AvatarManager : MonoBehaviour
{
    public float animationSpeed = 1.0f;
    public GameObject[] avatars;
    
    private float elapsedTime = 0.0f;
    private int currentAvatarIndex = 0;

    private void Start()
    {
        SwapAvatar();
        elapsedTime = 0.0f;
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= animationSpeed)
        {
            currentAvatarIndex += 1;
            if (currentAvatarIndex >= avatars.Length) currentAvatarIndex = 0;
            SwapAvatar();
            elapsedTime = 0.0f;
        }
    }

    private void SwapAvatar()
    {
        foreach (GameObject avatar in avatars)
        {
            avatar.SetActive(false);
        }
        avatars[currentAvatarIndex].SetActive(true);
    }
}
