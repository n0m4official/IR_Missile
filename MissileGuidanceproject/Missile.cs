using System.Numerics;

namespace MissileGuidanceSystem
{
    public class Missile
    {
        public Vector3 Position { get; private set; }
        public Vector3 TargetPosition => trackingSystem.TargetPosition;
        public float Speed { get; private set; }
        public bool HasReachedTarget => Vector3.Distance(Position, TargetPosition) < 1.0f;

        private TrackingSystem trackingSystem;
        private DirectionalControl directionalControl;

        public Missile(Vector3 initialPosition, Vector3 targetPosition, Vector3 targetVelocity, Vector3 initialExternalForce, float speed = 1.0f, float initialFuel = 100.0f)
        {
            Position = initialPosition;
            Speed = speed;

            trackingSystem = new TrackingSystem(targetPosition, targetVelocity);
            directionalControl = new DirectionalControl(initialExternalForce, initialFuel);
        }

        public void Update(float deltaTime, Vector3 wind, Vector3 gravity)
        {
            trackingSystem.UpdateTargetPosition(deltaTime);
            Vector3 direction = trackingSystem.CalculateDirection(Position);
            directionalControl.UpdateForces(wind, gravity, deltaTime);
            Position = directionalControl.MoveTowards(Position, direction, Speed, deltaTime);
        }

        public void ApplyExternalForce(Vector3 force)
        {
            directionalControl.ApplyExternalForce(force);
        }

        public void AddDecoy(Decoy decoy)
        {
            trackingSystem.AddDecoy(decoy);
        }
    }
}
