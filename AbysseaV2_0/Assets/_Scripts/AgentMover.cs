using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMover : MonoBehaviour
{
    //COMPONENTES
    Rigidbody2D rb;

    [SerializeField]
    private float acceleration, deacceleration, impulseForce;
    private float currentSpeed;

    float maxSpeed;
    public float MaxSpeed { get; set; }


    Vector2 movementInput;

    public Vector2 MovementInput { get; set; }

    public bool IsDashing { get; set; }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void FixedUpdate()
    {
        if (IsDashing)
            return;

        if(MovementInput != Vector2.zero && currentSpeed >= 0)
        {
            currentSpeed += acceleration * maxSpeed * Time.deltaTime;
        }
        else
        {
            currentSpeed -= deacceleration * maxSpeed * Time.deltaTime;
        }

        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
        rb.velocity = MovementInput.normalized * currentSpeed;
    }

    public void ImpulseAgent()
    {
        if(MovementInput == Vector2.zero)
        {
            rb.AddForce(transform.right * impulseForce, ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(MovementInput * impulseForce, ForceMode2D.Impulse);
        }
    }
}
