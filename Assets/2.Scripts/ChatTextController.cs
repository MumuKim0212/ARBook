using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class ChatTextController : MonoBehaviour
{
    [SerializeField] private string[] introDialogues;
    public Text ChatText; // 실제 채팅이 나오는 텍스트
    public AudioClip textAudioClip;
    int timming = -1;
    string writerText = "";
    public GameObject quiz;
    bool isButtonClicked = false;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            timming++;
            StopAllCoroutines();
            if (timming < introDialogues.Length)
                StartCoroutine(NormalChat(introDialogues[timming]));
            else
            {
                GameManager.Instance.soundPlay = 1;
                quiz.SetActive(true);
                gameObject.SetActive(false);
            }
        }
    }
    IEnumerator NormalChat(string narration)
    {
        int a = 0;
        writerText = "";
        if (isButtonClicked)
        {
            ChatText.text = narration;
            a = narration.Length; // 버튼 눌리면 그냥 다 출력하게 함
            isButtonClicked = false;
        }
        //텍스트 타이핑 효과
        for (a = 0; a < narration.Length; a++)
        {
            GameManager.Instance.audioSource.PlayOneShot(textAudioClip);
            writerText += narration[a];
            ChatText.text = writerText;
            yield return new WaitForSeconds(0.08f);
        }
    }
}