using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animation))]
public class WinPanelHandAnimator : MonoBehaviour
{
    [SerializeField] private float _delay;

    private Animation _animation;
    private Coroutine _playedAnimationInJob;

    private void Start()
    {
        _animation = GetComponent<Animation>();
        _animation.Stop();
    }

    public void StartAnimation()
    {
        if (_playedAnimationInJob != null)
            StopPlayingAnimation();

        StartPlayingAnimation();
    }

    private IEnumerator PlayAnimation(Animation animation, float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        
        animation.Play();
    }

    private void StartPlayingAnimation()
    {
        _playedAnimationInJob = StartCoroutine(PlayAnimation(_animation, _delay));
    }

    private void StopPlayingAnimation()
    {
        StopCoroutine(_playedAnimationInJob);
    }
}
