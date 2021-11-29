using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MovingSymbols : MonoBehaviour
{
    [SerializeField] GameConfig gameConfig;
    [SerializeField] private RectTransform[] symbols;
    [SerializeField] private Sprite[] sprites;
    private readonly float exitSymbol = 139.9f;
    [SerializeField] private float startPositionY;

    [SerializeField] private RectTransform mainCanvas;
    private float mainCanvasScale;
    private int currenSymbolIndex = 0;
    private int currenFinalSet = 0;

    [SerializeField] int reelId;
    private ReelState reelState = ReelState.Stop;
    internal ReelState ReelState { get => reelState; set => reelState = value;}

    public int ReelID => reelId;

    private void Start()
    {
        mainCanvasScale = mainCanvas.lossyScale.y;
    }
    private void Update() //+ change sprites
    {
        foreach(var symbol in symbols)
        {
            if(symbol.position.y <= exitSymbol *mainCanvasScale)
            {
                MoveUp(symbol);
                ChangeSprite(symbol);
            }
        }
    }

    public void ResetSymbols(float ReelStartPosY)
    {
        currenSymbolIndex = 0;
        if(currenFinalSet < gameConfig.FinalScreens.Length - 1)
        {
            currenFinalSet++;
        }
        else
        {
            currenFinalSet=0;
        }
        foreach(var symbol in symbols)
        {
            var SymbolPosY = symbol.localPosition;
            var newPos = SymbolPosY.y + ReelStartPosY;
            symbol.localPosition = new Vector3(SymbolPosY.x, newPos);
        }

    }

    private Sprite GetRandomSymbol()
    {
        var random = Random.Range(0, gameConfig.Symbols.Length);
        var sprite = gameConfig.Symbols[random].SymbolImage;
        return sprite;
    }

    private Sprite GetFinalSprite()
    {
        var finalScreenIndex = currenSymbolIndex + (reelId - 1) * gameConfig.VisibleSymbolsOnReel;
        var currentFinalScreen = gameConfig.FinalScreens[currenFinalSet].FinalScreeenData;
        if (finalScreenIndex >= currentFinalScreen.Length)
        {
            currenSymbolIndex = 0;
        }
        var newSymbol = gameConfig.Symbols[currentFinalScreen[finalScreenIndex]];
        return newSymbol.SymbolImage;
    }

    private void ChangeSprite(RectTransform symbolSprite)
    {
        if (reelState == ReelState.Stopping || reelState == ReelState.ForceStopping)
        {
            symbolSprite.GetComponent<Image>().sprite = GetFinalSprite();
            currenSymbolIndex++;
        }
        else
        {
            symbolSprite.GetComponent<Image>().sprite = GetRandomSymbol();
        }
    }

    private void MoveUp(RectTransform symbolMove)
    {
        var offset = symbolMove.position.y + startPositionY * mainCanvasScale;
        var newPosition = new Vector3(symbolMove.position.x, offset);
        symbolMove.position = newPosition;
    }
}
