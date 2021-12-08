using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FreeSpins : MonoBehaviour
{
    [SerializeField] private MovingReels movingReels;
    [SerializeField] private StartPopUp startPopUp;
    [SerializeField] private StopPopUp stopPopUp;
    [SerializeField] private FreeSpinsCounter freeSpinsCounterSC;

    [SerializeField] private Text textOfCounter;
    public Text TextOfCounter { get => textOfCounter; set => textOfCounter = value; }

    private int HowFreeSpins = 10;
    private int freeSpinsCounter;

    public void ShowFreeSpinsCounter()
    {
        freeSpinsCounterSC.Show();
    }



    public void StartFreeSpins()
    {
        startPopUp.Show();
        movingReels.FreeSpinsGo = true;
        freeSpinsCounter = HowFreeSpins;
        TextOfCounter.text = freeSpinsCounter.ToString();
    }

    public void StartDoSpins()
    {
        StartCoroutine(WaitAndStartNextSpin());
    }

    IEnumerator WaitAndStartNextSpin()
    {
        while (freeSpinsCounter > 0)
        {
            yield return new WaitUntil(() => movingReels.reelsDictionary[movingReels.reels[2]].ReelState == ReelState.ReelReadyForSpin);
            movingReels.DoMove();
            freeSpinsCounter--;
            TextOfCounter.text = freeSpinsCounter.ToString();
            movingReels.IsReelAnimated = false;
            
        }

        if (freeSpinsCounter == 0)
        {
            movingReels.LastSpin = true;
            yield return new WaitUntil(() => movingReels.reelsDictionary[movingReels.reels[2]].ReelState == ReelState.ReelReadyForSpin);
            movingReels.FreeSpinsGo = false;
            EndFreeSpins();
            movingReels.IsReelAnimated = false;
        }
    }

    private void EndFreeSpins()
    {
        freeSpinsCounterSC.Close();
        stopPopUp.Show();
        freeSpinsCounter = HowFreeSpins;

        movingReels.stopButtonRT.localScale = Vector3.zero;
        movingReels.stopButton.interactable = false;

        movingReels.playButtonRT.localScale = Vector3.one;
        movingReels.playButton.interactable = true;
    }
}
