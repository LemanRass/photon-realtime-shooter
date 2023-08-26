using System;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;

    public void AddForce(Vector3 force, ForceMode forceMode = ForceMode.Impulse)
    {
        _rigidbody.AddForce(force, forceMode);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(_rigidbody);
        transform.SetParent(other.transform);
    }
}