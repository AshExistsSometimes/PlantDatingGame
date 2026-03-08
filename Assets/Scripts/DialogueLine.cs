using UnityEngine;

[CreateAssetMenu(fileName="New Dialogue", menuName="Dialogue/Line")]
public class DialogueLine : ScriptableObject
{
    
    public string characterName;
    public Sprite characterBoxPortrait;
    public Sprite[] characterFullPortraits;

    // Split into chunks to wait for the voiceover to catch up
    public string[] messageChunks;
    public float[] chunkDuration;
    public DialogueLine nextLine;
    public DialogueChoice nextChoice;
    public AudioClip dialogueVO;

}
