using Ecs.Game.Components.Character;
using Ecs.Game.Components.Player;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Ecs.Game.Systems.Movement
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class CameraRotationSystem : ISystem
    {
        public World World { get; set; }
        
        private Stash<LookDirectionComponent> _lookDirectionStash;
        private Stash<TransformComponent> _transformStash;
        private Filter _playerFilter;
        private Filter _playerHeadFilter;

        public void OnAwake()
        {
            _lookDirectionStash = World.GetStash<LookDirectionComponent>();
            _transformStash = World.GetStash<TransformComponent>();
            
            _playerFilter = World.Filter
                .With<PlayerComponent>()
                .Build();
            
            _playerHeadFilter = World.Filter
                .With<PlayerHeadComponent>()
                .Build();
        }

        public void OnUpdate(float deltaTime)
        {
            var lookDirection = Vector3.zero;

            foreach (var playerEntity in _playerFilter)
            {
                lookDirection = _lookDirectionStash.Get(playerEntity).Value;
                HandlePlayerRotation(playerEntity);
            }

            foreach (var playerHead in _playerHeadFilter)
            {
                HandleHeadRotation(playerHead);
            }

            return;

            void HandleHeadRotation(Entity playerHead)
            {
                var headTransform = _transformStash.Get(playerHead).Value;
                headTransform.localRotation = Quaternion.Euler(lookDirection.x, 0, 0);
            }

            void HandlePlayerRotation(Entity player)
            {
                var playerTransform = _transformStash.Get(player).Value;
                playerTransform.localRotation = Quaternion.Euler(0, lookDirection.y, 0);
            }
        }

        public void Dispose()
        {
            _lookDirectionStash.Dispose();
            _transformStash.Dispose();
        }
    }
}