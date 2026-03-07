using UnityEngine;
using TMPro;
using System.Collections;

public class TextBoxWriter : MonoBehaviour
{
    
    [SerializeField] DialogueLine tempDialogue;

    // 60 times per second
    [SerializeField] float drawDelay = 0.016f;

    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI textBox;

    [SerializeField] AudioSource voicePlayer;

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
        nameText.text = dialogue.characterName;
        if (dialogue.dialogueVO)
        {
            voicePlayer.resource = dialogue.dialogueVO;
            voicePlayer.Play();
        }

        // Clear the text box
        textBox.text = "";

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
                yield return new WaitForSeconds(drawDelay);
            }
            // Wait for the current message chunk to be over. This isn't called until after the text is printed, so remove the time that has already passed
            yield return new WaitForSeconds(dialogue.chunkDuration[i] - (drawDelay * dialogue.messageChunks[i].Length));
        }
    }

}
