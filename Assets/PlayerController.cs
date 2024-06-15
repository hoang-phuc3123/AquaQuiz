using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float MovementSpeed = 5f;
    [SerializeField] private SpriteRenderer armSprite;
    [SerializeField] private SpriteRenderer robSprite;
    [SerializeField] private GameObject bobberPrefab;
    private FishCollection fishCollection;
    [SerializeField] private FishingManager fishingManager;

    [SerializeField] private float bobberOffset = 1.5f;
    [SerializeField] private float bobberRightLeftOffset = -1f; // Offset distance for bobber when fishing right or left
    [SerializeField] private float bobberUpDownOffset = 2f; // Offset distance for bobber when fishing up or down
    [SerializeField] private GameObject groundObject; // Reference to the ground object with TilemapCollider2D
    private CompositeToggle compositeToggle;
    private bool isFishing = false;
    private bool isFishingRodThrown = false;
    private Vector2 lastMovementDirection;

    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator animator;
    public LayerMask layerMask;
    private PlayerInput playerInput;
    private GameObject currentBobber;

    private enum Direction
    {
        North,
        South,
        East,
        West
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        armSprite = transform.Find("Player_289").GetComponent<SpriteRenderer>();
        robSprite = armSprite.transform.Find("Tools_125").GetComponent<SpriteRenderer>();
        fishCollection = FindObjectOfType<FishCollection>();
        playerInput = GetComponent<PlayerInput>();
        compositeToggle = groundObject.GetComponent<CompositeToggle>();
    }

    private void OnMovement(InputValue value)
    {
        if (!isFishing && !isFishingRodThrown)
        {
            movement = value.Get<Vector2>();
            if (movement.x != 0 || movement.y != 0)
            {
                lastMovementDirection = movement.normalized;
                animator.SetFloat("X", movement.x);
                animator.SetFloat("Y", movement.y);
                animator.SetBool("IsWalking", true);
            }
            else
            {
                animator.SetBool("IsWalking", false);
            }
        }
        else
        {
            movement = Vector2.zero;
        }
    }

    public void Test()
    {
        Debug.Log("Test");
    }

    private void OnFishing()
    {
        if (!isFishing && !isFishingRodThrown)
        {
            //if (compositeToggle != null)
            //{
            //    compositeToggle.DisableComposite();
            //}
            animator.ResetTrigger("Reeling");
            animator.SetTrigger("Fishing");
            animator.speed = 1;
            movement = Vector2.zero;
            StartCoroutine(ThrowFishingRod());
            
        }
        else if (isFishingRodThrown)
        {
            CancelFishing(false);
            fishingManager.EndFishing();
        }
    }
    private void OnOpen()
    {
        // Toggle the enabled state of the Canvas
        fishCollection.fishCollectionUI.transform.parent.GetComponent<Canvas>().enabled = true;
        DisableInput();
    }

    private IEnumerator ThrowFishingRod()
    {
        yield return new WaitForSeconds(0.0f); // Adjust this as needed for your casting animation

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 10f, layerMask);

        

        if (hit.collider == null || !hit.collider.gameObject.name.Equals("Ground")) // Check if NOT colliding with Decore (4)
        {

            armSprite.enabled = true;
            robSprite.enabled = true;
            isFishingRodThrown = true;

            yield return new WaitForSeconds(0.5f);

            SpawnBobber();

            StartCoroutine(WaitForFishToBite());
        }
        else
        {
            Debug.Log("Hit land! Reeling in...");
            CancelFishing(false);
        }
    }

    private IEnumerator HandleReelingAnimation(bool waitInfo)
    {
        animator.ResetTrigger("Fishing");
        animator.SetTrigger("Reeling");

        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        armSprite.enabled = false;
        robSprite.enabled = false;
        if (!waitInfo)
        {
            EnableInput();
        }
        if (currentBobber != null)
        {
            Destroy(currentBobber);
            currentBobber = null;
        }
    }


    private IEnumerator WaitForFishToBite()
    {
        float randomDelay = Random.Range(3f, 10f);
        yield return new WaitForSeconds(randomDelay);

        if (isFishingRodThrown)
        {
            playerInput.actions.FindActionMap("InGame").Disable();
            fishingManager.StartFishing();
            isFishingRodThrown = false;
            isFishing = true;
        }
    }

    public void CancelFishing(bool waitInfo)
    {
        isFishing = false;
        isFishingRodThrown = false;
        StopAllCoroutines();
        movement = Vector2.zero;
        animator.SetBool("IsWalking", false);
        StartCoroutine(HandleReelingAnimation(waitInfo));
    }

    public void EnableInput()
    {
        playerInput.actions.FindActionMap("InGame").Enable();
    }

    public void DisableInput()
    {

        playerInput.actions.FindActionMap("InGame").Disable();
    }
    private void SpawnBobber()
    {
        Vector3 bobberPosition = transform.position;

        if (lastMovementDirection.x > 0)
        {
            bobberPosition += Vector3.right * bobberRightLeftOffset;
            bobberPosition += Vector3.down * 0.5f;
        }
        else if (lastMovementDirection.x < 0)
        {
            bobberPosition += Vector3.left * bobberRightLeftOffset;
            bobberPosition += Vector3.down * 0.5f;
        }
        else if (lastMovementDirection.y > 0)
        {
            bobberPosition += Vector3.up * bobberUpDownOffset;
        }
        else if (lastMovementDirection.y < 0)
        {
            bobberPosition += Vector3.down * bobberUpDownOffset * 1.25f;
        }

        if (currentBobber != null)
        {
            Destroy(currentBobber);
        }

        currentBobber = Instantiate(bobberPrefab, bobberPosition, Quaternion.identity);
    }
    void Update()
    {
        if (!isFishing && !isFishingRodThrown)
        {
            rb.MovePosition(rb.position + movement * MovementSpeed * Time.fixedDeltaTime);
        }
    }
}
