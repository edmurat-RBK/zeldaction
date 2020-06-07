using UnityEngine;
using System.Collections;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Dialogue
{
    public class BeginDialogue : MonoBehaviour
    {
        [Header("Component")]
        private DialogueManager dialogManag;

        [Header("Component")]
        public Conversation Intro = null;
        public Collider2D triggerZone;

        private bool lockConv;

        //public PlayableDirector playableDirector;
        //public TimelineAsset timeline;

        private void Awake()
        {
            triggerZone = GetComponent<Collider2D>();
        }
        void OnEnable()
        {
            dialogManag = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
            //if la bool est true alors lance la fonction derrière
            dialogManag.isAuto = false;
            dialogManag.BeginCoversation(Intro);
            triggerZone.enabled = false; 
        }

        private void Update()
        {
            //if (Input.GetButtonDown("X"))
            //{
            //    dialogManag.NextEchange();
            //}

            if (dialogManag.haveEnd)
            {
                //timeline.GetOutputTracks();
                //playableDirector.Stop();
                triggerZone.enabled = true;
                this.enabled = false;
            }
        }

        //fonction pour chopper dans une liste de dialogue différentes
    }

    
}