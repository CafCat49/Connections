using UnityEngine;
using TMPro;

public class AvatarManager : MonoBehaviour
{
    public float animationSpeed = 1.0f;
    public GameObject[] avatars;
    public GameObject captionPanel;
    public TextMeshProUGUI captionText;
    
    
    private float elapsedTime = 0.0f;
    private int currentAvatarIndex = 0;

    private void Start()
    {
        SwapAvatar();
        captionPanel.SetActive(false);
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

    public void ToggleCaptions(bool displayCC, string caption = "")
    {
        if (!captionPanel) return;
        if (displayCC)
        {
            captionPanel.SetActive(true);
            captionText.text = caption;
        }
        else
        {
            captionPanel.SetActive(false);
        }
    }
}
