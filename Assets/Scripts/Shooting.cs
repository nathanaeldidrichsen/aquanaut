using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Shooting : MonoBehaviour
{
    private Vector3 mousePosition;
    LineRenderer line;
    public float moveToGrabbedPosSpeed;
    public float grappleShootSpeed = 20f;
    bool isGrappling = false;
    public bool retracting = false;
    Vector2 target;

    [SerializeField] LayerMask grappableMask;
    private float maxDistance = 10f;
    private Camera mainCam;
    public GameObject projectile;
    public Transform firePointTransform;
    public bool canFire;
    private float timer;
    public float timeBetweenFiring;
    public GameObject harpoonPrefab; // Prefab of the harpoon GameObject
    public float harpoonSpeed = 10f; // Speed of the harpoon
    [SerializeField] GameObject harpoonShootPoint;



    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        line = GetComponent<LineRenderer>();
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
            Shoot();
            //StartGrapple();
        }

                if (Input.GetMouseButtonDown(1) && !isGrappling) // Left mouse button
        {
            // harpoonShootPoint.SetActive(true);
            //Shoot();
            StartGrapple();
        }

        if (retracting)
        {
            Vector2 grapplePos = Vector2.Lerp(Player.Instance.transform.position, target, moveToGrabbedPosSpeed * Time.deltaTime);
            Player.Instance.transform.position = grapplePos;
            line.SetPosition(0, transform.position);

            if (Vector2.Distance(Player.Instance.transform.position, grapplePos) < 0.05f)
            {
                isGrappling = false;
                retracting = false;
                line.enabled = false;
                line.positionCount = 2;
            }
        }
    }

    private void Shoot()
    {

        // Instantiate harpoon at the fire point's position and rotation
        GameObject harpoon = Instantiate(harpoonPrefab, harpoonShootPoint.transform.position, Quaternion.identity);
    }

    private void StartGrapple()
    {
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        RaycastHit2D hit = Physics2D.Raycast(firePointTransform.position, dir, maxDistance, grappableMask);

        if (hit.collider != null)
        {
            isGrappling = true;
            target = hit.point;
            line.enabled = true;
            line.positionCount = 2;

            StartCoroutine(Grapple());
        }

    }

    IEnumerator Grapple()
    {
        float t = 0;
        float time = 10;
        line.SetPosition(0, firePointTransform.position);
        line.SetPosition(1, firePointTransform.position);
        Vector2 newPos;

        for (; t < time; t += grappleShootSpeed * Time.deltaTime)
        {
            newPos = Vector2.Lerp(firePointTransform.position, target, t / time);
            line.SetPosition(0, firePointTransform.position);
            line.SetPosition(1, newPos);
            yield return null;
        }

        line.SetPosition(1, target);
        retracting = true;

    }
}
