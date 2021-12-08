using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class StartPopUp : MonoBehaviour
{
    [SerializeField] private Image PopUpBG;
    [SerializeField] private RectTransform StartPURT;
    [SerializeField] private FreeSpins ToCallPU;

    public void Show()
    {
        StartPURT.DOScale(1, 1);
        PopUpBG.DOFade(0.5f, 1);
        PopUpBG.raycastTarget = true;
        StartCoroutine(Wait());
    }
    IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(2);
        Close();
        ToCallPU.ShowFreeSpinsCounter();
    }
    public void Close()
    {
        StartPURT.DOScale(0, 1);
        PopUpBG.DOFade(0, 1);
        PopUpBG.raycastTarget = false;
    }

}
