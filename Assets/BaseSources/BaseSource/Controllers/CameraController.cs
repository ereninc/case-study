using UnityEngine;
using Cinemachine;
using Sirenix.OdinInspector;

public class CameraController : Singleton<CameraController>
{
    public Camera main;
    public Camera uiCamera;
    [SerializeField] private CinemachineVirtualCamera gameplayCamera, finalCamera;
    [SerializeField] private CinemachineVirtualCamera activeCamera;

    public override void Initialize()
    {
        base.Initialize();
        ChangeCamera(CameraType.Gameplay);
    }

    [Button]
    public void ChangeCamera(CameraType type)
    {
        if (activeCamera != null)
        {
            activeCamera.SetActiveGameObject(false);
        }

        switch (type)
        {
            case CameraType.Gameplay:
                gameplayCamera.SetActiveGameObject(true);
                activeCamera = gameplayCamera;
                break;
            case CameraType.Final:
                finalCamera.SetActiveGameObject(true);
                activeCamera = finalCamera;
                break;
        }
    }
}

public enum CameraType
{
    Gameplay,
    Final
}