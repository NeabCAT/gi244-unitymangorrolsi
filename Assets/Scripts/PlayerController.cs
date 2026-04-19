using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //Exam04
    public int hp = 3;
    //Exam02
    public int jumpCount;
    public float jumpForce;
    public float gravityModifier;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;

    public AudioClip jumpSfx;
    public AudioClip crashSfx;

    private Rigidbody rb;
    private InputAction jumpAction;
    //Exam03
    private InputAction dashAction;
    private bool isOnGround = true;

    private Animator playerAnim;
    private AudioSource playerAudio;

    public bool gameOver = false;
    //Exam03
    public bool isDash = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Physics.gravity *= gravityModifier;

        jumpAction = InputSystem.actions.FindAction("Jump");
        //Exam03
        dashAction = InputSystem.actions.FindAction("Sprint");


        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Exam02
        if (jumpAction.triggered && jumpCount <2 && !gameOver)
        {
            rb.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
            //Exam02
            jumpCount++;
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSfx);
        }

        //Exam03
        if (dashAction.IsPressed())
        {
            isDash = true;
        }
        else
        {
            isDash = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            //Exam02
            jumpCount = 0;
            dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle")) //Exam04
        {

            //explosionParticle.Play();
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            Destroy(collision.gameObject);
            playerAudio.PlayOneShot(crashSfx);
            hp--;

            if (hp <= 0)
            {
                Debug.Log("Game Over!");
                gameOver = true;
                playerAnim.SetBool("Death_b", true);
                playerAnim.SetInteger("DeathType_int", 1);
                dirtParticle.Stop();
            }
        }
    }

}