using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class TextBoxWriter : MonoBehaviour
{

    [SerializeField] DialogueLine tempDialogue;

    // 60 times per second
    [SerializeField] float drawDelay = 0.016f;

    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI textBox;
    [SerializeField] GameObject dialogueBox;
    [SerializeField] Image portrait;

    [SerializeField] AudioSource voicePlayer;
    [SerializeField] GameObject choiceButtons;
    [SerializeField] TextMeshProUGUI choice1;
    [SerializeField] TextMeshProUGUI choice2;

    DialogueLine lastDialogue;

    bool waitingForLine = false;

    void Start()
    {
        //ReadDialogue(tempDialogue);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            ReadDialogue(tempDialogue);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log(voicePlayer.time);
        }
    }

    public void ReadDialogue(DialogueLine dialogue)
    {
        dialogueBox.SetActive(true);
        nameText.text = dialogue.characterName;
        if (dialogue.dialogueVO)
        {
            voicePlayer.resource = dialogue.dialogueVO;
            voicePlayer.Play();
        }

        if (dialogue.characterFullPortraits.Length > 0 && dialogue.characterFullPortraits[0])
        {
            portrait.sprite = dialogue.characterFullPortraits[0];
        }
        // Clear the text box
        textBox.text = "";

        lastDialogue = dialogue;

        StartCoroutine(WriteText(dialogue));
    }

    public IEnumerator WriteText(DialogueLine dialogue)
    {

        for (int i = 0; i < dialogue.messageChunks.Length; i++)
        {
            // Write each character, then wait briefly
            for (int j = 0; j < dialogue.messageChunks[i].Length; j++)
            {
                textBox.text += dialogue.messageChunks[i][j];
                if (dialogue.characterFullPortraits.Length > i && dialogue.characterFullPortraits[i])
                {
                    portrait.sprite = dialogue.characterFullPortraits[i];
                }
                yield return new WaitForSeconds(drawDelay);
            }
            // Wait for the current message chunk to be over. This isn't called until after the text is printed, so remove the time that has already passed
            yield return new WaitForSeconds(dialogue.chunkDuration[i] - (drawDelay * dialogue.messageChunks[i].Length));
        }

        if (dialogue.nextLine)
        {
            StartCoroutine(WriteText(dialogue));
        }
        else if (dialogue.nextChoice)
        {
            choiceButtons.SetActive(true);
            choice1.text = dialogue.nextChoice.choiceStrings[0];
            choice2.text = dialogue.nextChoice.choiceStrings[1];
        }
    }

    public void MakeChoice(int choice)
    {
        ReadDialogue(lastDialogue.nextChoice.choices[choice]);
    }

}
