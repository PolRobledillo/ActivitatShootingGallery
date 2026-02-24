using DG.Tweening;
using UnityEngine;

public class TargetBehaviour : MonoBehaviour
{
    public Transform startPos;
    public Transform endPos;
    public bool moving = true;
    public float speed = 5f;
    private Sequence moveSequence;

    private void Start()
    {
        moveSequence = DOTween.Sequence()
            .Append(transform.DOMove(endPos.position, speed).SetEase(Ease.InOutSine))
            .Append(transform.DOMove(startPos.position, speed).SetEase(Ease.InOutSine))
            .SetLoops(-1);
        moveSequence.Play();
    }
    public void SwitchMovement(bool move)
    {
        if (move)
        {
            moveSequence.Play();
        }
        else
        {
            moveSequence.Pause();
        }
        moving = move;
    }
}
