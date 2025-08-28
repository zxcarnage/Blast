namespace Utils.Transaction.Impls
{
    public class IntDeltaApplier : IDeltaApplier<int>
    {
        public int Add(int left, int right) => left + right;
    }
}