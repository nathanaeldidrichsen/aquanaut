using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Shooting : MonoBehaviour
{
    private Vector3 mousePosition;
    private Camera mainCam;
    public GameObject projectile;
    public Transform projectileTransform;
    public bool canFire;
    private float timer;
    public float timeBetweenFiring;


void Start()
{
    mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
}
    
    // Update is called once per frame
    void Update()
    {
        mousePosition = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePosition - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }
}
