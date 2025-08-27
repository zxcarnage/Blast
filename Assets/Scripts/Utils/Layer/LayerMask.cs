namespace Utils.Layer
{
    public class LayerMask
    {
        private static readonly Mask EnemyMask = new Mask(Layers.Enemy);
        private static readonly Mask PlayerMask = new Mask(Layers.Player);

		public static int Enemy => EnemyMask.Value;
		public static int Player => PlayerMask.Value;
		
		private class Mask
		{
			private readonly string[] _layerNames;

			private int? _value;

			public Mask(params string[] layerNames)
			{
				_layerNames = layerNames;
			}

			public int Value
			{
				get
				{
					if (!_value.HasValue)
						_value = UnityEngine.LayerMask.GetMask(_layerNames);
					return _value.Value;
				}
			}
		}
    }
}