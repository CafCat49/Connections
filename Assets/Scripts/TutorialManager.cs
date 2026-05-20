using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialWindow;
    public TextMeshProUGUI tutorialText;
    public PlayerController pc;
    
    void Start()
    {
        ShowTutorial("Controls:\n" +
                      "WASD to move\n" +
                      "F to use Planar Shift\n" +
                      "R to Respawn\n" +
                      "SPACE to Pause\n" +
                      "\n(Close this tutorial to begin)");
    }
    
    public void ShowTutorial(string message)
    {
        pc.Pause(true);
        tutorialText.text = message;
        tutorialWindow.SetActive(true);
    }
    
    public void HideTutorial()
    {
        pc.Pause(false, true);
        tutorialWindow.SetActive(false);
    }
}
