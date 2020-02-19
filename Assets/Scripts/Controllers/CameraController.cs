using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private CinemachineComposer composer;

    [SerializeField]
    private float sensitivity = 1f;

    [Range(1,8)]
    public float yMin = 3f;
    [Range(1,8)]
    public float yMax = 5f;
    [Range(-3,5)]
    public float xMin = -1f;
    [Range(-3,5)]
    public float xMax = 3f;
    

    private void Start()
    {
        composer = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineComposer>();
    }

    private void Update()
    {
        float vertical = Input.GetAxis("Mouse Y") * sensitivity;
        float horizontal = Input.GetAxis("Mouse X") * sensitivity;
        composer.m_TrackedObjectOffset.y += vertical;
        composer.m_TrackedObjectOffset.y = Mathf.Clamp(composer.m_TrackedObjectOffset.y, yMin, yMax);
        composer.m_TrackedObjectOffset.x += horizontal;
        composer.m_TrackedObjectOffset.x = Mathf.Clamp(composer.m_TrackedObjectOffset.x, xMin, xMax);
    }
}
