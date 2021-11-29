using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Symbol Data", menuName = "Symbol Data")]

public class Symbols : ScriptableObject
{
    [SerializeField] private Sprite symbolImage;
    [SerializeField] private float symbolCoast;
    [SerializeField] private symbolType symbolType;
    [SerializeField] public bool isWin;
    [SerializeField] public int numberForFS;


    public Sprite SymbolImage => symbolImage;
    public float SymbolCoast => symbolCoast;

    public int NumberForFS => numberForFS;

    internal symbolType SymbolType => symbolType;

    public bool IsWin => isWin;
}
