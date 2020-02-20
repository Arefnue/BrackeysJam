using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHolder : MonoBehaviour
{
    private void OnEnable()
    {
        GameMaster.instance.levelHolder = gameObject.transform;
    }

}
