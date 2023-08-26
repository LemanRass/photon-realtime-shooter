using Photon.Pun;
using UnityEngine;

public class PlayerController : MonoBehaviour, IPunObservable
{
    [SerializeField] private PhotonView _photonView;
    [SerializeField] private float _moveSpeed = 5.0f;
    [SerializeField] private float _lerpSpeed = 5.0f;

    
    private Vector3 _targetPosition;
    private Quaternion _targetRotation;
    
    private void Update()
    {
        if (_photonView.IsMine)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            transform.Translate(new Vector3(horizontal, 0, vertical) * (_moveSpeed * Time.deltaTime));
        }
    }

    private void LateUpdate()
    {
        if (!_photonView.IsMine)
        {
            transform.position = Vector3.Lerp(transform.position, _targetPosition, _lerpSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, _targetRotation, _lerpSpeed * Time.deltaTime);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting) // We are the player who is moving
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else // We are the other player
        {
            _targetPosition = (Vector3)stream.ReceiveNext();
            _targetRotation = (Quaternion)stream.ReceiveNext();
        }
    }
}
