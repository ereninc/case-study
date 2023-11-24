public class ControllerBaseModel : ObjectModel
{
    protected GameController GameController => GameController.Instance;
    protected AreaController AreaController => AreaController.Instance;
    protected CameraController CameraController => CameraController.Instance;
    protected PoolFactory PoolFactory => PoolFactory.Instance;
    protected ProductFactory ProductFactory => ProductFactory.Instance;

    protected void Reset()
    {
        transform.name = GetType().Name;
        transform.ResetLocal();
    }
}