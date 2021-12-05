using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReelAnalyzerAfterSpin : MonoBehaviour
{

    [SerializeField] private SymbolFotAnalyzer[] symbolsAn;
    [SerializeField] GameConfig gameConfig;
    private int FinalScreenCounter = 0;


    private void SymbolTypeAnalys(SymbolFotAnalyzer symbol)
    {
        if (symbol.SymbolNumberFS == 0) //make with case
        {
            symbol.SymbolType = symbolType.blue_flower;
            symbol.SymbolWin = 5;
        }
        else if (symbol.SymbolNumberFS == 1)
        {
            symbol.SymbolType = symbolType.butterfly;
            symbol.SymbolWin = 10;
        }
        else if (symbol.SymbolNumberFS == 2)
        {
            symbol.SymbolType = symbolType.scatter_clover;  //remove in 5 parts
            symbol.SymbolWin = 100;
        }
        else if (symbol.SymbolNumberFS == 3)
        {
            symbol.SymbolType = symbolType.frog;
            symbol.SymbolWin = 20;
        }
        else if (symbol.SymbolNumberFS == 4)
        {
            symbol.SymbolType = symbolType.green_flower;
            symbol.SymbolWin = 5;
        }
        else if (symbol.SymbolNumberFS == 5)
        {
            symbol.SymbolType = symbolType.purple_flower;
            symbol.SymbolWin = 5;
        }
        else if (symbol.SymbolNumberFS == 6)
        {
            symbol.SymbolType = symbolType.red_flower;
            symbol.SymbolWin = 5;
        }
        else if (symbol.SymbolNumberFS == 7)
        {
            symbol.SymbolType = symbolType.snail;
            symbol.SymbolWin = 50;
        }
        else if (symbol.SymbolNumberFS == 8)
        {
            symbol.SymbolType = symbolType.yellow_flower;
            symbol.SymbolWin = 5;
        }

    }

    // StartReelSymbolPositionTable
    //        y   x              y        x           y   x                !!! - scatter of values occurs
    // 1.0 = 140 750 | 2.0 = 143.2109!!! 960 | 3.0 = 140 1170               
    // 1.1 = 740 750 | 2.1 = 743.2109!!! 960 | 3.1 = 740 1170 
    // 1.2 = 540 750 | 2.2 = 543.2109!!! 960 | 3.2 = 540 1170
    // 1.3 = 340 750 | 2.3 = 343.2109!!! 960 | 3.3 = 340 1170

    private void SymbolType(SymbolFotAnalyzer symbol)
    {
        var FinalScreen = gameConfig.FinalScreens[FinalScreenCounter].FinalScreeenData;
        var PosY = symbol.GetComponent<RectTransform>();
        //var Name = symbol.name;

        float PosYAfterGameY = PosY.position.y;
        float PosYAfterGameX = PosY.position.x;


        if (PosYAfterGameY < 340 || PosYAfterGameY > 745)
        {
            symbol.SymbolNumberWL = -1;
        } else
        //1 reel
        if (PosYAfterGameX == 750 && PosYAfterGameY >= 740 && PosYAfterGameY <= 745)
        {
            symbol.SymbolNumberWL = 2;
            symbol.SymbolNumberFS = FinalScreen[2];
        } else if (PosYAfterGameX == 750 && PosYAfterGameY >= 540 && PosYAfterGameY <= 545)
        {
            symbol.SymbolNumberWL = 1;
            symbol.SymbolNumberFS = FinalScreen[1];
        }
        else if (PosYAfterGameX == 750 && PosYAfterGameY >= 340 && PosYAfterGameY <= 345)
        {
            symbol.SymbolNumberWL = 0;
            symbol.SymbolNumberFS = FinalScreen[0];
        } else
        //2 reel
        if (PosYAfterGameX == 960 && PosYAfterGameY >= 740 && PosYAfterGameY <= 745)
        {
            symbol.SymbolNumberWL = 5;
            symbol.SymbolNumberFS = FinalScreen[5];
        }
        else if (PosYAfterGameX == 960 && PosYAfterGameY >= 540 && PosYAfterGameY <= 545)
        {
            symbol.SymbolNumberWL = 4;
            symbol.SymbolNumberFS = FinalScreen[4];
        }
        else if (PosYAfterGameX == 960 && PosYAfterGameY >= 340 && PosYAfterGameY <= 345)
        {
            symbol.SymbolNumberWL = 3;
            symbol.SymbolNumberFS = FinalScreen[3];
        }
        else
        //3 reel
        if (PosYAfterGameX == 1170 && PosYAfterGameY >= 740 && PosYAfterGameY <= 745)
        {
            symbol.SymbolNumberWL = 8;
            symbol.SymbolNumberFS = FinalScreen[8];
        }
        else if (PosYAfterGameX == 1170 && PosYAfterGameY >= 540 && PosYAfterGameY <= 545)
        {
            symbol.SymbolNumberWL = 7;
            symbol.SymbolNumberFS = FinalScreen[7];
        }
        else if (PosYAfterGameX == 1170 && PosYAfterGameY >= 340 && PosYAfterGameY <= 345)
        {
            symbol.SymbolNumberWL = 6;
            symbol.SymbolNumberFS = FinalScreen[6];
        }

        //Debug.Log(PosYAfterGameY);
        //Debug.Log(PosYAfterGameX);
        //Debug.Log(Name);
    }

    public void StartAnalysisReel()
    {
        int FinalScreensLength = gameConfig.FinalScreens.Length;

            foreach (var Symbol in symbolsAn)
            {
                SymbolType(Symbol);                            
                SymbolTypeAnalys(Symbol);
            }

        //counter update
        if (FinalScreenCounter == (FinalScreensLength-1))
        {
            FinalScreenCounter = 0;
        }
        else {
            FinalScreenCounter++; 
        }

    }
}
