using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    Vector2 mousePos;

    PlayerInput input;
    [SerializeField] Camera cam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        input = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            Ray ray = cam.ScreenPointToRay(mousePos);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject.TryGetComponent(out TravelPoint point))
                {
                    transform.position = point.travelPoint;
                    transform.rotation = Quaternion.Euler(point.travelEulerRotation);
                }
            }
        }
    }

    public void OnPoint(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            mousePos = ctx.ReadValue<Vector2>();
        }
    }

}
