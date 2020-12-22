using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class SpaceShipAutoShootModule : MonoBehaviour
{
    [SerializeField]
    private bool pairShoot = false;

    [SerializeField]
    private Collider targetZone = null;

    [SerializeField]
    private AnimShootModuleActivator anim = null;

    private Action<Collider> checkMethod;


    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if(targetZone == null)
        {
            checkMethod = ZoneSimpleCheck;
        }
        else
        {
            checkMethod = CheckZoneWithTarget;
        }
    }

    private void CheckZoneWithTarget(Collider other)
    {
        if (other == targetZone)
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
