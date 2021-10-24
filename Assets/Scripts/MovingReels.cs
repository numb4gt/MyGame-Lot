using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;


public class MovingReels : MonoBehaviour
{
    [SerializeField] private RectTransform[] reels;
    [SerializeField] private MovingSymbols[] reelsSymbols; 
    [SerializeField] private float delayStep;
    [SerializeField] private Ease startEase;
    [SerializeField] private Ease stopEase;
    [SerializeField] private float boostDur, startDur, stopDur, boostDist, startDist, stopDist;
    [SerializeField] private Button playButton;

    private Dictionary<RectTransform, MovingSymbols> reelsDictionary;
    private float startPosY;

    private void Start()
    {
        reelsDictionary = new Dictionary<RectTransform, MovingSymbols>();
        for (int i =0; i < reels.Length; i++)
        {
            reelsDictionary.Add(reels[i], reelsSymbols[i]);         
        }
        startPosY = reels[0].localPosition.y;
    }

    public void DoMove()
    {
        for(int i=0; i < reels.Length; i++)
        {
            var reelRT = reels[i];
            reelRT.DOAnchorPosY(boostDist, boostDur)
                .SetDelay(i * delayStep)
                .SetEase(startEase)
                .OnComplete(() => ScroleStart(reelRT));
        }
    }
   
    private void ScroleStart(RectTransform reelRT)
    {
        reelRT.DOAnchorPosY(startDist, startDur)
            .SetEase(Ease.Linear)
            .OnComplete(()=> ScroleStop(reelRT));
    }

    private void ScroleStop(RectTransform reelRT)
    {
        reelRT.DOAnchorPosY(stopDist, stopDur)
            .SetEase(stopEase).OnComplete(() => ResetPosition(reelRT));
    }

    private void ResetPosition(RectTransform reelRT)
    {
        var currentPos = reelRT.localPosition.y;
        reelRT.localPosition = new Vector3(reelRT.localPosition.x, startPosY);
        reelsDictionary[reelRT].ResetSymbols(currentPos);
    }
}
