using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //public GameObject Gun;
    public float moveSpeed;
    public float runSpeed;
    public PlayerControls controls;
    public Camera mainCamera;
    public Rigidbody2D rb2d;
    public GameObject graphics;
    public GameObject gunPoint;
    private Vector2 moveInput;
    private Vector3 mousePos;
    private InputAction move;
    private InputAction fire;
    private InputAction sprint;
    private InputAction look;
    private InputAction reload;

    private void Awake(){
        controls = new PlayerControls();
    }
    private void OnEnable(){
        move = controls.Player.Move;
        fire = controls.Player.Fire;
        sprint = controls.Player.Sprint;
        look = controls.Player.Look;
        reload = controls.Player.Reload;
        reload.performed += Reload;
        reload.Enable();
        move.Enable();
        fire.Enable();
        sprint.Enable();
        look.Enable();
    }
    private void OnDisable(){
        move.Disable();
        fire.Disable();
        sprint.Disable();
        look.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        LookAt();
        Movement();
        Fire();
    }

    void LookAt(){
        mousePos = mainCamera.ScreenToWorldPoint(look.ReadValue<Vector2>());
        Vector3 lookTowards = new Vector3(mousePos.x - transform.position.x, mousePos.y - transform.position.y, 0);
        graphics.transform.up = lookTowards;
        gunPoint.transform.up = lookTowards;
    }

    void Movement(){
        moveInput = move.ReadValue<Vector2>();
        if(sprint.IsPressed()){
            rb2d.velocity = moveInput * runSpeed;
        }
        else{
            rb2d.velocity = moveInput * moveSpeed;
        }
    }

    private void Fire(){
        if(fire.IsPressed()){
            GetComponentInChildren<Gun>().Fire();
        }
    }

        private void Reload(InputAction.CallbackContext context){
        GetComponentInChildren<Gun>().Reload();
    }
}
