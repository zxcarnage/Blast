namespace Utils.Transaction.Impls
{
    public class IntDeltaApplier : IDeltaApplier<int>
    {
        public int Add(int left, int right) => left + right;
        public int Decrease(int left, int right) => left - right;
    }
}