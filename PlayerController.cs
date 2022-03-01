using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerController : MonoBehaviour
{
    public Transform player;
    private float sensibilità = 300f;
    private float rotazione;
    private float start_life = 10;
    [SerializeField] TextMeshProUGUI life_text;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        start_life = 100;
    }
    
    void Update()
    {
        float x   = Input.GetAxis("Mouse X")  * Time.deltaTime * sensibilità;
        float y = Input.GetAxis("Mouse Y")* Time.deltaTime * sensibilità;
        rotazione -= y;
        rotazione = Mathf.Clamp(rotazione, -60f, 60f);
        transform.localRotation = Quaternion.Euler(rotazione ,0 ,0);
        player.Rotate(Vector3.up * x);
        life_text.text = start_life + "/" + start_life;
    }
}