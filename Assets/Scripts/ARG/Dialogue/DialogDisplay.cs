using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogDisplay : MonoBehaviour
{
    public Conversation conversation;
    public GameObject speaker;

    private SpeakerUI speakerUi;

    private int activeIndex = 0;

    private void Start()
    {
        speakerUi = speaker.GetComponent<SpeakerUI>();
        speakerUi.Speaker = conversation.characterWhoSpeak;
    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            AdvanceConversation();
        }
    }

    void AdvanceConversation()
    {
        if (activeIndex < conversation.lines.Length)
        {
            DisplayLine();
            activeIndex += 1;
        }
        else
        {
            speakerUi.Hide();
            activeIndex = 0;
        }
    }

    void DisplayLine()
    {
        Line line = conversation.lines[activeIndex];
        Character character = line.character;

        if (speakerUi.SpeakerIs(character))
        {
            SetDialog(speakerUi, line.text);
        }
    }

    void SetDialog(SpeakerUI speaker, string text)
    {
        speaker.Dialog = text;
        speaker.Show();
    }
}
