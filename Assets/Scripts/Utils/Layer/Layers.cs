using UnityEngine;

namespace Utils.Layer
{
    public class Layers
    {
        public const string Default = "Default";
        public const string Bullet = "Bullet";
        public const string Enemy = "Enemy";
        public const string Player = "Player";
        
		private static readonly Layer _playerLayer = new Layer(Player);
		private static readonly Layer _enemyLayer = new Layer(Enemy);
		private static readonly Layer _bulletLayer = new Layer(Bullet);
		private static readonly Layer _defaultLayer = new Layer(Default);
        
        
		public static int PlayerLayer => _playerLayer.Id;
		public static int EnemyLayer => _enemyLayer.Id;
		public static int BulletLayer => _bulletLayer.Id;
		public static int DefaultLayer => _defaultLayer.Id;
        
        
        private class Layer
        {
            private readonly string _name;

            private int? _id;

            public int Id
            {
                get
                {
                    if (!_id.HasValue)
                        _id = LayerMask.NameToLayer(_name);
                    return _id.Value;
                }
            }

            public Layer(string name)
            {
                _name = name;
            }
        }
    }
}