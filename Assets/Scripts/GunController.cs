using Photon.Pun;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public static GunController instance { get; private set; }
    private void Awake()
    {
        instance = this;
    }
    
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _bulletSpeed = 1000;
    
    public void Shoot()
    {
        GameObject bulletGameObject = PhotonNetwork.Instantiate("Prefabs/BulletPrefab", 
            _firePoint.position, Quaternion.identity);
        BulletController bullet = bulletGameObject.GetComponent<BulletController>();
        bullet.transform.forward = transform.forward;
        bullet.AddForce(transform.forward * _bulletSpeed);
    }
}
