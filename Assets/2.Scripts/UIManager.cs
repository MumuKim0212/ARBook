using UnityEngine;
using DG.Tweening;
using System.Collections;

public class UIManager : MonoBehaviour
{
    [SerializeField] private UIBounceInEffect startScreenPanel;
    [SerializeField] private UIBounceInEffect gameTitleText;
    [SerializeField] private UIBounceInEffect gamestartText;
    [SerializeField] private GameObject introPanel;
    [SerializeField] private ChatTextController chatTextController;
    public AudioClip UIAudioClip;

    public void ShowStartScreen()
    {
        if(GameManager.Instance.isEnding == 0)
        {
        StartCoroutine(BounceAnimation(gameTitleText, 1f));
        StartCoroutine(BounceAnimation(gamestartText, 2f));
        }
        else
            startScreenPanel.gameObject.SetActive(false);
    }
    IEnumerator BounceAnimation(UIBounceInEffect bounceObject, float time)
    {
        yield return new WaitForSeconds(time);
        GameManager.Instance.audioSource.PlayOneShot(UIAudioClip);
        bounceObject.gameObject.SetActive(true);
        bounceObject.PlayBounceInAnimation();
    }

    public void ShowIntro()
    {
        startScreenPanel.PlayBounceOutAnimation(() =>
        {
            startScreenPanel.gameObject.SetActive(false);
            introPanel.gameObject.SetActive(true);
        });
    }

    public void CloseHint()
    {
        gameObject.SetActive(false);
    }

}