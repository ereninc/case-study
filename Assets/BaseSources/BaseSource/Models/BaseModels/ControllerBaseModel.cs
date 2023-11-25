public class ControllerBaseModel : ObjectModel
{
    protected GameController GameController => GameController.Instance;
    protected AreaController AreaController => AreaController.Instance;
    protected CameraController CameraController => CameraController.Instance;
    protected PoolFactory PoolFactory => PoolFactory.Instance;
    protected ProductFactory ProductFactory => ProductFactory.Instance;
    protected LevelController LevelController => LevelController.Instance;
    protected AudioController AudioController => AudioController.Instance;

    protected void Reset()
    {
        // transform.name = GetType().Name;
        transform.ResetLocal();
    }
}