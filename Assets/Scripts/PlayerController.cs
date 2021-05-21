using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public double speed;
    public double armor;
    public double rollSpeed;
    public double rollTime;
    public double rollCooldown;
    public double attackTime;
    public double attackCooldown;
    public int damage;
    
    public GameObject healthDisplay;
    public GameObject weapon;

    private enum PlayerState{
        Walking,
        Rolling,
        Attacking
    }

    PlayerState state;
    Rigidbody2D rigidBody;
    double stateTimer;
    Vector3 rollDirection;
    double lastRoll;
    double lastAttack;
    int health;
    Vector3 movement;

    HealthDisplayController healthDisplayController;
    SpriteRenderer weaponSprite;
    PolygonCollider2D weaponCollider;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        state = PlayerState.Walking;
        stateTimer = 0;
        lastAttack = 0;
        lastRoll = 0;
        health = 6;

        healthDisplayController = healthDisplay.GetComponent<HealthDisplayController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Debug Next")){
            TextDisplayController.DisplayText("test");
        }

        // Update timers
        float deltaTime = Time.deltaTime;
        lastRoll += deltaTime;
        lastAttack += deltaTime;
        stateTimer += deltaTime;

        // Get input direction normalized
        movement = Vector3.zero;
        movement.x += Input.GetAxisRaw("Horizontal");
        movement.y += Input.GetAxisRaw("Vertical");
        movement.Normalize();

        
        switch (state)
        {
            case PlayerState.Walking:
                Walking();
                break;
            case PlayerState.Rolling:
                Rolling();
                break;
            case PlayerState.Attacking:
                Attacking();
                break;
        }
    }

    void Walking(){
        Vector3 moveDirection = movement * (float)Time.deltaTime * (float)speed;
        rigidBody.AddForce(moveDirection);
        // Change sprite rotation
        if (moveDirection != Vector3.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg + 90;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        
        // Check if state should be changed to rolling
        if (moveDirection != Vector3.zero)
        {
            if (Input.GetButtonDown("Roll") && lastRoll >= rollCooldown)
            {
                state = PlayerState.Rolling;
                rollDirection = movement;
                stateTimer = 0;
            }
        }
        // Check if state should be changed to attacking
        if (Input.GetButtonDown("Attack") && lastAttack >= attackCooldown)
        {

            state = PlayerState.Attacking;
            stateTimer = 0;
        }
    }

    void Rolling(){
        stateTimer += Time.deltaTime;
        if (stateTimer >= rollTime)
        {
            state = PlayerState.Walking;
            lastRoll = 0;
            stateTimer = 0;
        }
        rigidBody.AddForce(rollDirection * Time.deltaTime * (float)rollSpeed);
    }


    void Attacking(){
        if(!weapon.activeSelf){
            weapon.SetActive(true);
        }
        
        if (attackTime < stateTimer)
        {
            state = PlayerState.Walking;
            weapon.SetActive(false);
            lastAttack = 0;
            stateTimer = 0;
        }
    }

    void OnCollisionEnter2D(Collider2D collider){
        Debug.Log("hit");
        if(collider.gameObject.tag == "Enemy"){
            health -= 2;
            healthDisplayController.UpdateHealth(health);
        }
    }
}
