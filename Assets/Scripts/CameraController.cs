using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance { get; private set; }
    
    private void Awake()
    {
        instance = this;
    }
    
    
    [field:SerializeField] public Camera camera { get; private set; }
    [field:SerializeField] public CinemachineVirtualCamera virtualCamera { get; private set; }
}
