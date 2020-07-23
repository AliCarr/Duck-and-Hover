using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HoverCraft : MonoBehaviour
{
    [Header("Required Projection Points")]
    [SerializeField]
    private Transform[] m_transformPoints = new Transform[4];
    
    [Header("Controls")]
    [SerializeField]
    private float m_forwardSpeed = 1200;

    [SerializeField]
    private float m_strafeSpeed;

    [SerializeField]
    private float m_rotationSpeed;

    [Header("Hover Attributes")]
    [SerializeField]
    private float m_hoverHeight = 0.2f;

    //Internal Variables
    private Vector3[] hitPoints = new Vector3[4];
    private Vector3 forces = Vector3.zero;
    private Vector3 diff = Vector3.zero;
    private Rigidbody myRigidBody;


    private void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        CastRays();
        SlopeCheck();
        HandleInputs();
        CalculateUpThrust();
    }

    void HandleInputs()
    {
        if (Input.GetKey(KeyCode.W))
            myRigidBody.AddForce(transform.forward * m_forwardSpeed);

        if (Input.GetKey(KeyCode.A))
            myRigidBody.AddTorque(Vector3.up * -m_rotationSpeed);

        if (Input.GetKey(KeyCode.D))
            myRigidBody.AddTorque(Vector3.up * m_rotationSpeed);

        if(Input.GetKey(KeyCode.S))
            myRigidBody.AddForce(transform.forward * -m_forwardSpeed);

        if(Input.GetKey(KeyCode.Q))
            myRigidBody.AddForce(-transform.right * m_strafeSpeed);

        if (Input.GetKey(KeyCode.E))
            myRigidBody.AddForce(transform.right * m_strafeSpeed);
    }

    void CastRays()
    {
        RaycastHit hit;
        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        for (int c = 0; c < 4; ++c)
            if (Physics.Raycast(m_transformPoints[c].position, new Vector3(0, -1, 0), out hit, Mathf.Infinity, layerMask))
                hitPoints[c] = new Vector3(m_transformPoints[c].position.x, m_transformPoints[c].position.y - hit.distance, m_transformPoints[c].position.z);

    }

    void CalculateUpThrust()
    {
        for (int c = 0; c < 4; c++)
        {
            diff = (m_transformPoints[c].position - hitPoints[c]) * 1.3f;
            forces = new Vector3(0, (Mathf.Pow(0.78f, ((diff.y * 2) - 1.7f)) + m_hoverHeight) * 600, 0);
            myRigidBody.AddForceAtPosition(forces, m_transformPoints[c].position);
        }
    }

    void SlopeCheck()
    {
        //get the current angle relative to the X axis. If it is greater than a value, add a small amount of force in that direction
        float myAngleX = WrapAngle(transform.eulerAngles.x);
        float myAngleZ = WrapAngle(transform.eulerAngles.z);

        if (myAngleX >= 20 || myAngleX <= -20)
            myRigidBody.AddForce(transform.forward * 15 * myAngleX);

        if (myAngleZ >= 20 || myAngleZ <= -20)
            myRigidBody.AddForce(-transform.right * 15 * myAngleZ);
    }

    private float WrapAngle(float angle)
    {
        angle %= 360;

        if (angle > 180)
            return angle - 360;

        return angle;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int c = 0; c < 4; c++)
        {
            Gizmos.DrawSphere(m_transformPoints[c].position, 0.3f);
            Gizmos.DrawSphere(hitPoints[c], 0.3f);
            Debug.DrawRay(m_transformPoints[c].position, Vector3.up * (hitPoints[c].y - m_transformPoints[c].position.y), Color.red);
        }
    }
}