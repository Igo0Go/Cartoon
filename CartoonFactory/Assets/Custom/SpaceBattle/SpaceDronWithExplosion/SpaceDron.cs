using UnityEngine;
using Cinemachine;

public class SpaceDron : MonoBehaviour
{
    [SerializeField]
    private CinemachineDollyCart cart = null;

    [SerializeField]
    private GameObject explosionPrefab = null;

    private Transform myTransform;

    private void Awake()
    {
        myTransform = transform;
    }

    public void Explosion()
    {
        Destroy(Instantiate(explosionPrefab, myTransform.position, myTransform.rotation), 2);
        cart.m_Position = 0;
    }
}
