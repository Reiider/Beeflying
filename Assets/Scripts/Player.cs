using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    float flySpeed;
    Rigidbody2D rb;
    private int speedCoins = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        flySpeed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;
        position.y += flySpeed;
        transform.position = Vector3.Lerp(transform.position, position, flySpeed);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * (speed + speedCoins * 0.2f), 
                                  Input.GetAxis("Vertical") * (speed + speedCoins * 0.2f));
    }

    public void IncreaseSpeed(float inc)
    {
        flySpeed = inc; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "SpeedCoin")
        {
            speedCoins++;
            collision.gameObject.SetActive(false);
        }
    }
}
