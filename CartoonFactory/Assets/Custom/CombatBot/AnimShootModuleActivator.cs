using UnityEngine;



public class AnimShootModuleActivator : MonoBehaviour
{
    public ShootModule rightShoot;
    public ShootModule leftShoot;

    public void StartShootForRight() => rightShoot.AnimStartShoot();

    public void StartShootForLeft() => leftShoot.AnimStartShoot();

    public void StopShootForRight() => rightShoot.AnimStopShoot();

    public void StopShootForLeft() => leftShoot.AnimStopShoot();
}
