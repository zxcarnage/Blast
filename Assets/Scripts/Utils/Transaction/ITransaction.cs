namespace Utils.Transaction
{
    public interface ITransaction<T>
    {
        T CurrentValue { get; }
        bool IsDirty { get; }
        void SaveBackup(T initial);
        T Add(T delta);
        T Decrease(T delta);
        T Commit();
        T Revert();
    }
}