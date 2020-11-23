﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> decals = null;

    private float bulletSpeed;
    private float bulletLiveTime;

    private Vector3 oldPos;
    private Transform myTransform;
    private float counter = 0;

    private void Awake()
    {
        myTransform = transform;
    }

    /// <summary>
    /// Запустить снаряд
    /// </summary>
    /// <param name="speed">Скорость снаряда</param>
    /// <param name="liveTime">время жизни снаряда</param>
    public void LaunchBullet(float speed, float liveTime)
    {
        bulletLiveTime = liveTime;
        bulletSpeed = speed;
        oldPos = myTransform.position;
    }

    void Update()
    {
        myTransform.position += myTransform.forward * bulletSpeed * Time.deltaTime;
        CheckHit();
        oldPos = myTransform.position;
        counter += Time.deltaTime;
        if(counter >= bulletLiveTime)
        {
            Destroy(gameObject);
        }
    }

    private void CheckHit()
    {
        if(Physics.Linecast(oldPos, myTransform.position, out RaycastHit hit))
        {
            for (int i = 0; i < decals.Count; i++)
            {
                Instantiate(decals[i], hit.point + hit.normal * 0.1f, Quaternion.identity).transform.forward = hit.normal;
            }
            Destroy(gameObject);
        }
    }
}