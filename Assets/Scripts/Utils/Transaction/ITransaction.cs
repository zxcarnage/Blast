namespace Utils.Transaction
{
    public interface ITransaction<T>
    {
        void SaveBackup(T initial);
        void Add(T delta);
        T Commit();
        T Revert();
    }
}