using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;


public class MovingReels : MonoBehaviour
{
    [SerializeField] public RectTransform[] reels;
    [SerializeField] private MovingSymbols[] reelsSymbols; 
    [SerializeField] private float delayStep;
    [SerializeField] private Ease startEase;
    [SerializeField] private Ease stopEase;
    [SerializeField] private float boostDur, startDur, stopDur, boostDist, startDist, stopDist;
    [SerializeField] public Button playButton;
    [SerializeField] public RectTransform playButtonRT;
    [SerializeField] public Button stopButton;
    [SerializeField] public RectTransform stopButtonRT;
    [SerializeField] private float symbolHight;
    [SerializeField] private int visibleSymb;
    [SerializeField] private WinLinesChecker WLchecker; //second
    [SerializeField] private ReelAnalyzerAfterSpin RTanalyzer; //first

    

    public Dictionary<RectTransform, MovingSymbols> reelsDictionary;
    private float startPosY;
    private float posReel3;

    public bool FreeSpinsGo = false;
    public bool IsReelAnimated = false;
    public bool LastSpin = false;

    private void Start()
    {
        stopButton.interactable = false;
        stopButtonRT.localScale = Vector3.zero;
        reelsDictionary = new Dictionary<RectTransform, MovingSymbols>();
        for (int i =0; i < reels.Length; i++)
        {
            reelsDictionary.Add(reels[i], reelsSymbols[i]);         
        }
        posReel3 = reels[2].position.x;
        startPosY = reels[0].localPosition.y;
    }

    public void DoMove()
    {   if (FreeSpinsGo == true)
        {
            stopButton.interactable = false;
        }
        reelsDictionary[reels[2]].ReelState = ReelState.Reel;
        playButtonRT.localScale = Vector3.zero;
        playButton.interactable = false;
        stopButtonRT.localScale = Vector3.one;
        for (int i=0; i < reels.Length; i++)
        {
            var reelRT = reels[i];
            reelRT.DOAnchorPosY(boostDist, boostDur)
                .SetDelay(i * delayStep)
                .SetEase(startEase)
                .OnComplete(() => {
                    ScroleStart(reelRT);
                   if(reelsDictionary[reelRT].ReelID == reels.Length)
                    {
                        stopButton.interactable = true;
                    }
                    
                });
        }
    }
   
    private void ScroleStart(RectTransform reelRT)
    {
        reelsDictionary[reelRT].ReelState = ReelState.Spin;
        DOTween.Kill(reelRT);
        reelRT.DOAnchorPosY(startDist, startDur)
            .SetEase(Ease.Linear)
            .OnComplete(()=> CorrectReelPosition(reelRT));
    }

    private void ScroleStop(RectTransform reelRT)
    {
        reelsDictionary[reelRT].ReelState = ReelState.Stopping;
        DOTween.Kill(reelRT);

        var reelCurrentPosY = reelRT.localPosition.y;
        
        var slowDownDistance = reelCurrentPosY - symbolHight * visibleSymb;

        reelRT.DOAnchorPosY(slowDownDistance, stopDur)
            .SetEase(stopEase).OnComplete(() => {
                reelsDictionary[reelRT].ReelState = ReelState.Stop;
                ResetPosition(reelRT);
                if(reelRT.position.x == posReel3)
                {
                    if (FreeSpinsGo == true)
                    {
                        IsReelAnimated = true;
                    } 
                    else
                    {
                        stopButton.interactable = false;
                    }

                    RTanalyzer.StartAnalysisReel();
                    WLchecker.StartWinAnalys();

                    StartCoroutine(WaitForAnalyz(reelRT));

                }

            });
    }

    IEnumerator WaitForAnalyz(RectTransform ReelRT)
    {
        yield return new WaitUntil(() => WLchecker.ButtonActivate == true);
        if (reelsDictionary[ReelRT].ReelState == ReelState.Stop)
        {
            if (FreeSpinsGo == false) {
                stopButtonRT.localScale = Vector3.zero;

                playButtonRT.localScale = Vector3.one;
                playButton.interactable = true;
            }

            WLchecker.ButtonActivate = false;
            reelsDictionary[ReelRT].ReelState = ReelState.ReelReadyForSpin;
        }

    }
    private void CorrectReelPosition(RectTransform reel)
    {
        DOTween.Kill(reel);
        var currentReelPos = reel.localPosition.y;
        var extraDistance = StopHighSymbol(currentReelPos);
        var correctionDistance = currentReelPos - extraDistance;
        var correctionDuration = extraDistance / -(startDist / startDur);

        reel.DOAnchorPosY(correctionDistance, correctionDuration).OnComplete(() => ScroleStop(reel));


    }

    private float StopHighSymbol(float reelposy)
    {
        var travelDist = startPosY - reelposy;
        var SymbolScroled = travelDist / symbolHight;
        var integer = Mathf.Floor(SymbolScroled);
        var factionalPart = SymbolScroled - integer;

        var extraDistance = (1 - factionalPart) * symbolHight;
        return extraDistance;
    }

    public void StopClick()
    {
        if (FreeSpinsGo==false) {
            stopButton.interactable = false;
        }

        if (IsReelAnimated == true)
        {
            WLchecker.StopAnimated();
        }
        else
        {

            foreach (var reelRT in reels)
            {
                if (reelsDictionary[reelRT].ReelState == ReelState.Spin)
                {
                    CorrectReelPosition(reelRT);
                }
            }
        }
    }

    private void ResetPosition(RectTransform reelRT)
    {
        var currentPos = reelRT.localPosition.y;
        reelRT.localPosition = new Vector3(reelRT.localPosition.x, startPosY);
        reelsDictionary[reelRT].ResetSymbols(currentPos);
    }
}
