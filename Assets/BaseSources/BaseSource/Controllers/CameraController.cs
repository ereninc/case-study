using UnityEngine;
using Cinemachine;
using Sirenix.OdinInspector;

public class CameraController : Singleton<CameraController>, IAreaStateObserver
{
    [SerializeField] private CinemachineVirtualCamera sewingCamera, paintingCamera;
    [SerializeField] private CinemachineVirtualCamera activeCamera;
    
    public Camera main;
    public Camera uiCamera;

    public override void Initialize()
    {
        base.Initialize();
        AddToAreaObserverList();
        ChangeCamera(CameraType.Sewing);
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
            case CameraType.Sewing:
                sewingCamera.SetActiveGameObject(true);
                activeCamera = sewingCamera;
                break;
            case CameraType.Painting:
                paintingCamera.SetActiveGameObject(true);
                activeCamera = paintingCamera;
                break;
        }
    }

    public void AddToAreaObserverList()
    {
        AreaController.AddListener(this);
        Debug.Log("CameraController is now AreaStateListener");
    }

    public void OnAreaStateChanged()
    {
        switch (AreaController.currentAreaState)
        {
            case AreaStates.Sewing:
                ChangeCamera(CameraType.Sewing);
                break;
            case AreaStates.Painting:
                ChangeCamera(CameraType.Painting);
                break;
        }
    }
}

public enum CameraType
{
    Sewing,
    Painting
}