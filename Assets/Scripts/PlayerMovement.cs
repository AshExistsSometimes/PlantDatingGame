using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    Vector2 mousePos;

    PlayerInput input;
    [SerializeField] Camera cam;
    SunGame sunPot;
    WaterGame waterGame;
    DateChooser date;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        input = GetComponent<PlayerInput>();
        sunPot = FindFirstObjectByType<SunGame>();
        waterGame = FindFirstObjectByType<WaterGame>();
        date = FindFirstObjectByType<DateChooser>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            // Try to move to another place
            Ray ray = cam.ScreenPointToRay(mousePos);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject.TryGetComponent(out TravelPoint point))
                {
                    transform.position = point.travelPoint;
                    transform.rotation = Quaternion.Euler(point.travelEulerRotation);
                }
                else if (hit.collider.gameObject.TryGetComponent(out Saul saul))
                {
                    // Kill Saul :(
                    Destroy(saul.gameObject);
                    date.canSaul = false;
                }
            }
            // Start watering
            if (waterGame.gameRunning)
            {
                waterGame.pouringWater = true;
            }
        }
        else if (ctx.canceled)
        {
            // Stop watering
            if (waterGame.gameRunning)
            {
                waterGame.pouringWater = false;
            }
        }
    }

    public void OnPoint(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            mousePos = ctx.ReadValue<Vector2>();
            // Move the pot to the cursor
            if (sunPot.gameRunning)
            {
                Ray ray = cam.ScreenPointToRay(mousePos);
                if (Physics.Raycast(ray, out RaycastHit hit, 200f, ~LayerMask.GetMask("Pot"), QueryTriggerInteraction.Ignore))
                {
                    sunPot.transform.parent.position = hit.point;
                }
            }
        }
    }

}
