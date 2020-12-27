using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class ShootModule : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet = null;

    [SerializeField]
    private GameObject muzzleFlash = null;

    [SerializeField]
    private AudioClip shootClip = null;

    [SerializeField]
    private Transform startShootPoint = null;

    [SerializeField]
    private List<Transform> targetShootPoints = null;

    [SerializeField]
    private int currentActiveTargetIndex = 0;

    [SerializeField]
    [Range(1,100)]
    private float bulletSpeed = 1;

    [SerializeField]
    [Range(0, 10)]
    private float bulletLifetime = 1;

    [SerializeField]
    [Range(0, 10)]
    private float shootPause = 1;

    [SerializeField]
    private bool shoot = false;
    [SerializeField]
    private bool onceShoot = false;
    [SerializeField]
    private bool debug = false;

    private AudioSource shootSource;

    private float currentShootPauseValue = 0;


    private void Awake()
    {
        shootSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (onceShoot)
            ShootOnce();
        else
            LoopShoot();
    }

    public void AnimStartShoot() => shoot = true;
    public void AnimStopShoot() => shoot = false;

    private void ShootOnce()
    {
        if(shoot)
        {
            muzzleFlash.SetActive(true);
            StartCoroutine(DisactiveMuzzleFlashCoroutine(shootPause));
            shoot = false;
            shootSource.PlayOneShot(shootClip);
            InstanceBullet();
        }
    }

    private void LoopShoot()
    {
        if (shoot)
        {
            if (currentShootPauseValue == 0)
            {
                muzzleFlash.SetActive(true);
                InstanceBullet();
            }
            currentShootPauseValue += Time.deltaTime;
            if(currentShootPauseValue >= shootPause)
            {
                currentShootPauseValue = 0;
            }
        }
        else
        {
            muzzleFlash.SetActive(false);
            currentShootPauseValue = 0;
        }
    }

    private void InstanceBullet()
    {
        var bul = Instantiate(bullet, startShootPoint.position, Quaternion.identity);
        bul.transform.forward = targetShootPoints[currentActiveTargetIndex].position - startShootPoint.position;
        bul.GetComponent<Bullet>().LaunchBullet(bulletSpeed, bulletLifetime);
        shootSource.PlayOneShot(shootClip);
    }

    private IEnumerator DisactiveMuzzleFlashCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        muzzleFlash.SetActive(false);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if(debug)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(startShootPoint.position, targetShootPoints[currentActiveTargetIndex].position);
        }
    }
#endif


}
