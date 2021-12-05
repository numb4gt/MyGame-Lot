using UnityEngine;
using UnityEngine.UI;

public class SymbolFotAnalyzer : MonoBehaviour
{
    [SerializeField] private Animation symbolAnimation;   //pulse or shadow 
    [SerializeField] private ParticleSystem particleAnimation;
    [SerializeField] private Image symbolImage;
    [SerializeField] private int symbolNumberFromWinLine;
    [SerializeField] private int symbolNumberFromFinalScreen; //updated with every FinalScreen in the gamem Analyzer
    [SerializeField] private symbolType symbolType;
    [SerializeField] private int symbolWin;


    public Image SymbolImage { get => symbolImage; set => symbolImage = value; }
    public int SymbolWin { get => symbolWin; set => symbolWin = value; }
    public int SymbolNumberWL { get => symbolNumberFromWinLine; set => symbolNumberFromWinLine = value; }
    public int SymbolNumberFS { get => symbolNumberFromFinalScreen; set => symbolNumberFromFinalScreen = value; }
    public Animation SymbolAnimation { get => symbolAnimation; set => symbolAnimation = value; }
    public ParticleSystem ParticleAnimation { get => particleAnimation; set => particleAnimation = value; }
    internal symbolType SymbolType { get => symbolType; set => symbolType = value; }
}

