public class ControllerBaseModel : ObjectModel
{
    protected GameController GameController => GameController.Instance;
    protected CameraController CameraController => CameraController.Instance;
    protected PoolFactory PoolFactory => PoolFactory.Instance;

    protected void Reset()
    {
        transform.name = GetType().Name;
        transform.ResetLocal();
    }
}