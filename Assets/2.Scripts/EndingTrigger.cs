using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingTrigger : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnDisabled()
    {
        SceneManager.LoadScene("Ending");

    }
}
