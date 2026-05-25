using UnityEngine;

public class Victory : MonoBehaviour
{
 
    public GameObject winScreenUI;

    private void Start()
    {
        if (winScreenUI != null) winScreenUI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish")) WinGame();
        
    }

    private void WinGame()
    {
        if (winScreenUI != null)  winScreenUI.SetActive(true);
        

    }
}