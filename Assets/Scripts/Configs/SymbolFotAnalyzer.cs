using UnityEngine;
using UnityEngine.UI;

public class SymbolFotAnalyzer : MonoBehaviour
{
    [SerializeField] private Animation symbolAnimation;   //pulse or shadow 
    [SerializeField] private int symbolNumberFromWinLine;
    [SerializeField] private int symbolNumberFromFinalScreen; //updated with every FinalScreen in the gamem Analyzer
    [SerializeField] private symbolType symbolType;


    //[SerializeField] private RectTransform symbolRT;
    //public RectTransform SymbolRT { get => symbolRT; set => symbolRT = value; }
    public int SymbolNumberWL { get => symbolNumberFromWinLine; set => symbolNumberFromWinLine = value; }
    public int SymbolNumberFS { get => symbolNumberFromFinalScreen; set => symbolNumberFromFinalScreen = value; }
    public Animation SymbolAnimation { get => symbolAnimation; set => symbolAnimation = value; }
    internal symbolType SymbolType { get => symbolType; set => symbolType = value; }
}

