using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private float _delay = 3f;

    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadGameOverScene()
    {
        StartCoroutine(LoadGameOver());
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    private IEnumerator LoadGameOver()
    {
        yield return new WaitForSeconds(_delay);
        SceneManager.LoadScene(2);
    }
}
