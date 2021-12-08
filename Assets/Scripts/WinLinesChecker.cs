using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WinLinesChecker : MonoBehaviour
{
    [SerializeField] private SymbolFotAnalyzer[] symbols;
    [SerializeField] private GameConfig gameConfig;
    [SerializeField] private Text Counter;
    [SerializeField] private MovingReels movingReels;
    public bool ButtonActivate = false;
    private int prizeWin;
    public int FreeSpinTotal;


    public void CountPrize(List<SymbolFotAnalyzer> winSymbols)
    {
        var startPrize = prizeWin;
        var prizeOfComb = winSymbols[0].SymbolWin;
        prizeWin += prizeOfComb;     
        Counter.DOCounter(startPrize, prizeWin, 1.5f); 
        if (movingReels.FreeSpinsGo == true)
        {
            FreeSpinTotal += prizeOfComb;
        }
    }
    public void ResetFreeTotalPrize()
    {
        FreeSpinTotal = 0;
    }

    private void WinLineCheck(WinLine winLine)
    {
        var List = AnalyzOfWinSymbols(winLine.WinLines);

        var winSymbol1 = List.WinList[0];
        var winSymbol2 = List.WinList[1];
        var winSymbol3 = List.WinList[2];

        if (winSymbol1.SymbolType != symbolType.scatter_clover)
        {
            if (winSymbol1.SymbolType == winSymbol2.SymbolType && winSymbol2.SymbolType == winSymbol3.SymbolType)
            {
                AnimateSymbol(List);
                CountPrize(List.WinList);
            }
        }
    }

    private ListsForAnalys AnalyzOfWinSymbols(int[] winLine)
    {
        var resultsList = new ListsForAnalys();
        foreach (SymbolFotAnalyzer symbol in symbols)
        {
            SeparatingSymbols(resultsList, symbol, winLine);
        }
        return resultsList;
    }

    private ListsForAnalys SeparatingSymbols(ListsForAnalys resultsList, SymbolFotAnalyzer symbol, int[] winLine)
    {
        bool Anl = false;
        foreach (var WL in winLine)
        {
            if (symbol.SymbolNumberWL == WL)
            {
                resultsList.WinList.Add(symbol);
                Anl = true;
            }
        }
        if (!Anl)
        {
            resultsList.LoseList.Add(symbol);
        }

        return resultsList;
    }

    public void AnimateSymbol(ListsForAnalys resultList)
    {

        var winSymbols = resultList.WinList;
        var loseSymbols = resultList.LoseList;

        foreach (SymbolFotAnalyzer winSymbol in winSymbols)
        {
            winSymbol.ParticleAnimation.Play();
            winSymbol.SymbolAnimation.Play("pulse");
            
        }
        foreach (SymbolFotAnalyzer loseSymbol in loseSymbols)
        {
            loseSymbol.SymbolAnimation.Play("shadow");
        }
    }

    IEnumerator WaitNextWinLines(WinLine[] winLines)
    {

        foreach (var winLine in winLines)
        {
            yield return new WaitUntil(() => !symbols[symbols.Length - 1].SymbolAnimation.isPlaying);
            WinLineCheck(winLine);
        }
        yield return new WaitUntil(() => !symbols[symbols.Length - 1].SymbolAnimation.isPlaying);
        ButtonActivate = true;

    }


    public void StartWinAnalys()    //start fn
    {

        var winLines = gameConfig.WinLines;
        StartCoroutine(WaitNextWinLines(winLines));

    }

    public void StopAnimated()
    {
       
        foreach (SymbolFotAnalyzer symbol in symbols)
        {
            symbol.SymbolAnimation.Stop("pulse");
            symbol.SymbolAnimation.Stop("shadow");
            symbol.SymbolImage.DOFade(1, 0.1f);
            symbol.ParticleAnimation.Stop();
            symbol.SymbolImage.rectTransform.sizeDelta = new Vector2(150,150);
        }
       
    }

}
