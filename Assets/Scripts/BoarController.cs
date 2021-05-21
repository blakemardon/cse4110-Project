using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BoarState{
    Aiming,
    Charging,
    Waiting,
    Hit
}

public class BoarController : MonoBehaviour
{
    public float chargeSpeed;
    public float chargeTime;
    public float hitTime;
    public GameObject player;

    public BoarState state;
    private float time;
    private float timer;
    private int health;
    private Rigidbody2D rigidbody;
    private SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        state = BoarState.Waiting;
        time = 0;
        timer = 0.5f;
        health = 6;
        TextDisplayController.DisplayText("Look a boar!!!");
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (state == BoarState.Aiming){
            if(time < timer){
                float angle = AngleBetweenTwoPoints(transform.position, player.transform.position);
                transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
            }
            else{
                time = 0;
                timer = chargeTime;
                state = BoarState.Charging;
            }
            
        }
        else if (state == BoarState.Charging){
            if(time < timer){
                float x = -1 * Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.Deg2Rad) * chargeSpeed;
                float y = -1 * Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.Deg2Rad) * chargeSpeed;
                rigidbody.AddForce(new Vector2(x, y));
            }
            else{
                state = BoarState.Waiting;
                time = 0;
                timer = 1f;
            }
        }
        else if (state == BoarState.Hit){
            if(time < timer){
                sprite.color = Color.red;
            }
            else{
                sprite.color = Color.white;
                time = 0;
                timer = 0.5f;
                state = BoarState.Waiting;
            }

        }
        else if (state == BoarState.Waiting){
            if(time >= timer){
                time = 0;
                timer = 2;
                state = BoarState.Aiming;
            }
        }
    }

    void OnTriggerEnter2D (Collider2D collider){
        if(collider.tag == "Weapon" && state != BoarState.Charging && state != BoarState.Aiming){
            health--;
            if (health == 0){
                Destroy(gameObject);
            }
            state = BoarState.Hit;
            time = 0;
            timer = hitTime;
        }
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
    
}
