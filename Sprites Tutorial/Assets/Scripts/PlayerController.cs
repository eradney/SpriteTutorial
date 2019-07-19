using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Animator anim;
    private int count;
    private int lives;
    private bool facingRight = true;

    public float speed;
    public float jumpForce;
    public Text winText;
    public Text countText;
    public Text scoreText;
    public Text loseText;
    public AudioClip Background;
    public AudioClip WinMusic;
    public AudioSource MusicSource;


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        count = 0;
        lives = 3;
        winText.text = "";
        SetCountText();
        MusicSource.clip = Background;
        MusicSource.Play();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(moveHorizontal, 0);
        rb2d.AddForce(movement * speed);

        if(facingRight == false && moveHorizontal > 0)
        {
            Flip();
        }
        else if(facingRight == true && moveHorizontal < 0)
        {
            Flip();
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            anim.SetInteger("State", 1);
        }
        else
        {
            anim.SetInteger("State", 0);
        }

        if (Input.GetKey("escape"))
            Application.Quit();
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            if(Input.GetKey(KeyCode.UpArrow))
            {
                rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                anim.SetInteger("State", 2);
            }

        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            lives = lives - 1;
            SetScoreText();
        }
        if (count == 4)
        {
            transform.position = new Vector2(-31.0f, -24.5f);
            lives = 3;
        }
        if (lives == 0)
        {
            gameObject.SetActive(false);
            loseText.text = "You Lose :(";
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if (count >= 8 && MusicSource.isPlaying)
        {
            winText.text = "You win!";
            MusicSource.Stop();
            MusicSource.clip = WinMusic;
            MusicSource.Play();
        }
    }

    void SetScoreText()
    {
        scoreText.text = "Lives: " + lives.ToString();
    }
}
