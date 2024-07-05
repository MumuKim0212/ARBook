using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private string[] correctAnswers;
    [SerializeField] private UIManager uiManager;
    public AudioSource audioSource;
    public int isEnding = 0;
    public int soundPlay = 0;
    private int currentStage = 0;

    public AudioClip clickAudio;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        uiManager.ShowStartScreen();
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (soundPlay == 1)
        {
            audioSource.Play();
            soundPlay = 2;
        }
    }

    public void CheckAnswer(string recognizedObject)
    {
        if (recognizedObject == correctAnswers[currentStage])
        {
            currentStage++;
        }
    }

    public void UIClick()
    {
        audioSource.PlayOneShot(clickAudio);
    }

}