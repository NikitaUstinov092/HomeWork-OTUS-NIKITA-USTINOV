public interface IGameListener
{

}

public interface IStartListner : IGameListener
{
    void Start();
}

public interface IInitListner : IGameListener
{
    void OnInit();
}

public interface IDisableListner : IGameListener
{
    void Disable();
}

