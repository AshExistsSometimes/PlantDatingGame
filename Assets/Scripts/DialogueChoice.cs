using UnityEngine;

[CreateAssetMenu(fileName = "DialogueChoice", menuName = "Dialogue/Choice")]
public class DialogueChoice : ScriptableObject
{

    public DialogueLine[] choices;
    public string[] choiceStrings;
}
