using Cinemachine;
using Photon.Pun;
using UnityEngine;

public class JoinedGameController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    
    private void Start()
    {
        Vector3 randomPosition = GenerateRandomPosition();

        GameObject playerGameObject = PhotonNetwork.Instantiate("Prefabs/Players/CubicPlayerPrefab", 
            randomPosition, Quaternion.identity);

        PlayerController playerController = playerGameObject.GetComponent<PlayerController>();
        playerController.SetBodyColor(Random.ColorHSV());
        
        _virtualCamera.Follow = playerGameObject.transform;
    }
    
    private static Vector3 GenerateRandomPosition()
    {
        while (true)
        {
            Vector3 randomPosition = new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));
            
            if (Physics.CheckBox(randomPosition, new Vector3(0.5f, 0.5f, 0.5f)))
                continue;
            
            return randomPosition;
        }
    }
}