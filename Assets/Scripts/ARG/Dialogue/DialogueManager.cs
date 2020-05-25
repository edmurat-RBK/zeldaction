using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using Management;

namespace Dialogue
{
    public class DialogueManager : MonoBehaviour
    {
        //[SerializeField] private InputManager _input = null;

        [Header("Dialogue")]
        public Conversation _actualConversation;
        private Phrase _actualPhrase;
        private int _PhraseCounter = 0;
        private int _SentanceCounter = 0;

        [Header("UI Elements")]
        [SerializeField] private RectTransform _textBox = null;
        [SerializeField] private Image _Sprite = null;
        [SerializeField] private Sprite _SpriteBase = null;
        [SerializeField] private Text _nom = null;
        [SerializeField] private Text _dialogues = null;
        [SerializeField] private Image bouton;

        [Header("Variable")]
        [SerializeField] [Range(0, 0.05f)]
        private float DelayBetweenLetters = 0.05f;
        [SerializeField]
        private float tempsLectParLettre = 0.1f;
        public bool haveEnd = false;
        public bool isAuto = false;

        void Start()
        {
            //_input = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputManager>();
        }

        public void BeginCoversation(Conversation conversation)
        {
            if (isAuto)
            {
                bouton.enabled = (false);
            }
            else
            {
                bouton.enabled = (true);
            }
            

            _actualConversation = conversation;

            _PhraseCounter = 0;

            _actualPhrase = _actualConversation.phraseList[_PhraseCounter];

            _SentanceCounter = 0;

            haveEnd = false;

            StartCoroutine(StartDialogue());
        }

        private IEnumerator StartDialogue()
        {
            float enterTime = 0.3f;

            LeanTween.moveY(_textBox, -185, enterTime);

            yield return new WaitForSeconds(enterTime);
           
            NextEchange();
        }

        private IEnumerator TypeSentence(string name, string phrase)
        {
            _dialogues.text = "";
            _nom.text = "";

            _Sprite.sprite = _actualPhrase.interlocuteur;
            _nom.text = name;


            foreach (char letter in phrase.ToCharArray())
            {
                _dialogues.text += letter;

                if (isAuto == false)
                {
                    if (!Input.GetButtonDown("RB"))
                    {
                        yield return new WaitForSeconds(DelayBetweenLetters);
                    }
                }
                
            }


            float wait = phrase.Length * tempsLectParLettre;

            if (!Input.GetButtonDown("X"))
            {
                yield return new WaitForSeconds(wait);
            }

            yield return new WaitForSeconds(0.1f);

            DisplayNextSentence();
        }

        public void DisplayNextSentence()
        {
            if (_SentanceCounter >= _actualPhrase._phrase.Length)
            {
                if (isAuto)
                {
                    NextEchange();
                }
                return;
            }

            if (haveEnd)
            {
                //StartDialogue();
                haveEnd = false;

            }

            StopAllCoroutines();
            StartCoroutine(TypeSentence(_actualPhrase.nameInterlocuteur, _actualPhrase._phrase[_SentanceCounter]));
            
            _SentanceCounter++;


        }

        public void NextEchange()
        {
            if (_PhraseCounter >= _actualConversation.phraseList.Length)
            {
                EndDialogue();
                return;
            }

            StopAllCoroutines();

            _actualPhrase = _actualConversation.phraseList[_PhraseCounter];
            _SentanceCounter = 0;

            DisplayNextSentence();

            _PhraseCounter++;

        }

        public void EndDialogue()
        {
            StopAllCoroutines();
            _dialogues.text = "";
            _nom.text = "";
            _Sprite.sprite = _SpriteBase;
            LeanTween.moveY(_textBox, -300, 0.3f);
            haveEnd = true;

        }
    }
}