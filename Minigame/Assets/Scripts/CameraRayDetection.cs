using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRayDetection : MonoBehaviour
{
    //TODO add highlighted item label
    public Camera playerCamera;
    RaycastHit hit;
    Ray ray;
    ParticleSystem particle;
    Transform objectHit, prevObjectHit;
    CharacterStatusManager m_CharStats;
    void Start()
    {
        prevObjectHit = this.transform;
        m_CharStats = GetComponent<CharacterStatusManager>();
    }

    void Update()
    {
        ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit)) {
            objectHit = hit.transform;
            // Stop emitting particles when mouse leaves
            if (prevObjectHit.tag == "Item" && prevObjectHit != objectHit){
                particle = prevObjectHit.GetComponent<ParticleSystem>();
                particle.Stop();
            }
            // Emit particles if object hovered
            if (objectHit.tag == "Item" && prevObjectHit != objectHit){
                particle = objectHit.GetComponent<ParticleSystem>();
                particle.Play();
                
            }
            m_CharStats.raycastTarget = objectHit;
            prevObjectHit = objectHit;
        }
    }
}
