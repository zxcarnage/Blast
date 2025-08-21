using System;
using System.Collections.Generic;
using Scellecs.Morpeh;
using UnityEngine;
using VContainer.Unity;

namespace Ecs.Core.Utils
{
    public class RootWorld : IInitializable, IDisposable, ITickable, IFixedTickable, IPostLateTickable
    {
        private readonly IEnumerable<ICleanupSystem> _cleanUpSystems;
        private readonly IEnumerable<IFixedSystem> _fixedUpdateSystems;
        private readonly IEnumerable<ISystem> _updateSystems;
        private readonly IEnumerable<IInitializer> _initializeSystems;
        private readonly World _world;
        
        public RootWorld(
            World world,
            IEnumerable<ISystem> updateSystems,
            IEnumerable<IFixedSystem> fixedUpdateSystems,
            IEnumerable<ICleanupSystem> cleanUpSystems,
            IEnumerable<IInitializer> initializeSystems
        )
        {
            _world = world;
            _updateSystems = updateSystems;
            _fixedUpdateSystems = fixedUpdateSystems;
            _cleanUpSystems = cleanUpSystems;
            _initializeSystems = initializeSystems;
        }
 
        public void Initialize()
        {
            var defaultGroup = _world.CreateSystemsGroup();

            foreach (var initializeSystem in _initializeSystems)
            {
                defaultGroup.AddInitializer(initializeSystem);
            }
            
            foreach (var updateSystem in _updateSystems)
            {
                defaultGroup.AddSystem(updateSystem);
            }
 
            foreach (var fixedUpdateSystem in _fixedUpdateSystems)
            {
                defaultGroup.AddSystem(fixedUpdateSystem);
            }
 
            foreach (var cleanUpSystem in _cleanUpSystems)
            {
                defaultGroup.AddSystem(cleanUpSystem);
            }
            
            _world.AddSystemsGroup(0, defaultGroup);
        }
        
        public void Tick()
        {
            _world.Update(Time.deltaTime);
        }

        public void FixedTick()
        {
            _world.FixedUpdate(Time.fixedDeltaTime);
        }

        public void PostLateTick()
        {
            _world.CleanupUpdate(Time.deltaTime);
        }
 
        public void Dispose()
        {
            _world?.Dispose();
        }
    }
}