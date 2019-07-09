using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Vector3 velocity;
    float speed;
    int frame;
    public int distance;
    int hp;
    public int max_hp;
    public double timeStop;
    public GameObject hpBar;
    public float speedTemplet;
    public float moveD;
    public bool faceLeft = true;

    animatorController animatorControllerObj;
    // Start is called before the first frame update
    void Start()
    {
        frame = 0;
        hp = max_hp;
        speed = speedTemplet;
    }

    // Update is called once per frame
    void Update()
    {
        velocity = new Vector3(speed, 0, 0);
        if (frame == 0)
        {
            flip();
        }
        if (frame < distance)
        {
            transform.position += velocity;
            frame++;
        }

        if (frame >= distance && frame <= 2*distance)
        {
            if (frame == distance)
            {
                flip();
            }
            frame++;
            transform.position -= velocity;
        }
        if (frame >= 2*distance)
        { 
            frame = 0;
        }
            
        if (hp == 0)
        {
            Destroy(this.gameObject);
        }
        if (timeStop >= 0)
        {
            speed = 0;
            timeStop -= Time.deltaTime;
        }
        if (timeStop < 0)
        {
            speed = speedTemplet;
        }
        float hpPer = ((float) hp / (float) max_hp);
        hpBar.transform.localScale = new Vector3(hpPer, hpBar.transform.localScale.y, hpBar.transform.localScale.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "sword")
        {
            GameObject obj = GameObject.Find("bodyWithSword");
            animatorControllerObj = obj.GetComponent <animatorController>();
            if (animatorControllerObj.isAttack)
            {
                timeStop = 0.2;
                hp--;
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
}
