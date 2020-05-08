using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName ="Dialogue/Character")]
public class Character : ScriptableObject
{
    public string nameOfCharacter;
    public Sprite sprite;
}
