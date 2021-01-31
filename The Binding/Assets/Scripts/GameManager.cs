﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement.ToggleMovement(true);
    }
}
