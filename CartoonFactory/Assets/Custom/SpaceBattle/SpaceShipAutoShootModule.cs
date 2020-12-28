using UnityEngine;
using System;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class SpaceShipAutoShootModule : MonoBehaviour
{
    [SerializeField]
    private bool pairShoot = false;

    [SerializeField]
    private List<Collider> targetZone = null;

    [SerializeField]
    private AnimShootModuleActivator anim = null;

    [SerializeField]
    private int currentActiveTargetIndex = 0;

    private Action<Collider> checkMethod;


    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if(currentActiveTargetIndex < 0)
        {
            checkMethod = ZoneSimpleCheck;
        }
        else
        {
            if(targetZone == null || targetZone.Count -1 < currentActiveTargetIndex)
            {
                Debug.LogError("Алина!, на объекте " + gameObject.name + " ты пытаешься реагировать на зону с индексом " + currentActiveTargetIndex +
                    " но у тебя пустой лист зон или нет такого количества зон");
            }
            checkMethod = CheckZoneWithTarget;
        }
    }

    private void CheckZoneWithTarget(Collider other)
    {
        if (other == targetZone[currentActiveTargetIndex])
        {
            Shoot();
        }
    }

    private void ZoneSimpleCheck(Collider other)
    {
        if(other.CompareTag("Target"))
        {
            Shoot();
        }

    }

    private void Shoot()
    {
        if (pairShoot)
        {
            anim.StartShootForLeft();
            anim.StartShootForRight();
        }
        else if (UnityEngine.Random.Range(0, 2) == 0)
        {
            anim.StartShootForLeft();
        }
        else
        {
            anim.StartShootForRight();
        }
    }




    private void OnTriggerEnter(Collider other)
    {
        checkMethod(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            anim.StopShootForLeft();
            anim.StopShootForRight();
        }
    }
}
