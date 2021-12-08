using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class StopPopUp : MonoBehaviour
{
    [SerializeField] private Image PopUpBG;
    [SerializeField] private RectTransform StopPURT;
    [SerializeField] private WinLinesChecker FreeTotalPrize;
    [SerializeField] private Text prizeForFSText;
    
    public void Show()
    {
        StopPURT.DOScale(1, 1);
        PopUpBG.DOFade(0.5f , 1);
        PopUpBG.raycastTarget = true;
        var prizeFS = FreeTotalPrize.FreeSpinTotal;
        prizeForFSText.DOCounter(0, prizeFS, 2f);
        StartCoroutine(Wait());
    }
    IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(3);
        Close();
    }
    public void Close()
    {
        StopPURT.DOScale(0, 1);
        PopUpBG.DOFade(0, 1);
        PopUpBG.raycastTarget = false;
        FreeTotalPrize.ResetFreeTotalPrize();
    }

}
