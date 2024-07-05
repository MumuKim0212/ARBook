using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingScene : MonoBehaviour
{
    [SerializeField] private UIBounceInEffect endingScene;

    void Start()
    {
        GameManager.Instance.isEnding = 1;
        StartCoroutine(Outro());
    }

    private IEnumerator Outro()
    {
        yield return new WaitForSeconds(2f);
        endingScene.gameObject.SetActive(true);
        endingScene.PlayBounceInAnimation();
    }
    public void GoGame()
    {
        SceneManager.LoadScene("Main");
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
