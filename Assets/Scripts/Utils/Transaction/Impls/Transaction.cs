using UnityEngine;

namespace Utils.Transaction.Impls
{
    public class Transaction<T> : ITransaction<T>
    {
        private readonly IDeltaApplier<T> _deltaApplier;
        
        private bool _hasBackup;
        
        private T _initialValue;

        public Transaction(IDeltaApplier<T> deltaApplier)
        {
            _deltaApplier = deltaApplier;
        }

        public T CurrentValue { get; private set; }
        public bool IsDirty { get; private set; }

        public void SaveBackup(T initial)
        {
            _initialValue = initial;
            CurrentValue = initial;
            _hasBackup = true;
            IsDirty = false;
        }

        public T Add(T delta)
        {
            EnsureStarted();
            var next = _deltaApplier.Add(CurrentValue, delta);
            IsDirty = true;
            CurrentValue = next;
            return CurrentValue;
        }

        public T Decrease(T delta)
        {
            EnsureStarted();
            var next = _deltaApplier.Decrease(CurrentValue, delta);
            IsDirty = true;
            CurrentValue = next;
            return CurrentValue;
        }

        public T Commit()
        {
            EnsureStarted();
            var delta = _deltaApplier.Decrease(CurrentValue, _initialValue);
            _initialValue = CurrentValue;
            IsDirty = false;
            return delta;
        }

        public T Revert()
        {
            EnsureStarted();
            CurrentValue = _initialValue;
            IsDirty = false;
            return _initialValue;
        }

        private void EnsureStarted()
        {
            if (!_hasBackup)
                Debug.LogError("Transaction has not been started. Call SaveBackup first.");
        }
    }
}