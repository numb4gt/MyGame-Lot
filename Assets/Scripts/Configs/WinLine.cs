using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Win Line", menuName = "Win Line")]
public class WinLine : ScriptableObject
{
    [SerializeField] private int[] winLine;

    public int[] WinLines => winLine;
}