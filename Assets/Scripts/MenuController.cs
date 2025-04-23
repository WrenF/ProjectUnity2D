using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadCreditsScene()
    {
        SceneManager.LoadScene("Credits");
    }
    
    public void Quit()
    {
        Application.Quit();
    }   
}
