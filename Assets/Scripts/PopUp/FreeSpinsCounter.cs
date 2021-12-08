using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class FreeSpinsCounter : MonoBehaviour
{
    [SerializeField] private RectTransform FreeSCRT;
    [SerializeField] private FreeSpins ForCallFSC;
    

    public void Show()
    {
        FreeSCRT.DOScale(1, 1).OnComplete(() =>{ ForCallFSC.StartDoSpins(); });
    }

    public void Close()
    {
        FreeSCRT.DOScale(0, 0);
    }
}