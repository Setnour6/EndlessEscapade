namespace EndlessEscapade.Content.Particles
{
    internal abstract class ParticleComponent : IParticleComponent
    {
        protected virtual void Update(Particle particle) { }
    }
}
