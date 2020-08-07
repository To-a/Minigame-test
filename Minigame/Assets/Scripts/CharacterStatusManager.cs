using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatusManager : MonoBehaviour
{
    //movement parameters
    public float move_speed = 0.2f;
    public float strafe_penalty = 0.8f; // strafe speed reduction
    public float backpedal_penalty = 0.6f; // backpedal speed reduction
    public float sprint_speed_multiplier = 4.0f;
    public float jump_strength = 300f;
    //character states
    public bool isSprinting;
    public bool isIdle;
    public Transform raycastTarget;

    //other
    public float pickupRange = 3.0f;
    void Start()
    {
        raycastTarget = transform; //work around for null exception - fix this
    }

    void Update()
    {
        
    }
}
