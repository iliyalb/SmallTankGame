using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class TankController : MonoBehaviour
{
    private Camera _mainCamera;

    public LayerMask WhatIsGround;

    public Transform BulletSpawnPoint;

    public float BulletForce;

    private Animator _anim;

    // Use this for initialization
    void Start()
    {
        _anim = this.GetComponent<Animator>();
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameControl.GameControlInstance.IsGameEnded)
        {
            return;
        }
        RotateTank();

        CheckShootInput();
    }



    private void RotateTank()
    {
        RaycastHit hit;
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition + new Vector3(0, -Screen.height / 40f, 0));

        if (Physics.Raycast(ray, out hit, 100f, WhatIsGround))
        {
            transform.LookAt(hit.point);
        }
    }

    private void CheckShootInput()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            var bullet = PoolManager.Instance.GetABullet().GetComponent<Bullet>();

            GameControl.GameControlInstance.ChangeEnergy(-bullet.EnergyCost);

            bullet.Initialize(ColorContainer.Color.red);

            ShootBullet(bullet);
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            var bullet = PoolManager.Instance.GetABullet().GetComponent<Bullet>();

            GameControl.GameControlInstance.ChangeEnergy(-bullet.EnergyCost);

            bullet.Initialize(ColorContainer.Color.blue);

            ShootBullet(bullet);
        }



    }


    private void ShootBullet(Bullet bullet)
    {
        AudioManager.AudioManagerInstance.PlayAudio(0);
        _anim.SetInteger("ShootNumber", UnityEngine.Random.Range(1, 5));
        _anim.SetTrigger("Shoot");
        bullet.transform.position = BulletSpawnPoint.transform.position;
        bullet.transform.LookAt(BulletSpawnPoint.transform.forward * 100);
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * BulletForce, ForceMode.Force);
    }



}
