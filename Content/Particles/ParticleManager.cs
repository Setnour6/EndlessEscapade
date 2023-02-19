using System;
using System.Collections;
using System.Collections.Generic;

namespace EndlessEscapade.Content.Particles
{
    internal static class ParticleManager
    {
        internal static int Size = 4096;
        internal static BitArray activeParticles = new BitArray(Size);
        private static Stack<int> freeParticles = new(Size / 4);
        private static int lastID;

        public static ActiveParticlesEnumerator ActiveParticles() => new(0);

        /// <summary>Creates a new particle.</summary>
        /// <returns>The newly created particle.</returns>
        /// <remarks>The returned particle is completely new, there's no components on it.</remarks>
        public static Particle Create() {
            if (!freeParticles.TryPop(out int id)) {
                if (lastID >= Size - 1) return default;
                id = ++lastID;
            }
            activeParticles[id] = true;
            OnCreateParticle?.Invoke(id);
            return new(id);
        }

        /// <summary>
        /// Destroys a particle.
        /// </summary>
        /// <param name="particle"></param>
        /// <returns><see langword="true"/> if the particle was succesfully destroyed or <see langword="false"/> the provided particle was previously destroyed or had an invalid ID.</returns>
        public static bool Destroy(Particle particle) {
            if (!particle.ValidID)
                return false;
            if (activeParticles[particle.id]) {
                activeParticles[particle.id] = false;
                freeParticles.Push(particle.id);
                OnDestroyParticle?.Invoke(particle.id);
                return true;
            }
            return false;
        }

        /// <summary>Clears all data, intended for unloading.</summary>
        internal static void Clear() {
            activeParticles.SetAll(false);
            freeParticles.Clear();
            lastID = 0;
            OnClear?.Invoke();
        }


        internal static event Action<int> OnCreateParticle;
        internal static event Action<int> OnDestroyParticle;
        internal static event Action OnClear;

        public struct ActiveParticlesEnumerator : IEnumerable<Particle>, IEnumerator<Particle>
        {
            int position;

            internal ActiveParticlesEnumerator(int start) {
                position = start - 1;
            }

            public Particle Current => new(position);
            object IEnumerator.Current => Current;

            public bool MoveNext() {
                int next = position + 1;
                while (next < Size) {
                    if (activeParticles[next]) {
                        position = next;
                        return true;
                    }
                    next++;
                }
                return false;
            }

            public ActiveParticlesEnumerator GetEnumerator() => this;

            IEnumerator<Particle> IEnumerable<Particle>.GetEnumerator() => GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            public void Reset() {
                position = -1;
            }

            public void Dispose() {

            }
        }
    }
}
