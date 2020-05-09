using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Dialogue
{
    public class NextDialogue : MonoBehaviour
    {
        [Header("Component")]
        private DialogueManager dialogManag;

        void OnEnable()
        {
            dialogManag = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
            dialogManag.isAuto = false;
            dialogManag.NextEchange();
            
        }
    }
}