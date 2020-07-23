using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Player Reference")]
    [SerializeField]
    private GameObject m_player;

    //Captures initial offset between player and camera (play scene camera desired distance from player)
    private Vector3 m_offset;

    //temp fix for offset issue in rotation
    private const float c_offset = 6;

    void Start()
    {
        m_offset = m_player.transform.position - transform.position;
    }

    void Update()
    {
        //Determine the resulting vector from player forward, and offset
        Vector3 resulting = new Vector3(m_player.transform.forward.x * c_offset, m_player.transform.forward.y + m_offset.y, m_player.transform.forward.z * c_offset);
        
        transform.position = m_player.transform.position - resulting;
        transform.LookAt(m_player.transform);
    }
}
