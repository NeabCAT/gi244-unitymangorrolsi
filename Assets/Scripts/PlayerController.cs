using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public int hp = 3;

    public int jumpCount;
    public float jumpForce;
    public float gravityModifier;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;

    public AudioClip jumpSfx;
    public AudioClip crashSfx;
    public AudioClip ghotAudio;
    public AudioClip bossAudio;

    public AudioClip shootSfx;
    public Transform firePoint;          
    public float shootCooldown = 0.3f;

    private float lastShootTime;
    private InputAction shootAction;

    private Rigidbody rb;
    private InputAction jumpAction;

    //private InputAction dashAction;
    private bool isOnGround = true;

    private Animator playerAnim;
    private AudioSource playerAudio;

    public bool gameOver = false;
    public bool noDamage = false;
    //public bool isDash = false;

    public GameObject cam;
    public TextMeshProUGUI countdownText;

    public float disarmDuration;
    public Renderer playerRenderer;

    private bool isDisarmed = false;
    private float disarmTimer = 0f;
    private Color originalColor;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Physics.gravity = new Vector3(0, -9.81f, 0) * gravityModifier;

        jumpAction = InputSystem.actions.FindAction("Jump");
        shootAction = InputSystem.actions.FindAction("Attack");
        //dashAction = InputSystem.actions.FindAction("Sprint");

        if (playerRenderer == null)
            playerRenderer = GetComponentInChildren<Renderer>();

        originalColor = playerRenderer.material.color;

        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (jumpAction.triggered && jumpCount < 2 && !gameOver)
        {
            rb.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);

            jumpCount++;
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSfx);
        }

        //if (dashAction.IsPressed())
        //{
        //    isDash = true;
        //}
        //else
        //{
        //    isDash = false;
        //}

        if (isDisarmed)
        {
            disarmTimer -= Time.deltaTime;
            if (disarmTimer <= 0f)
            {
                CureDisarm();
            }
        }

        if (shootAction.triggered && !gameOver && !isDisarmed && Time.time >= lastShootTime + shootCooldown)
        {
            Shoot();
        }
    }
    private void Shoot()
    {
        lastShootTime = Time.time;

        var bullet = BulletPool.staticInstance.Acquire(firePoint.position, Quaternion.identity);

        if (bullet.TryGetComponent<Rigidbody>(out var rb))
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        if (shootSfx != null)
            playerAudio.PlayOneShot(shootSfx);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (isDisarmed) CureDisarm();

        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            jumpCount = 0;
            dirtParticle.Play();

            cam.transform.position = new Vector3(8, 4, -15);
        }

        else if (collision.gameObject.CompareTag("Platformer"))
        {
            isOnGround = true;
            jumpCount = 1;
            dirtParticle.Play();
        }

        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            //explosionParticle.Play();
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            playerAudio.PlayOneShot(crashSfx);

            if (!noDamage)
            {
                hp--;
            }
            Dead();
        }

        else if (collision.gameObject.CompareTag("Ghost"))
        {
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            playerAudio.PlayOneShot(crashSfx);
            playerAudio.PlayOneShot(ghotAudio);
            if (!noDamage)
            {
                hp--;
            }
            Dead();
        }

        else if (collision.gameObject.CompareTag("Boss"))
        {
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            playerAudio.PlayOneShot(crashSfx);
            playerAudio.PlayOneShot(bossAudio);

            if (!noDamage)
            {
                hp -= 2;
            }

            Boss bossScript = collision.gameObject.GetComponent<Boss>();
            if (bossScript != null)
            {
                bossScript.DieFromCollision();
            }

            Dead();
        }
    }

    public void ApplyDisarm(float duration)
    {
        isDisarmed = true;
        disarmTimer = duration;

        if (playerRenderer != null)
            playerRenderer.material.color = Color.green;
    }

    private void CureDisarm()
    {
        isDisarmed = false;
        disarmTimer = 0f;

        if (playerRenderer != null)
            playerRenderer.material.color = originalColor;
    }
    public void Dead()
    {
        if (hp <= 0)
        {
            Debug.Log("Game Over!");
            gameOver = true;
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            dirtParticle.Stop();

            GameManager.Instance.ShowGameOver();
        }
    }
}