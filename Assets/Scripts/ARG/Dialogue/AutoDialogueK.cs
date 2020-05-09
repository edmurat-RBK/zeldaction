using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Dialogue
{
    public class AutoDialogueK : MonoBehaviour
    {
        [Header("Component")]
        private DialogueManager dialogManag;

        [Header("Component")]
        public Conversation Intro = null;
        //public PlayableDirector playableDirector; //joue la cinématique
        //public TimelineAsset timeline; //renseigner les élément de cinématique

        void OnEnable()
        {
            dialogManag = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
            dialogManag.BeginCoversation(Intro);
            dialogManag.isAuto = true;
        }

        private void Update()
        {
            if (dialogManag.haveEnd)
            {
                //timeline.GetOutputTracks();
                //playableDirector.Stop();
            }
        }
    }
}