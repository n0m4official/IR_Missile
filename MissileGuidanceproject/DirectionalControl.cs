using System.Numerics;

namespace MissileGuidanceSystem
{
    public class DirectionalControl
    {
        private Vector3 externalForce;
        private float fuel;

        public DirectionalControl(Vector3 initialExternalForce, float initialFuel)
        {
            externalForce = initialExternalForce;
            fuel = initialFuel;
        }

        public void ApplyExternalForce(Vector3 force)
        {
            externalForce += force;
        }

        public void UpdateForces(Vector3 wind, Vector3 gravity, float deltaTime)
        {
            externalForce += wind * deltaTime;
            externalForce += gravity * deltaTime;
        }

        public Vector3 MoveTowards(Vector3 currentPosition, Vector3 direction, float speed, float deltaTime)
        {
            if (fuel <= 0)
            {
                speed = 0; // Stop the missile if it runs out of fuel
            }
            else
            {
                fuel -= deltaTime; // Reduce fuel over time
            }

            Vector3 displacement = direction * speed * deltaTime + externalForce * deltaTime * deltaTime / 2;
            externalForce *= 0.95f; // Simulate diminishing external force over time
            return currentPosition + displacement;
        }
    }
}
