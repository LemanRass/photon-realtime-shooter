using Photon.Pun;
using UnityEngine;

public class JoinedGameController : MonoBehaviour
{
    private void Start()
    {
        Vector3 randomPosition = GenerateRandomPosition();

        GameObject playerGameObject = PhotonNetwork.Instantiate("Prefabs/Players/CubicPlayerPrefab", 
            randomPosition, Quaternion.identity);

        MeshRenderer playerMeshRenderer = playerGameObject.GetComponent<MeshRenderer>();
        Material playerMaterial = new Material(playerMeshRenderer.sharedMaterial);
        playerMaterial.color = Random.ColorHSV();
        playerMeshRenderer.sharedMaterial = playerMaterial;
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