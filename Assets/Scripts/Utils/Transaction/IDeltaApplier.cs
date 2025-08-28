namespace Utils.Transaction
{
    public interface IDeltaApplier<T>
    {
        T Add(T left, T right);
    }
}