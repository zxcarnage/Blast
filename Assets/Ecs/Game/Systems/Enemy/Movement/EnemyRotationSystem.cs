using Ecs.Game.Components.Character;
using Ecs.Game.Components.Enemy;
using Ecs.Game.Components.Player;
using Scellecs.Morpeh;
using UnityEngine;

namespace Ecs.Game.Systems.Enemy.Movement
{
    public class EnemyRotationSystem : ISystem
    {
        private Filter _enemyFilter;
        private Filter _playerFilter;
        
        private Stash<TransformComponent> _transformStash;
        
        public World World { get; set; }

        public void OnAwake()
        {
            _enemyFilter = World.Filter
                .With<EnemyComponent>()
                .Without<DeadComponent>()
                .Build();
            
            _playerFilter = World.Filter
                .With<PlayerComponent>()
                .Build();
            
            _transformStash = World.GetStash<TransformComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var enemyEntity in _enemyFilter)
            {
                var enemyTransform = _transformStash.Get(enemyEntity).Value;

                foreach (var playerEntity in _playerFilter)
                {
                    var playerTransform = _transformStash.Get(playerEntity).Value;
                    var directionToPlayer = playerTransform.position - enemyTransform.position;
                    directionToPlayer.y = 0;
                    enemyTransform.rotation = Quaternion.LookRotation(directionToPlayer.normalized,Vector3.up);
                }
            }
        }

        public void Dispose()
        {
        }
    }
}