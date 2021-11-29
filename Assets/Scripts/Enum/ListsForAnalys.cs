using System.Collections.Generic;

public class ListsForAnalys
{
    public List<SymbolFotAnalyzer> WinList { get; set; }
    public List<SymbolFotAnalyzer> LoseList { get; set; }

    public ListsForAnalys()
    {
        WinList = new List<SymbolFotAnalyzer>();
        LoseList = new List<SymbolFotAnalyzer>();
    }
}