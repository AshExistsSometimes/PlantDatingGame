using UnityEngine;

public class Saul : MonoBehaviour
{

    [SerializeField] float moveSpeed = 0.5f;
    [SerializeField] GameObject target;
    DateChooser date;
    Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        date = FindFirstObjectByType<DateChooser>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
        rb.linearVelocity = transform.forward * moveSpeed;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Plant"))
        {
            // Saul is a hungry fella and doesn't mind if his meals are sentient
            date.jasmine -= 2;
            date.AD -= 2;
            Debug.Log("Crunch crunch crunch");
        }
    }
}
