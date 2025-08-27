namespace Game.Services.Pool
{
    public interface IPool <in TType, TObject>
    {
        TObject SpawnObject(TType type);
        void DespawnObject(TType type, TObject obj);
    }
}