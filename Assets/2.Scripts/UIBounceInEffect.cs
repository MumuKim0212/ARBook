using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Unity.VisualScripting;
using Sequence = DG.Tweening.Sequence;

public class UIBounceInEffect : MonoBehaviour
{
    public float duration = 0.5f;
    public float startScale = 0.3f;
    public float overshootScale = 1.2f;
    public Ease easeType = Ease.OutBack;

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void PlayBounceInAnimation()
    {
        // 초기 크기 설정
        rectTransform.localScale = Vector3.one * startScale;

        // 애니메이션 시퀀스 생성
        Sequence bounceSequence = DOTween.Sequence();

        // 크기 증가 애니메이션
        bounceSequence.Append(rectTransform.DOScale(Vector3.one * overshootScale, duration * 0.6f).SetEase(easeType));

        // 원래 크기로 돌아오는 애니메이션
        bounceSequence.Append(rectTransform.DOScale(Vector3.one, duration * 0.4f).SetEase(Ease.InOutSine));

        // 애니메이션 시작
        bounceSequence.Play();
    }

    public void PlayBounceOutAnimation(TweenCallback onComplete = null)
    {
        // 애니메이션 시퀀스 생성
        Sequence bounceOutSequence = DOTween.Sequence();

        // 약간 커지는 애니메이션
        bounceOutSequence.Append(rectTransform.DOScale(Vector3.one * overshootScale, duration * 0.3f).SetEase(Ease.OutSine));

        // 작아지면서 사라지는 애니메이션
        bounceOutSequence.Append(rectTransform.DOScale(Vector3.one * startScale, duration * 0.7f).SetEase(Ease.InBack));

        // 애니메이션 완료 후 콜백 실행
        if (onComplete != null)
        {
            bounceOutSequence.OnComplete(onComplete);
        }

        // 애니메이션 시작
        bounceOutSequence.Play();
    }
}