﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gimmick : MonoBehaviour
{
    // Gimmickの状態:true=解かれている,false=解かれていない。
    protected bool _active;
    
    // Start is called before the first frame update
    void Start()
    {
        _active = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
