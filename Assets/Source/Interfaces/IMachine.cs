public interface IMachine
{
    void OnStartProcess();
    void OnFinishProcess();
    void OnAvailableAgain(Product product);
}