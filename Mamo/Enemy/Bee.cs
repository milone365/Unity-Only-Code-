﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : EnemyAI
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        base.Move();
    }
    private void OnDisable()
    {
        Destroy(gameObject);
    }
}
