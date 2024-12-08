using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    void Start()
    {
        rb = gameObject.GetComponentInParent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (rb.linearVelocityX > 0)
        {
            Debug.Log("Right");
            anim.SetInteger("Vector", 1);
        }
        if (rb.linearVelocityX < 0)
        {
            Debug.Log("Left");
            anim.SetInteger("Vector", -1);
        }
        if (rb.linearVelocityX == 0)
            anim.SetInteger("Vector", 0);
    }
}
