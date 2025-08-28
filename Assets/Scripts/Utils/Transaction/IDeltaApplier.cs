namespace Utils.Transaction
{
    public interface IDeltaApplier<T>
    {
        T Add(T left, T right);
        T Decrease(T left, T right);
    }
}