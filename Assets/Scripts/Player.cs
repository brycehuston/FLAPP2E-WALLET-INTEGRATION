using UnityEngine;

public class Player : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    private int spriteIndex;
    private Vector3 direction;

    public float gravity = -9.8f;
    public float strength = 5f;
    [SerializeField] private AudioSource jumpSoundEffect;

    private float maxFallSpeed = -15f;
    private float maxRiseSpeed = 8f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
    }

    private void OnEnable()
    {
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;
        direction = Vector3.zero;
    }

    private void Update()
    {
        if (!enabled) return;

        bool jump = Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0);

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            jump = true;
        }

        if (jump)
        {
            direction = Vector3.up * strength;
            jumpSoundEffect?.Play();
        }

        direction.y += gravity * Time.deltaTime;
        direction.y = Mathf.Clamp(direction.y, maxFallSpeed, maxRiseSpeed);

        transform.position += direction * Time.deltaTime;
    }

    private void AnimateSprite()
    {
        spriteIndex = (spriteIndex + 1) % sprites.Length;
        spriteRenderer.sprite = sprites[spriteIndex];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!enabled) return;

        if (other.CompareTag("Obstacle"))
        {
            FindObjectOfType<GameManager>().GameOver();
        }
        else if (other.CompareTag("Scoring"))
        {
            FindObjectOfType<GameManager>().IncreaseScore(1);
        }
    }
}
