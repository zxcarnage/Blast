using UnityEngine;

namespace Utils.Transaction.Impls
{
    public class Transaction<T> : ITransaction<T>
    {
        private readonly IDeltaApplier<T> _deltaApplier;
        
        private bool _hasBackup;
        
        private T _initialValue;
        private T _currentValue;

        public Transaction(IDeltaApplier<T> deltaApplier)
        {
            _deltaApplier = deltaApplier;
        }

        public void SaveBackup(T initial)
        {
            _initialValue = initial;
            _currentValue = initial;
            _hasBackup = true;
        }

        public void Add(T delta)
        {
            EnsureStarted();
            _currentValue = _deltaApplier.Add(_currentValue, delta);
        }

        public T Commit()
        {
            EnsureStarted();
            _initialValue = _currentValue;
            return _initialValue;
        }

        public T Revert()
        {
            EnsureStarted();
            _currentValue = _initialValue;
            return _initialValue;
        }

        private void EnsureStarted()
        {
            if (!_hasBackup)
                Debug.LogError("Transaction has not been started. Call SaveBackup first.");
        }
    }
}