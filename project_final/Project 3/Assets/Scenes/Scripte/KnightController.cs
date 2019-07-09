using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KnightController : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    public Text hpOutput;
    public static int hp;
    public int hpMax;
    public float moveD;
    public bool faceLeft = true;

    //jump
    public bool onGround;
    public Transform checkG;
    public float checkR;
    public LayerMask groundSensor;
    public int doubleJump;
    public Vector3 velocity;

    //item
    public GameObject Helmet;
    public GameObject Sword;
    public GameObject Sheild;

    //skin
    public GameObject body;
    public GameObject withHelmet;
    public GameObject withSheild;
    public GameObject withSword;

    //health
    public GameObject mushroom;

    public GameObject heart;

    //game over
    bool gameOver;

    public int offset;

    public float abilityTime;
    public float abilityTimeMax;
    public float trapTime;
    public float trapTimeMax;
    public bool isGifted;
    int speedCount = 0;
    public bool isTraped;
    int trapCount = 0;
    int mapCount = 0;
    public bool hasMap;
    int healCount = 0;
    public Text alert;

    BossController bossController;
    //bar
    public GameObject coolDBar;
    public GameObject trapCD;

    Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        rb = GetComponent<Rigidbody2D>();
        velocity = new Vector3(0, 0, 0);
        hp = hpMax;
        SetHpOutputText();
        gameOver = false;
        coolDBar.SetActive(false);
        trapCD.SetActive(false);
        alert.text = "Welcome to the Jungle! First, you need to follow the arrows to get equipped!";
    }

    // Update is called once per frame
    void Update()
    {
        if (onGround)
        {
            doubleJump = 2;
        }
        jump();

        if (Input.GetKey(KeyCode.E))
        {
            print("attack");
        }
        SetHpOutputText();

        if (hp <= 0)
        {
            gameOver = true;
            this.gameObject.SetActive(false);
            Application.LoadLevel("Lose");
            hpOutput.text = "";
            camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
            camera.enabled = false;
        }
       

    }

    private void FixedUpdate()
    {
        if (!gameOver)
        {
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                velocity = new Vector3(1, 0, 0);
                transform.position += velocity * speed;
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                velocity = new Vector3(-1, 0, 0);
                transform.position += velocity * speed;
            }
            jump();
            moveD = Input.GetAxis("Horizontal");

            if (!faceLeft && moveD < 0)
            {
                flip();
            }
            else if (faceLeft && moveD > 0)
            {
                flip();
            }
            onGround = Physics2D.OverlapCircle(checkG.position, checkR, groundSensor);
        }

        if (isGifted)
        {
            abilityTime -= Time.deltaTime;
            float cdPer = (abilityTime / abilityTimeMax);
            coolDBar.transform.localScale = new Vector3(cdPer, trapCD.transform.localScale.y, trapCD.transform.localScale.z);
            if (abilityTime < 0)
            {
                speed /= 2;
                isGifted = false;
                if (speedCount == 1)
                {
                    alert.text = "";
                }
            }
        }

        if (isTraped)
        {
            trapTime -= Time.deltaTime;
            float tcdPer = (trapTime / trapTimeMax);
            trapCD.transform.localScale = new Vector3(tcdPer, coolDBar.transform.localScale.y, coolDBar.transform.localScale.z);
            if (trapTime < 0)
            {
                speed *= 4;
                isTraped = false;
                if (trapCount == 1)
                {
                    alert.text = "";
                }
            }
        }

        trapCD.SetActive(isTraped);
        coolDBar.SetActive(isGifted);
    }

    void jump()
    {
        if ( Input.GetKeyDown(KeyCode.Space) && doubleJump > 0) 
        {
            rb.AddForce(new Vector2(0, 4), ForceMode2D.Impulse);
            doubleJump--;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            rb.AddForce(new Vector2(-5, 0), ForceMode2D.Impulse);
            hp--;
            SetHpOutputText();
        }

        if (collision.gameObject.tag == "boss")
        {
            GameObject obj = GameObject.FindWithTag("enemy");
            bossController = obj.GetComponent<BossController>();
            if (bossController.isAttack)
            {
                rb.AddForce(new Vector2(-5, 0), ForceMode2D.Impulse);
                hp--;
                SetHpOutputText();
            }
        }

        if (collision.gameObject.tag == "heal")
        {
            if (hp < hpMax)
            {
                hp++;
            }
            SetHpOutputText();
            Destroy(collision.gameObject);
            healCount++;
            if (healCount == 1)
            {
                alert.text = "Your HP has incrased by one!";
            }
        }

        if (collision.gameObject.tag == "maxhp")
        {
            hp += hpMax/2;
            Destroy(collision.gameObject);
        }

            if (collision.gameObject == Helmet)
        {
            Destroy(Helmet);
            body.gameObject.SetActive(false);
            withHelmet.gameObject.SetActive(true);
            hpMax++;
            hp++;
            alert.text = "You are stronger now! Your HP limit has increased!";
        }

        if (collision.gameObject == Sheild)
        {
            Destroy(Sheild);
            withHelmet.gameObject.SetActive(false);
            withSheild.gameObject.SetActive(true);
            hpMax++;
            hp++;
            alert.text = "It's time to get the weapon!";
        }

        if (collision.gameObject == Sword)
        {
            Destroy(Sword);
            withSheild.gameObject.SetActive(false);
            withSword.gameObject.SetActive(true);
            alert.text = "Now you are able to use the mouse left button to attack!";
        }

        if (collision.gameObject.tag == "door")
        {
            if (!hasMap)
            {
                alert.text = "You need to get the map first!";
            }
            if (hasMap)
            {
                alert.text = "";
            }
        }

        if (collision.gameObject.tag == "Speed")
        {
            abilityTimeMax = 10;
            abilityTime = abilityTimeMax;
            speed *= 2;
            Destroy(collision.gameObject);
            isGifted = true;
            speedCount++;
            if (speedCount == 1)
            {
                alert.text = "Speed Up! It's only for 10s!";
            }
        }

        if (collision.gameObject.tag == "Finish")
        {
            heart.SetActive(true);
        }

        if (collision.gameObject.tag == "trap")
        {
            trapTimeMax = 5;
            trapTime = trapTimeMax;
            speed /= 4;
            Destroy(collision.gameObject);
            isTraped = true;
            trapCount++;
            if (trapCount == 1)
            {
                alert.text = "Oops! You got trapped. Speed down for 5s!";
            }
        }

        if (collision.gameObject.tag == "map")
        {
            Destroy(collision.gameObject);
            hasMap = true;
            mapCount++;
            if (mapCount == 1)
            {
                alert.text = "Now, it's time to start your adventure!";
            }
        }
    }

    void flip()
    {
        faceLeft = !faceLeft;
        Vector2 dir = transform.localScale;
        dir.x *= -1;
        transform.localScale = dir;
    }
    public void SetHpOutputText()
    {
        hpOutput.text = "HP: " + hp.ToString();
    }
}
