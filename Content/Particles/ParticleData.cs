using System;
using System.Collections;
using System.Runtime.CompilerServices;

#nullable enable
namespace EndlessEscapade.Content.Particles
{
    internal static class ParticleData<T>
    {
        internal static BitArray componentFlags;
        internal static T?[] particleData;

        static ParticleData() {
            particleData = new T[ParticleManager.Size];
            componentFlags = new BitArray(ParticleManager.Size);

            ParticleManager.OnClear += ClearAllData;
            ParticleManager.OnDestroyParticle += CleanDestroyed;
        }

        internal static void Set(int index, T value) {
            particleData[index] = value;
            componentFlags[index] = true;
        }
        internal static void Set(int index, in T value) {
            particleData[index] = value;
            componentFlags[index] = true;
        }
        internal static void SetAdded(int index) {
            componentFlags[index] = true;
        }
        internal static bool Has(int index) {
            return componentFlags[index];
        }
        internal static bool Remove(int index) {
            if (componentFlags[index]) {
                componentFlags[index] = false;
                return true;
            }
            return false;
        }

        internal static ref T? Get(int index) {
            ref T? component = ref particleData[index];
            return ref component;
        }

        private static void CleanDestroyed(int id) {
            particleData[id] = default;
        }

        private static void ClearAllData() {
            Array.Clear(particleData);
        }
    }
}
