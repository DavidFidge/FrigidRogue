using System;
using System.Collections.Generic;
using System.Linq;

using FrigidRogue.MonoGame.Core.Components;
using FrigidRogue.MonoGame.Core.Extensions;
using FrigidRogue.MonoGame.Core.Interfaces.Services;

using Microsoft.Xna.Framework;

namespace FrigidRogue.MonoGame.Core.Physics
{
    public class SteeringBehaviors
    {
        public Vector3 Calculate()
        {
            return new Vector3();
        }
    }

    public class MovableComponent : IMovable
    {
        public float Mass { get; set; }
        public float MaxSpeed { get; set; }
        public float MaxForce { get; set; }
        public float MaxTurnRate { get; set; }
        public Vector3 Velocity { get; set; }
        public Vector3 Heading { get; set; }
        public Vector3 Side { get; set; }
        public SteeringBehaviors SteeringBehaviors { get; set; }

        public void Update(IGameTimeService gameTimeService, Entity entity)
        {
            var steeringForce = SteeringBehaviors.Calculate();

            var acceleration = steeringForce / Mass;
            Velocity = acceleration * gameTimeService.GameTime.ElapsedGameTime.Milliseconds;
            Velocity.Truncate(MaxSpeed);

            var movement = Velocity * gameTimeService.GameTime.ElapsedGameTime.Milliseconds; 
            entity.Transform.ChangeTranslationRelative(movement);

            if (Velocity.LengthSquared() > 0.00001)
            {
                Heading = Vector3.Normalize(Velocity);
                //Side = Vector3.
            }
        }
    }

    public interface IMovable
    {
        float Mass { get; set; }
        float MaxSpeed { get; set; }
        float MaxForce { get; set; }
        float MaxTurnRate { get; set; }
        Vector3 Velocity { get; set; }
        Vector3 Heading { get; set; }
        Vector3 Side { get; set; }
        SteeringBehaviors SteeringBehaviors { get; set; }
    }
}
