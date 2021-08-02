using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileController : MonoBehaviour
{
    public static ProfileController Instance { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    void UpdatePlayerInfo()
    {
        
    }
}
