using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using MonoMod.Utils;

namespace EndlessEscapade.Content.Particles
{
    /// <summary>Represents a <see cref="Particles.Particle"/> type.</summary>
    /// <remarks>It's recommended to inherit <see cref="ParticleType{T}"/> instead.</remarks>
    internal abstract class ParticleType : ModTexturedType {
        private int type;
        private Particle particle;

        /// <summary>The type of this <see cref="ParticleType"/></summary>
        public int Type => type;

        /// <summary>If <see cref="InstancePerEntity"/> is <see langword="true"/> this field is the particle this type is attached to, otherwise it's an invalid particle. </summary>
        protected Particle Particle => particle;

        /// <summary>If <see langword="true"/>, each particle will have its own instance of this particle type.</summary>
        /// <remarks>Defaults to <see langword="true"/> if there's fields on the current type, <see langword="false"/> otherwise.</remarks>
        public virtual bool InstancePerEntity => HasFields(GetType());

        /// <summary>Sets properties for the provided particle. Like color, time left, etc.</summary>
        /// <param name="particle">The particle that was just spawned.</param>
        public virtual void SetDefaults(Particle particle) {
            particle.Scale = new Vector2(1);
        }
        /// <summary>Called each tick on the specified particle.</summary>
        /// <param name="particle"></param>
        public virtual void Update(Particle particle) { }

        /// <summary>Controls the instance for new particles.<br /></summary>
        /// <param name="particle"></param>
        /// <returns></returns>
        public virtual ParticleType NewInstance(Particle particle) {
            if (InstancePerEntity) {
                // try to invoke parameterless ctor
                if (GetType().GetConstructor(BindingFlags.Public | BindingFlags.Instance, Array.Empty<Type>()) is not null)
                    return (ParticleType)Activator.CreateInstance(GetType());
                // failed so clone
                return (ParticleType)MemberwiseClone();
            }
            return this; // singleton 
        }

        protected override void Register() {
            type = ParticleLoader.ReserveID();
            ModTypeLookup<ParticleType>.Register(this);
        }

        internal static bool HasFields(Type type) {
            return !type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Any(static field => field.DeclaringType != typeof(ParticleType) && field.DeclaringType.Assembly != typeof(ModType).Assembly);
        }
    }

    /// <summary>Represents a <see cref="Particle"/> type.</summary>
    /// <typeparam name="TSelf">The type inheriting this class.</typeparam>
    /// <remarks>This class contains additional stuff that's available knowing the type.<br />Like <see cref="Instance"/>.</remarks>
    internal class ParticleType<TSelf> : ParticleType where TSelf : ParticleType {
        private static bool hasParameterlessCtor = typeof(TSelf).GetConstructor(BindingFlags.Public | BindingFlags.Instance, Array.Empty<Type>()) != null;
        private static bool hasFields = ParticleType.HasFields(typeof(TSelf));

        /// <summary>The base instance of <see cref="TSelf"/>.</summary>
        public static TSelf Instance => ModContent.GetInstance<TSelf>();

        public static Particle SpawnAt(Vector2 position, Vector2 velocity) {
            Particle particle = Particle.SpawnParticle(position, velocity);
            ParticleType instance = Instance.NewInstance(particle);
            instance.SetDefaults(particle);
            return particle;
        }

        public override bool InstancePerEntity => !hasFields;

        public override ParticleType NewInstance(Particle particle) {
            if (InstancePerEntity) {
                if (hasParameterlessCtor)
                    return Activator.CreateInstance<TSelf>();

                return (TSelf)MemberwiseClone();
            }
            return this; // singleton
        }
    }
}
