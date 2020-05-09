using UnityEngine;

namespace Dialogue
{
    [CreateAssetMenu(fileName = "New_Conversation", menuName = "Dialogue/Conversation")]
    public class Conversation : ScriptableObject
    {
        public Phrase[] phraseList;
    }
}