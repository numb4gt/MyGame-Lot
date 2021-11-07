using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Symbol Data", menuName = "Symbol Data")]

public class Symbols : ScriptableObject
{
    [SerializeField] private Sprite symbolImage;
    [SerializeField] private float symbolCoast;
    [SerializeField] private symbolType symbolType;

    public Sprite SymbolImage => symbolImage;
    public float SymbolCoast => symbolCoast;

    internal symbolType SymbolType => symbolType;
}
