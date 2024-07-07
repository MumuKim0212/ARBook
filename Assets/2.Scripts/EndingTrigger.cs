using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingTrigger : MonoBehaviour
{
    public void GoEnding()
    {
        SceneManager.LoadScene("Ending");
        gameObject.SetActive(false);
    }

    private void OnDisabled()
    {

    }
}
