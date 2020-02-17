using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CmConfig : MonoBehaviour
{
    private PlayerController targetPlayer;
    private CinemachineVirtualCamera cmCam;

    private void Start()
    {
        cmCam = GetComponent<CinemachineVirtualCamera>();
        targetPlayer = FindObjectOfType<PlayerController>();

        cmCam.Follow = targetPlayer.transform;
        cmCam.LookAt = targetPlayer.transform;
    }
}
