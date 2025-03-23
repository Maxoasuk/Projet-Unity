using UnityEngine;

public class MobController : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private float moveSpeed = 1.0f;
    [SerializeField] private float life = 10.0f;
    private Vector2 movement;
    private float timeMove = 0.0f;
    [SerializeField] private SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        
        movement = Vector2.zero;
    }

    void TakeDamage(float damage)
    {
        life -= damage;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (life <= 0.0f)
        {
            this.gameObject.SetActive(false);
        }
        if (timeMove < 0.0f)
        {
            timeMove = Random.value * 2.0f;
            int dir = Random.Range(0, 5);
            switch (dir)
            {
                case 0:
                    movement = new Vector2(0, -1.0f);
                    break;
                case 1:
                    movement = new Vector2(0, 1.0f);
                    break;
                case 2:
                    movement = new Vector2(-1.0f, 0);
                    break;
                case 3:
                    movement = new Vector2(1.0f, 0);
                    break;
                default:
                    movement = Vector2.zero;
                    timeMove += 2.0f;
                    break;
            }
            if (movement.x != 0)
                spriteRenderer.flipX = true;
            if (movement.y != 0)
                spriteRenderer.flipX = false;
            
            animator.SetBool("isMove", movement != Vector2.zero);
            animator.SetFloat("MoveX", movement.x);
            animator.SetFloat("MoveY", movement.y);
        }
        else
        {
            timeMove -= Time.fixedDeltaTime;
        }
        
        transform.Translate(movement * (Time.fixedDeltaTime * moveSpeed));
    }
}
