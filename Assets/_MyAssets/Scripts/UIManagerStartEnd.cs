using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManagerStartEnd : MonoBehaviour
{
    public void LoadNextScene()
    {
        int noScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(noScene+1);
    }

    public void Quitter()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
