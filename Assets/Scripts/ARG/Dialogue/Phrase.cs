using UnityEngine;
using UnityEngine.UI;

namespace Dialogue
{
    [CreateAssetMenu(fileName = "New_Echange", menuName = "Dialogue/Paragraphe")]
    public class Phrase : ScriptableObject
    {

        [Header("Spite interlocuteur")]
        public Sprite interlocuteur;

        [Header("Interlocuteur")]
        public string nameInterlocuteur;

        [Header("Paragraphe")]
        [TextArea(2, 10)] public string[] _phrase;


    }
}