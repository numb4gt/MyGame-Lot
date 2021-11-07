using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Game Config", menuName = "Game Config")]
public class GameConfig : ScriptableObject
{
    [SerializeField] private Symbols[] symbols;
    [SerializeField] private FinalScreenData[] finalScreens;
    [SerializeField] private int visibleSymbolsOnReel;

    public Symbols[] Symbols => symbols;
    public FinalScreenData[] FinalScreens => finalScreens;
    public int VisibleSymbolsOnReel => visibleSymbolsOnReel;
}
