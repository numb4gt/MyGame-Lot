using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFinalScreen", menuName = "FinalScreen")]

public class NewBehaviourScript : ScriptableObject
{
    [SerializeField] private int[] finalScreen;

    public int[] FinalScreen => finalScreen;


}
