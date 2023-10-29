public interface IGameListener
{

}
public interface IStartListener : IGameListener
{
    void Start();
}
public interface IInitListener : IGameListener
{
    void OnInit();
}
public interface IDisableListener : IGameListener
{
    void Disable();
}

