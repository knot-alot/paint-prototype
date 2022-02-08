using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TargetMovement : MonoBehaviour
{
    [SerializeField]
    Transform[] wayPoints;

    [SerializeField]
    float moveDuration = 5;

    [SerializeField]
    List<Ease> eases = new List<Ease>();

    Ease moveEase;
    Tween moveTween;
    int currentWayPoint, currentEase = 0;

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (moveTween.IsActive() || eases.Count < 1) return;

        currentEase = NextIterator(currentEase, eases.Count);
        moveEase = (Ease)currentEase;

        if (Vector3.Distance(transform.position, wayPoints[currentWayPoint].position) < 0.25f)
        {
            currentWayPoint = NextIterator(currentWayPoint, wayPoints.Length);
        }
        moveTween = transform.DOLocalMove(wayPoints[currentWayPoint].localPosition, moveDuration).SetEase(moveEase);
    }

    private int NextIterator(int currentIterator, int maxIterator)
    {
        currentIterator += 1;
        currentIterator = currentIterator % maxIterator;
        return currentIterator;
    }
}
