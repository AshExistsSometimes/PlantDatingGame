using UnityEngine;

public class DateChooser : MonoBehaviour
{

    public int jasmine;
    public int AD;
    public bool canSaul = true;

    [SerializeField] int friendPoints = 5;
    [SerializeField] int datePoints = 7;

    [SerializeField] TextBoxWriter textBox;

    [SerializeField] DialogueLine saulDialogue;
    [SerializeField] DialogueLine jasmineDialogue;
    [SerializeField] DialogueLine ADDialogue;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textBox = FindFirstObjectByType<TextBoxWriter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            PickDate();
        }
    }

    void PickDate()
    {

        // He's here for you. He doesn't mind if you're not good with dating
        if (canSaul)
        {
            StartDate(saulDialogue);
        }
        else if (jasmine > datePoints)
        {
            StartDate(jasmineDialogue);
        }
        else if (AD > datePoints)
        {
            StartDate(ADDialogue);
        }
    }

    void StartDate(DialogueLine dateDialogue)
    {
        textBox.ReadDialogue(dateDialogue);
    }
}
