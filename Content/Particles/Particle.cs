using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework;

#nullable enable
namespace EndlessEscapade.Content.Particles
{
    /// <summary>
    /// Represents a particle in-game.
    /// </summary>
    public readonly struct Particle
    {
        /// <summary>Represents an invalid particle.</summary>
        public static Particle None => new Particle();
        internal readonly int id;

        /// <summary>Clones the id of the provided particle.</summary>
        /// <remarks>This constructor does not clone the provided particle in-game.<br />
        /// Intended for use with <see href="https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/how-to-initialize-objects-by-using-an-object-initializer">object initializers.</see></remarks>
        internal Particle(Particle id) => this.id = id.id; 
        internal Particle(int id) => this.id = id;

        public readonly ref T? Get<T>() => ref ParticleData<T>.Get(id);
        internal readonly ref T? GetOrAdd<T>() {
            if (Has<T>()) SetAdded<T>();
            return ref Get<T>();
        }
        internal readonly ref T? GetOrAdd<T>(T defaultValue) {
            if (!Has<T>()) Set(defaultValue);
            return ref Get<T>();
        }
        internal readonly void SetAdded<T>() => ParticleData<T>.SetAdded(id);
        public readonly void Set<T>(T component) => ParticleData<T>.Set(id, component);
        public readonly void Set<T>(in T component) => ParticleData<T>.Set(id, in component);
        public readonly bool Has<T>() => ParticleData<T>.Has(id);
        public readonly bool Remove<T>() => ParticleData<T>.Remove(id);

        internal readonly bool Active => ParticleManager.activeParticles[id];
        internal readonly bool ValidID => id >= 1 && id < ParticleManager.Size;

        // shorthands
        // its not obligatory to create one of these each time a new component is made
        internal readonly ref Vector2 Position => ref GetOrAdd<PositionComponent>().Position;
        internal readonly ref Vector2 Velocity => ref GetOrAdd<VelocityComponent>().Velocity;
        internal readonly ref Vector2 Scale => ref GetOrAdd<ScaleComponent>().Scale;
        internal readonly ref Color Color => ref GetOrAdd<ColorComponent>().Color;
        internal readonly ref int TimeLeft => ref GetOrAdd<TimeLeftComponent>().TimeLeft;
        internal readonly ref float Rotation => ref GetOrAdd<RotationComponent>().Rotation;

        public override string ToString() {
            return $"ID: {id}";
        }

        public static Particle SpawnParticle(Vector2 position, Vector2 velocity) {
            Particle particle = ParticleManager.Create();
            particle.Position = position;
            particle.Velocity = velocity;
            return particle;
        }
    }
}
