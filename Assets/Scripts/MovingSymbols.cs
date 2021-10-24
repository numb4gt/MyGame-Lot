using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MovingSymbols : MonoBehaviour
{
    [SerializeField] private RectTransform[] symbols;
    [SerializeField] private Sprite[] sprites;
    private readonly float exitSymbol = 139.9f;
    [SerializeField]private float startPositionY;

    [SerializeField] private RectTransform mainCanvas;
    private float mainCanvasScale;

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
        foreach(var symbol in symbols)
        {
            var SymbolPosY = symbol.localPosition;
            var newPos = SymbolPosY.y + ReelStartPosY;
            symbol.localPosition = new Vector3(SymbolPosY.x, newPos);
        }

    }

    private void ChangeSprite(RectTransform symbolSprite)
    {
        var random = Random.Range(0, sprites.Length);
        symbolSprite.GetComponent<Image>().sprite = sprites[random];
    }

    private void MoveUp(RectTransform symbolMove)
    {
        var offset = symbolMove.position.y + startPositionY * mainCanvasScale;
        var newPosition = new Vector3(symbolMove.position.x, offset);
        symbolMove.position = newPosition;
    }
}
