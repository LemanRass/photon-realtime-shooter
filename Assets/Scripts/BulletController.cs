using Photon.Pun;
using UnityEngine;

public class BulletController : MonoBehaviour, IPunObservable
{
    [SerializeField] private PhotonView _photonView;
    [SerializeField] private Rigidbody _rigidbody;

    private Vector3 _targetPosition;
    private Quaternion _targetRotation;
    
    public void AddForce(Vector3 force, ForceMode forceMode = ForceMode.Impulse)
    {
        _rigidbody.AddForce(force, forceMode);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_photonView.IsMine)
        {
            Transform parentTransform = other.transform;
            Vector3 globalScale = transform.localScale;
            
            transform.SetParent(other.transform);
            _rigidbody.isKinematic = true;
            
            
            transform.localScale = new Vector3(globalScale.x / parentTransform.lossyScale.x,
                globalScale.y / parentTransform.lossyScale.y,
                globalScale.z / parentTransform.lossyScale.z);

        }
    }

    private void LateUpdate()
    {
        if (!_photonView.IsMine)
        {
            if (_rigidbody != default) Destroy(_rigidbody);
            
            transform.position = Vector3.Lerp(transform.position, _targetPosition, 5 * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, _targetRotation, 5 * Time.deltaTime);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            Vector3 position = transform.position;
            Quaternion rotation = transform.rotation;
            stream.Serialize(ref position);
            stream.Serialize(ref rotation);
        }
        else
        {
            stream.Serialize(ref _targetPosition);
            stream.Serialize(ref _targetRotation);
        }
    }
}