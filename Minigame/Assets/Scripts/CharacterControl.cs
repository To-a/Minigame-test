using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    //TODO limit number of jumps
    public Camera playerCamera;
    public GameObject gameManager;
    public GameObject inventoryMenu;

    private Vector2 mouseDelta;
    private float x, y;
    private float v_in, h_in, total_in, vertical, horizontal;

    CharacterStatusManager m_CharStats;
    Rigidbody m_Rb;
    ItemStatusManager m_ItemStatus;
    Transform rayCastTarget;

    OptionsManager m_Options;
    CharacterInventory m_inventory;
    


    void Start()
    {
        m_CharStats = GetComponent<CharacterStatusManager>();
        m_Rb = GetComponent<Rigidbody>();

        m_Options = gameManager.GetComponent<OptionsManager>();
        m_inventory = inventoryMenu.GetComponent<CharacterInventory>();

        rayCastTarget = transform; //prevent null reference - fix this
        Cursor.lockState = CursorLockMode.Locked;

    }
    void Update()
    {
        //Camera control and player horizontal rotation
        if (Cursor.lockState != CursorLockMode.None){
            mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            x -= mouseDelta.x * m_Options.sensitivity;
            y -= mouseDelta.y * m_Options.sensitivity;
            y = Mathf.Clamp(y, -90.0f, 90.0f);
            transform.rotation = Quaternion.Euler(0.0f, -x, 0.0f);
            playerCamera.transform.rotation = Quaternion.Euler(y, -x, 0.0f);
        }
        

        //Check for wasd input
        v_in = Input.GetAxisRaw("Vertical");
        h_in = Input.GetAxisRaw("Horizontal");
        if (v_in == 0 && h_in == 0){
            m_CharStats.isIdle = true;
        } else {
            m_CharStats.isIdle = false;
        }

        //Player movement
        if (m_CharStats.isIdle == false){
            total_in = Mathf.Abs(v_in) + Mathf.Abs(h_in);
            if (Input.GetKey("left shift")){
                m_CharStats.isSprinting = true;
                vertical = (v_in / total_in) * Time.deltaTime * m_CharStats.move_speed * m_CharStats.sprint_speed_multiplier;
                horizontal = (h_in / total_in) * Time.deltaTime * m_CharStats.move_speed * m_CharStats.sprint_speed_multiplier;
            } else {
                m_CharStats.isSprinting = false;
                vertical = (v_in / total_in) * Time.deltaTime * m_CharStats.move_speed;
                horizontal = (h_in / total_in) * Time.deltaTime * m_CharStats.move_speed;
            }
            //movespeed penalties
            if (vertical < 0){
                vertical = vertical * m_CharStats.backpedal_penalty;
                horizontal = horizontal * m_CharStats.backpedal_penalty;
            } else if (horizontal != 0){
                horizontal = horizontal * m_CharStats.strafe_penalty;
            }
            transform.Translate(horizontal, 0, vertical);
        }

        //Jumping
        if (Input.GetKeyDown("space")){
            m_Rb.AddForce(Vector3.up * m_CharStats.jump_strength);
        }

        //Environment interaction
        if (rayCastTarget){
            rayCastTarget = m_CharStats.raycastTarget;
            m_ItemStatus = rayCastTarget.GetComponent<ItemStatusManager>();
            //Item interaction
            //Picking up items
            if (m_ItemStatus && m_ItemStatus.inInventory == false){
                if (rayCastTarget.tag == "Item" && m_ItemStatus.distanceToPlayer < m_CharStats.pickupRange){
                    if (Input.GetKeyDown("e")){
                        rayCastTarget.gameObject.SetActive(false);
                        m_inventory.addToInventory(rayCastTarget);
                    }
                }
            } else if (m_ItemStatus && m_ItemStatus.inInventory){
                //Interact with items in inventory
                if (rayCastTarget.tag == "Item"){
                    if (Input.GetKeyDown("e")){
                        m_inventory.toggle();
                        rayCastTarget.SetParent(transform);
                        rayCastTarget.transform.position 
                            = playerCamera.transform.rotation * Vector3.one * 5.0f;
                    }
                }
            }
        }
        
        //Open or close inventory

        if (Input.GetKeyDown("q")){
            m_inventory.toggle();
        }
    }
    
}
