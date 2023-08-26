using Photon.Pun;
using UnityEngine;

public class PlayerController : MonoBehaviour, IPunObservable
{
    [SerializeField] private PhotonView _photonView;
    [SerializeField] private float _moveSpeed = 5f;
    
    private void Update()
    {
        if (_photonView.IsMine)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            transform.Translate(new Vector3(horizontal, 0, vertical) * (_moveSpeed * Time.deltaTime));
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
            transform.position = (Vector3)stream.ReceiveNext();
            transform.rotation = (Quaternion)stream.ReceiveNext();
        }
    }
}
