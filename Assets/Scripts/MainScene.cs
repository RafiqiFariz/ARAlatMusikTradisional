using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScene : MonoBehaviour
{
    public void BackToHome()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
