using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;



public class Shooting : MonoBehaviour
{
    private Vector3 mousePosition;
    LineRenderer line;
    public float moveToGrabbedPosSpeed = 10;
    public float grappleShootSpeed = 100f;
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
    public SpriteRenderer hookSprite;



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


        if (Input.GetMouseButton(0)) // Left mouse button
        {
            harpoonShootPoint.GetComponent<SpriteRenderer>().enabled = true;
            // harpoonShootPoint.SetActive(true);
            //StartGrapple();
        }
        if(Input.GetMouseButtonUp(0))
        {
            harpoonShootPoint.GetComponent<SpriteRenderer>().enabled = false;
            Shoot();

        }


        if (Input.GetMouseButton(1) && !isGrappling) // Left mouse button
        {
            hookSprite.enabled = true;
        }


        if (Input.GetMouseButtonUp(1) && !isGrappling) // Left mouse button
        {
            // harpoonShootPoint.SetActive(true);
            //Shoot();
            hookSprite.enabled = false;
            StartGrapple();
        }

        if (retracting)
        {
            Vector2 moveDirection = (target - (Vector2)Player.Instance.transform.position).normalized;
            Player.Instance.rb.MovePosition(target);
// = moveDirection * moveToGrabbedPosSpeed;
            line.SetPosition(0, transform.position);

            if (Vector2.Distance(Player.Instance.transform.position, target) < 0.05f)
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
        Vector3 instPos = new Vector3(harpoonShootPoint.transform.position.x, harpoonShootPoint.transform.position.y);



        // Instantiate harpoon at the fire point's position and rotation
        GameObject harpoon = Instantiate(harpoonPrefab, instPos, Quaternion.identity);
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
