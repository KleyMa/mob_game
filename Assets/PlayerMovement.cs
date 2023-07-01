using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    bool isDeath, isInmortal;
    float activeVelocity;
    Vector2 input;
    SpriteRenderer spriteRenderer;
    //ImageUIScript imageUIScript;
    [SerializeField] AudioClip damagedSfx;
    // [SerializeField] TextMeshProUGUI vidasText;
    [SerializeField] float tiempoDeInmortalidad = 1f;
    [SerializeField] float moveVelocity = 1f;
    [SerializeField] float dashVelocity = 5f;
    [SerializeField] float dashLength = .5f;
    [SerializeField] int health = 6;
    [SerializeField] float dashCooldown = 1f;

    float dashCounter, dashCoolCounter;
    Rigidbody2D thisRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        activeVelocity = moveVelocity;
        isDeath=false;
        thisRigidbody =  GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }
    private void MovePlayer()
    {
        if (!isDeath)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");
            input.Normalize();
            thisRigidbody.inertia = 0;
            thisRigidbody.velocity = activeVelocity * input;
            Debug.Log(thisRigidbody.velocity);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                thisRigidbody.velocity = (activeVelocity * input) / 2;
            }
            if (dashCoolCounter <= 0 && dashCounter <= 0)
            {
                spriteRenderer.color = Color.white;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (dashCoolCounter <= 0 && dashCounter <= 0)
                {
                    activeVelocity = dashVelocity;
                    dashCounter = dashLength;
                    spriteRenderer.color = new Color32(160, 160, 160, 255);
                }

            }

            if (dashCounter > 0)
            {
                dashCounter -= Time.deltaTime;
                isInmortal = true;
                if (dashCounter <= 0)
                {
                    activeVelocity = moveVelocity;
                    dashCoolCounter = dashCooldown;
                    isInmortal = false;

                }
            }
            if (dashCoolCounter > 0)
            {
                dashCoolCounter -= Time.deltaTime;
            }
        }
    }
}
