using System.Collections;
using UnityEngine;
using UnityEngine.UI;



public class Shooting : MonoBehaviour
{
    private Vector3 mousePosition;
    private LineRenderer line;
    public float moveToGrabbedPosSpeed = 10;
    public float grappleShootSpeed = 100f;
    bool isGrappling = false;
    public bool retracting = false;
    private Vector2 target;

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
    public bool capturing;
    private Creature hookedCreature;


    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        line = GetComponent<LineRenderer>();
    }

    void Update()
    {
        mousePosition = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePosition - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        if (Input.GetMouseButton(0)) // Left mouse button
        {
            harpoonShootPoint.GetComponent<SpriteRenderer>().enabled = true;
        }
        if (Input.GetMouseButtonUp(0))
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
            hookSprite.enabled = false;
            StartGrapple();
        }

        if (retracting && !capturing)
        {
            MoveTowardsTarget();
        }

        if (capturing)
        {
            KeepTargetHooked();
        }
    }

    private void Shoot()
    {
        Vector3 instPos = new Vector3(harpoonShootPoint.transform.position.x, harpoonShootPoint.transform.position.y);
        GameObject harpoon = Instantiate(harpoonPrefab, instPos, Quaternion.identity);
    }


    public void UnhookCreature()
    {
        capturing = false;
        hookedCreature = null;
        isGrappling = false;
        retracting = false;
        line.enabled = false;
        line.positionCount = 2;
    }
    public void KeepTargetHooked()
    {
        if (hookedCreature != null)
        {
            // Vector2 currentPosition = Player.Instance.transform.position;
            // Vector2 direction = (target - currentPosition).normalized;
            // Vector2 newPosition = currentPosition + direction * moveToGrabbedPosSpeed * Time.deltaTime;

            line.SetPosition(0, Player.Instance.transform.position);
            line.SetPosition(1, hookedCreature.transform.position);
        }
    }
    private void MoveTowardsTarget()
    {
        Vector2 currentPosition = Player.Instance.transform.position;
        Vector2 direction = (target - currentPosition).normalized;
        Vector2 newPosition = currentPosition + direction * moveToGrabbedPosSpeed;

        if (Vector2.Distance(newPosition, target) < 0.2f)
        {
            //Player.Instance.transform.position = target;
            isGrappling = false;
            retracting = false;
            line.enabled = false;
            line.positionCount = 2;
        }
        else
        {
            Player.Instance.rb.MovePosition(newPosition);
            line.SetPosition(0, transform.position);
            line.SetPosition(1, newPosition);
        }
    }

    private void StartGrapple()
    {
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(firePointTransform.position, dir, maxDistance, grappableMask);


        if (hit.collider != null)
        {

            if (hit.collider.CompareTag("Enemy"))
            {
                Debug.Log("Grabbed creature");
                if (hit.collider.GetComponent<Creature>().isCapturable)
                {
                    hookedCreature = hit.collider.GetComponent<Creature>();
                    hookedCreature.StartCapture();
                    hookedCreature.isHooked = true;
                    capturing = true;
                }
            }

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
        float time = 10; // How fast the line attaches to the grab point
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
