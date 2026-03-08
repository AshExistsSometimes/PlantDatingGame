using UnityEngine;
using UnityEngine.UI;

public class SunGame : MonoBehaviour
{


    // Sun threshold 1 does nothing. If you fail to reach threshold 2 then Saul is your only hope
    [SerializeField] float sunThreshold1 = 0f;
    [SerializeField] float sunThreshold2 = 10f;
    [SerializeField] float sunThreshold3 = 20f;

    [SerializeField] float gameDuration = 30f;
    public bool gameRunning;
    float gameTime;
    
    [SerializeField] GameObject sunbeamPivot;
    [SerializeField] Slider sunSlider;
    [SerializeField] float sunbeamFullRotation = 150f;
    float sunExposure;
    DateChooser date;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        date = FindFirstObjectByType<DateChooser>();
        sunSlider.maxValue = gameDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameRunning)
        {
            sunbeamPivot.transform.Rotate(0f, (sunbeamFullRotation / gameDuration) * Time.deltaTime, 0f);
            sunSlider.value = sunExposure;
            gameTime += Time.deltaTime;
            if (gameTime > gameDuration)
            {
                EndMinigame();
            }
        }

        // Reset, then start the game
        if (Input.GetKeyDown(KeyCode.A))
        {
            gameRunning = true;
            sunSlider.gameObject.SetActive(true);
            gameTime = 0f;
            sunExposure = 0f;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Sunbeam"))
        {
            sunExposure += Time.fixedDeltaTime;
        }
    }

    void EndMinigame()
    {
        gameRunning = false;
        sunSlider.gameObject.SetActive(false);
        // Choose who should get points based on the sunlight
        if (sunExposure >= sunThreshold3)
        {
            date.jasmine += 1;
        }
        else if (sunExposure >= sunThreshold2)
        {
            date.AD += 1;
        }
        // No sunlight for Saul :(
    }

}
