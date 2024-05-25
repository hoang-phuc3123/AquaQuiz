using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float MovementSpeed = 5f;
    [SerializeField] private SpriteRenderer armSprite;
    [SerializeField] private SpriteRenderer robSprite; 
    private bool isFishing = false;

    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator animator;

    private enum Direction
    {
        North,
        South,
        East,
        West
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        armSprite = transform.Find("Player_289").GetComponent<SpriteRenderer>();
        robSprite = armSprite.transform.Find("Tools_125").GetComponent<SpriteRenderer>();
    }

    private void OnMovement(InputValue value)
    {
        if(!isFishing)
        {
            movement = value.Get<Vector2>();
            if(movement.x !=0 || movement.y !=0)
            {
                animator.SetFloat("X",movement.x);
                animator.SetFloat("Y",movement.y);
                
                animator.SetBool("IsWalking",true);
            }
            else
            {
                animator.SetBool("IsWalking", false);
            } 
        }     
    }
    private void OnFishing()
    {
        isFishing = !isFishing; 
        if (isFishing)
        {
            animator.ResetTrigger("Reeling");
            animator.SetTrigger("Fishing");
            animator.speed = 1;

            armSprite.enabled = true;
            robSprite.enabled = true;

        }else
        {
            StartCoroutine(HandleReelingAnimation());
        }
    }
    private IEnumerator HandleReelingAnimation()
    {
        animator.ResetTrigger("Fishing");
        animator.SetTrigger("Reeling");

        // Wait until the reeling animation has finished
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        armSprite.enabled = false;
        robSprite.enabled = false;
    }
    
    // Update is called once per frame
    void Update()
    {
      rb.MovePosition(rb.position + movement * MovementSpeed * Time.fixedDeltaTime);
    }
}
