using UnityEngine;
using UnityEngine.SceneManagement;

public class CutscenePlayer : MonoBehaviour
{
    public float animationDelay = 5.0f;
    public GameObject[] frames;
    public string mainlevelname = "PrototypeLevel";
    
    private float elapsedTime = 0.0f;
    private int currentFrame = 0;
    private int lastFrame = 1;


private void Start()
{
    if (frames.Length == 0) return;
    SwapFrame();
    lastFrame = frames.Length - 1;
}

private void Update()
{
    if (frames.Length == 0) return;
    if (currentFrame >= lastFrame) return;
    
    elapsedTime += Time.deltaTime;
    if (elapsedTime >= animationDelay)
    {
        currentFrame += 1;
        SwapFrame();
        elapsedTime = 0.0f;
    }
}

private void SwapFrame()
{
    foreach (GameObject frame in frames)
    {
        frame.SetActive(false);
    }
    frames[currentFrame].SetActive(true);
}

public void StartGame()
{
    SceneManager.LoadScene(mainlevelname);
}

}