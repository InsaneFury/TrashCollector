using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    protected Camera cam;
    private bool moving;
    private Vector3 movingTo;
    protected Rigidbody rb;

    public int groundLayer = 0;
    public float speed = 10;
    public float rotationSpeed = 5;
    public float distanceToStop = 1;


    private Animator animator;
    private bool attack;

    public GameObject rightWheel;
    public GameObject leftWheel;

    void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        GameObject goHit;

        if (Input.GetMouseButton(1))
        {
            if (Physics.Raycast(ray, out hit, 50))
            {
                goHit = hit.transform.gameObject;
                if (goHit)
                {
                    if (goHit.layer == groundLayer)
                        SetDestination(hit.point);
                }
            }
        }

        if (moving)
        {
            Move();
        }

        if (Vector3.Distance(transform.position, movingTo) < distanceToStop)
        {
            Stop();
        }
    }

    private void SetDestination(Vector3 _movingTo)
    {
        movingTo = _movingTo;
        moving = true;
    }

    private void Move()
    {
        Vector3 wantedVelocity = transform.forward.normalized * speed * Time.deltaTime;

        RaycastHit hitInfo;
        Vector3 m_GroundNormal;
        if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, 5))
        {
            m_GroundNormal = hitInfo.normal;
            wantedVelocity = Vector3.ProjectOnPlane(wantedVelocity, m_GroundNormal);
            
            Vector3 des = new Vector3(movingTo.x, transform.position.y, movingTo.z);

            Quaternion destiny = Quaternion.identity;
            destiny.SetLookRotation(des - transform.position, m_GroundNormal);

            Quaternion carRotation = Quaternion.Lerp(transform.rotation, destiny, rotationSpeed * Time.deltaTime);
            transform.rotation = carRotation;

        }
        rb.velocity = wantedVelocity;
    }

    private void Stop()
    {
        moving = false;
        rb.velocity = Vector3.zero;
    }
}
