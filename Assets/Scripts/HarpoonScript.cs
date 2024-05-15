using UnityEngine;

public class HarpoonScript : MonoBehaviour
{
    private Vector3 mousePos;
    private Rigidbody2D rb;
    private int damage = 1;
    public float force;
    public float timeBeforeDestroy = 1f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dir = mousePos - transform.position;




        Vector3 rotation = transform.position - mousePos;
        rb.velocity = new Vector2(dir.x, dir.y).normalized * force;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 180);
    }

    void OnCollisionEnter2D(Collision2D other)
    {

        if(other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Health>().GetHurt(damage, transform.position);
            other.gameObject.GetComponent<Creature>().wasAttacked = true;
        }

        Destroy(this.gameObject, timeBeforeDestroy);

    }

//THIS SCRIPT WILL CHANGE VELOCITY BASED ON HOW FAR FROM THE PLAYER U CLICK

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class HarpoonScript : MonoBehaviour
// {
//     private Vector3 mousePos;
//     private Rigidbody2D rb;
//     private int damage = 1;
//     public float force;
//     public float timeBeforeDestroy = 1f;
//     private Vector2 moveDir;

//     private float initialAngle;
//     // Start is called before the first frame update
//     void Start()
//     {
//         rb = GetComponent<Rigidbody2D>();
//         Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//         Vector3 dir = mousePos - transform.position;
//         rb.velocity = dir.normalized * force;

//         // Initial rotation
//         float initialAngle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
//         transform.rotation = Quaternion.Euler(0, 0, initialAngle);
//     }

//     void Update()
//     {
//         // moveDir = rb.velocity;
//         // Quaternion toRotation = Quaternion.LookRotation(Vector3.up, moveDir.normalized);
//         // transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 4000 * Time.deltaTime);
//     }

//     void OnCollisionEnter2D(Collision2D other)
//     {

//         if (other.gameObject.CompareTag("Enemy"))
//         {
//             other.gameObject.GetComponent<Health>().GetHurt(damage);
//         }

//         Destroy(this.gameObject, timeBeforeDestroy);

//     }
// }

}
