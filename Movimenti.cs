using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimenti : MonoBehaviour
{
    //variabili per il movimento
    private CharacterController controller;
    private float velocità = 5f;

    //variabili per salto/gravità
    public LayerMask TerraMask;
    Vector3 Velocitày;
    private float gravità = -9.8f;
    private float altezzasalto = 3f;
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }
    
    void Update()
    {
        //movimento player
        float z = Input.GetAxis("Vertical");
        float x = Input.GetAxis("Horizontal");
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * Time.deltaTime * velocità);

        //gravità
        bool grounded = controller.isGrounded;
        if (grounded && Velocitày.y < 0)
        {
            Velocitày.y = -10f;
        }

        //salto
        if (Input.GetButtonDown("Jump") && grounded)
        {
            Velocitày.y = Mathf.Sqrt(gravità * altezzasalto * -2f);
        }

        //aggiornamento sulla velocità di gravità o di salto
        Velocitày.y += gravità * Time.deltaTime;
        controller.Move(Velocitày * Time.deltaTime);
    }
}
