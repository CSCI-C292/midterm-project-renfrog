using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Instruction")]

public class Instruction : ScriptableObject
{
    public string Keyword;
    public string[] Response;
}
