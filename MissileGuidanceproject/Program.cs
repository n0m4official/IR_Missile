using System;
using System.Numerics;
using System.Threading;

namespace MissileGuidanceSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Vector3 targetPosition = new Vector3(100, 50, 0);
            Vector3 targetVelocity = new Vector3(1, 0, 0); // Target moving along the x-axis
            Vector3 initialExternalForce = new Vector3(0, -0.1f, 0); // Initial downward force
            Vector3 wind = new Vector3(0.1f, 0, 0); // Simulated wind force
            Vector3 gravity = new Vector3(0, -0.98f, 0); // Simulated gravity

            Missile missile = new Missile(new Vector3(0, 0, 0), targetPosition, targetVelocity, initialExternalForce, 1.0f, 100.0f);

            float deltaTime = 0.1f; // Time step

            while (!missile.HasReachedTarget)
            {
                missile.Update(deltaTime, wind, gravity);
                Console.WriteLine($"Missile Position: {missile.Position}");

                // Simulate adding decoys occasionally
                if (new Random().NextDouble() < 0.1)
                {
                    Vector3 decoyPosition = missile.Position + new Vector3(10, 10, 0);
                    Vector3 decoyVelocity = new Vector3(0.5f, 0.5f, 0); // Decoy with different velocity
                    Decoy decoy = new Decoy(decoyPosition, decoyVelocity);
                    missile.AddDecoy(decoy);
                }

                Thread.Sleep((int)(deltaTime * 1000)); // Wait for deltaTime milliseconds
            }

            Console.WriteLine("Target reached!");
        }
    }
}
