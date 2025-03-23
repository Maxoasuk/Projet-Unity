using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputAsset;
    [SerializeField] private int moveSpeed;
    private Vector2 movement = new Vector2(0,0);
    private Vector2 orientation = new Vector2(1,0);
    private bool isAttackPress = false;
    private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject[] sword;

    private float startTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputAsset.Enable();
        InputActionMap m = inputAsset.FindActionMap("Gameplay");
        InputAction move  = m.FindAction("Move");
        move.started += moveCallback;
        move.performed += moveCallback;
        move.canceled += moveCallback;

        InputAction attack = m.FindAction("Attack");
        attack.started += attackCallback;
        attack.performed += attackCallback;
        attack.canceled += attackCallback;
        
        animator = GetComponent<Animator>();
        startTime = 2.0f;
    }

    private void moveCallback(InputAction.CallbackContext obj)
    {
        movement = obj.ReadValue<Vector2>();
        if (movement != Vector2.zero)
            orientation = movement.normalized;
        
        animator.SetBool("isMove", movement != Vector2.zero);
        animator.SetBool("isSide", movement.x != 0 && movement.y == 0);
        animator.SetFloat("MoveY", movement.y);
    }

    private void attackCallback(InputAction.CallbackContext obj)
    {
        isAttackPress = obj.ReadValue<float>() != 0.0f;
        
        animator.SetBool("isAttack", obj.ReadValue<float>() != 0.0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((LayerMask.GetMask("Mob") & (1 << collision.gameObject.layer)) > 0)
        {
            SceneManager.LoadScene("Menu");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool isStartTime = startTime > 0.0f;
        
        if (!isAttackPress && !isStartTime)
            transform.Translate(movement * (Time.fixedDeltaTime * moveSpeed));

        spriteRenderer.flipX = movement.x switch
        {
            < 0.0f => true,
            > 0.0f => false,
            _ => spriteRenderer.flipX
        };

        if (isAttackPress && movement == Vector2.zero)
        {
            if (orientation.y < 0.0f)
                sword[0].SetActive(true);
            else if (orientation.y > 0.0f)
                sword[1].SetActive(true);
            else if (orientation.x > 0.0f)
                sword[2].SetActive(true);
            else
                sword[3].SetActive(true);
        }
        else
        {
            sword[0].SetActive(false);
            sword[1].SetActive(false);
            sword[2].SetActive(false);
            sword[3].SetActive(false);
        }
        
        if (isStartTime)
            startTime -= Time.fixedDeltaTime;
    }
}
