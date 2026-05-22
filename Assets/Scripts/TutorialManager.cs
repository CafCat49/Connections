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
                      "SPACE to Pause\n" +
                      "\nTip: for more chaos, the game is compatible with PC Flight Stick controls");
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
