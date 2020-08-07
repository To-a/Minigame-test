using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStatusManager : MonoBehaviour
{
    public GameObject player;
    public CharacterStatusManager m_CharStats;
    public ParticleSystem particle;
    public Rigidbody m_Rb;
    public bool inInventory = false;
    public bool inHand = false;

    public float distanceToPlayer;
    void Start()
    {
        m_CharStats = player.GetComponent<CharacterStatusManager>();
        particle = GetComponent<ParticleSystem>();
        m_Rb = GetComponent<Rigidbody>();
    }

 
    void Update()
    {
        var main = particle.main;
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer < m_CharStats.pickupRange){
            main.startColor = new ParticleSystem.MinMaxGradient(Color.green);
        } else {
            main.startColor = new ParticleSystem.MinMaxGradient(Color.magenta);
        }
    }

}
