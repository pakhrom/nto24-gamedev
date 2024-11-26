using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        Vector2 movement = new(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rb.linearVelocity = movement * speed;
    }
}
