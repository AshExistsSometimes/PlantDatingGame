using UnityEngine;
using UnityEngine.UI;

public class WaterGame : MonoBehaviour
{

    public bool gameRunning;
    public bool pouringWater;

    float waterGiven;
    float pourSpeed;

    [SerializeField] float pourSpeedRamp = 0.3f;
    [SerializeField] float maxPourSpeed = 1f;
    [SerializeField] Vector3 pourRotation;
    [SerializeField] GameObject wateringCan;

    DateChooser date;
    [SerializeField] Image waterIndicator;
    [SerializeField] Sprite[] waterSprites;

    void Start()
    {
        date = FindFirstObjectByType<DateChooser>();
    }

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.W))
        // {
        //     gameRunning = true;
        //     waterGiven = 0f;
        //     waterIndicator.gameObject.SetActive(true);
        //     wateringCan.SetActive(true);
        // }
        // else if (Input.GetKeyDown(KeyCode.E))
        // {
        //     EndGame();
        // }
        if (pouringWater)
        {
            pourSpeed += pourSpeedRamp * Time.deltaTime;
        }
        else
        {
            pourSpeed -= pourSpeedRamp * Time.deltaTime;
        }

        pourSpeed = Mathf.Clamp(pourSpeed, 0f, maxPourSpeed);
        waterGiven += pourSpeed * Time.deltaTime;

        wateringCan.transform.localRotation = Quaternion.Euler(Vector3.Slerp(Vector3.zero, pourRotation, pourSpeed / maxPourSpeed));
        waterIndicator.sprite = waterSprites[Mathf.FloorToInt(Mathf.Clamp(waterGiven, 0, waterSprites.Length - 1))];
    }

    void EndGame()
    {
        gameRunning = false;
        pouringWater = false;
        pourSpeed = 0f;
        waterIndicator.gameObject.SetActive(false);
        wateringCan.SetActive(false);
        if (waterGiven >= 3)
        {
            date.AD++;
            Debug.Log("Added point for AD");
        }
        else if (waterGiven >= 2)
        {
            date.jasmine++;
            Debug.Log("Added point for Jasmine");
        }
        // Saul has no points. Saul needs no points.
    }
}
