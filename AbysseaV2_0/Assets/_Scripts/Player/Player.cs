using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //MOVIMENTAÇÃO
    AgentMover playerMover;

    [SerializeField]
    private float playerMoveSpeed;

    bool isMoving;

    float dashCD = 1f, dashingTime = 0.2f;
    bool canDash;

    //INPUTS E ACTIONS
    private Vector2 movementInput, pointerInput;
    public Vector2 MovementInput { get => movementInput; set => movementInput = value; }
    public Vector2 PointerInput { get => pointerInput; set => pointerInput = value; }

    //COMPONENTES
    Animator anim;
    SpriteRenderer sr;

    bool isDead;

    private void OnEnable()
    {
        isDead = false;
        canDash = true;
    }

    private void OnDisable()
    {
        isDead = true;
    }

    void Start()
    {
        playerMover = GetComponent<AgentMover>();

        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }
  
    void Update()
    {
        if (isDead)
            return;

        //MOVIMENTAÇÃO
        movementInput = MovementInput;
        playerMover.MaxSpeed = playerMoveSpeed;
        playerMover.MovementInput = movementInput;

        PlayMovementAnimation();
    }

    //MOVIMENTAÇÃO BÁSICA
    private void PlayMovementAnimation()
    {
        if (movementInput.x == 0 && movementInput.y > 0)
        {
            anim.SetBool("BackWalk", true);
            anim.SetBool("HorWalk", false);
            anim.SetBool("FrontWalk", false);
        }
        else if (movementInput.x == 0 && movementInput.y < 0)
        {
            anim.SetBool("BackWalk", false);
            anim.SetBool("HorWalk", false);
            anim.SetBool("FrontWalk", true);
        }
        else if (movementInput.x > 0)
        {
            sr.flipX = false;

            anim.SetBool("BackWalk", false);
            anim.SetBool("HorWalk", true);
            anim.SetBool("FrontWalk", false);
        }
        else if (movementInput.x < 0)
        {
            sr.flipX = true;

            anim.SetBool("BackWalk", false);
            anim.SetBool("HorWalk", true);
            anim.SetBool("FrontWalk", false);
        }
        else
        {
            anim.SetBool("BackWalk", false);
            anim.SetBool("HorWalk", false);
            anim.SetBool("FrontWalk", false);
        }
    }

    //DASH
    public void PlayerDash()
    {
        if(GameManager.Instance.playerResourcesController.O2 < 10f || canDash == false)
        {
            //Warnings O2 está baixo;
            return;
        }

        GameManager.Instance.playerResourcesController.O2 -= 10f;

        playerMover.IsDashing = true;
        canDash = false;
        playerMover.ImpulseAgent();
        //trail emmiting
        //som de dash

        StartCoroutine(DashCoolDown());
    }

    IEnumerator DashCoolDown()
    {
        yield return new WaitForSeconds(dashingTime);
        playerMover.IsDashing = false;
        //parar trail emmiting

        yield return new WaitForSeconds(dashCD);
        canDash = true;
    }

    //ATAQUE PRIMÁRIO
    //ATAQUE SECUNDÁRIO
    //CONTROLE DE VIDA
    //CONTROLE DE RECURSOS
}
