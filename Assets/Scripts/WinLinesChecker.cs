using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLinesChecker : MonoBehaviour
{
    [SerializeField] private SymbolFotAnalyzer[] symbols;
    [SerializeField] private GameConfig gameConfig;
    public bool ButtonActivate = false;


    private void WinLineCheck(WinLine winLine)
    {
        var List = AnalyzOfWinSymbols(winLine.WinLines);

        var winSymbol1 = List.WinList[0];
        var winSymbol2 = List.WinList[1];
        var winSymbol3 = List.WinList[2];


        if (winSymbol1.SymbolType == winSymbol2.SymbolType && winSymbol2.SymbolType == winSymbol3.SymbolType)
        {
            AnimateSymbol(List);
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

    private void AnimateSymbol(ListsForAnalys resultList)
    {

        var winSymbols = resultList.WinList;
        var loseSymbols = resultList.LoseList;

        foreach (SymbolFotAnalyzer winSymbol in winSymbols)
        {
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

}
