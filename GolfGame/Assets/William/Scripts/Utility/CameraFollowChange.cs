using UnityEngine;
using Cinemachine;

public class CameraFollowChange : Singleton<CameraFollowChange>
{
    private CinemachineVirtualCamera cameraObject;

    void Awake()
    {
        cameraObject = GetComponent<CinemachineVirtualCamera>();
    }

    public void FollowTarget(Transform toFollow)
    {
        cameraObject.Follow = toFollow;
    }
}
