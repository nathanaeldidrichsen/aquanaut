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
        public GameObject harpoonPrefab; // Prefab of the harpoon GameObject
    public float harpoonSpeed = 10f; // Speed of the harpoon
    [SerializeField] GameObject harpoonShootPoint;



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


        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            // harpoonShootPoint.SetActive(true);
            ShootHarpoon();
        }
    }

    private void ShootHarpoon()
{

    // Instantiate harpoon at the fire point's position and rotation
    GameObject harpoon = Instantiate(harpoonPrefab, harpoonShootPoint.transform.position, Quaternion.identity);


}
}
