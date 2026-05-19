using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialWindow;
    public TextMeshProUGUI tutorialText;

    void Start()
    {
        ShowTutorial("Controls:\n" +
                      "WASD to move\n" +
                      "F to use Planar Shift\n" +
                      "R to Respawn");
    }
    
    public void ShowTutorial(string message)
    {
        tutorialText.text = message;
        tutorialWindow.SetActive(true);
        Time.timeScale = 0;
    }
    
    public void HideTutorial()
    {
        tutorialWindow.SetActive(false);
        Time.timeScale = 1;
    }
}
