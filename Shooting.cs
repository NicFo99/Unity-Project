using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

public class Shooting : MonoBehaviour
{
    [Header("Nome Identificativo:")] 
    private string _nome = "fucile 1";
    //funzione per richiamare il nome del fucile in caso di necessità
    public string GetNomeFucile()
    {
        return this._nome;
        //per recuperare da un altro script :
        //string nome = player.GetComponentInChild<ShotingPlayer>().GetNomeFucile();
    }
    [Header("Impostazioni fucile:")] 
    bool aRipetizione;
    private bool IsShotting, Reloading, _ReadyToShoot;
    
    //inizio caratterizzazione dell'arma
    private int sizeCaricatore = 30 ;
    private int BulletPerShot = 1 ;
    private int BulletsLeft;
    private int BulletsShot;

    private float TempoRicarica = 1;
    private float TimeBetweenShots = 0.5f ;
    private float TimeBetweenBullets = 0.5f ;
    private float _maxdistance = 100;
    private float Spread = 2; 
    //fine caratterizzazione dell arma
    
    [Header("Uscite")] 
    [SerializeField] Camera _playerCam;
    [SerializeField] private LayerMask definitedLayer;
    [SerializeField] TextMeshProUGUI bullets;
    RaycastHit _rayhit;
    private Animator animator;
    void Start()
    {
        IsShotting = false;
        Reloading = false;
        _ReadyToShoot = true;
        BulletsLeft = sizeCaricatore;
    }
    void Update() {
        if (aRipetizione) IsShotting = Input.GetKey(KeyCode.Mouse0);
        else IsShotting = Input.GetKeyDown(KeyCode.Mouse0);
    
        //ricarica
        if (Input.GetKeyDown(KeyCode.R) && BulletsLeft < sizeCaricatore && !Reloading)
        {
            Reload();
            animator.SetBool("ReloadAnim", true);
        }
        //spara
        if (IsShotting && BulletsLeft > 0 && !Reloading && _ReadyToShoot){
            BulletsShot = BulletPerShot;
            Shoot(); 
            animator.SetBool("ShootAnim", true);
        }

        bullets.text = BulletsLeft + " / " + sizeCaricatore;
    }
    void Reload() {
        Reloading = true;
        Invoke("Reloding", TempoRicarica);
        Reloading = false;
    }

    void Reloding() {
        BulletsLeft = sizeCaricatore;
    }
    void Shoot() {
        _ReadyToShoot = false;
        //raycast
        float x = Random.Range(-Spread ,Spread);
        float y = Random.Range(-Spread, Spread);
        Vector3 direction = _playerCam.transform.forward + new Vector3(x, y, 0);
        if (Physics.Raycast(_playerCam.transform.position, direction, out _rayhit, _maxdistance, definitedLayer))
        {
            Debug.Log(_rayhit.collider.name);
        }
        BulletsLeft--;
        BulletsShot--;
        Invoke("ReadyToShoot", TimeBetweenShots);
        if(BulletsShot > 0 && BulletsLeft > 0) Invoke("Shoot",TimeBetweenBullets);
    }

    void ReadyToShoot() {
        _ReadyToShoot = true;
    }
    
}