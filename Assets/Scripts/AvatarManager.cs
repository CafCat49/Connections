using UnityEngine;

public class AvatarManager : MonoBehaviour
{
    public GameObject[] avatars;

    public void SetActiveAvatar(int index)
    {
        if (index >= avatars.Length) return;
        for (int i = 0; i < avatars.Length; i++)
        {
            avatars[i].SetActive(i == index);
        }
    }
}
