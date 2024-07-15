using System;
using System.Collections.Generic;
using System.Numerics;

namespace MissileGuidanceSystem
{
    public class Decoy
    {
        public Vector3 Position { get; set; }
        public Vector3 Velocity { get; set; }

        public Decoy(Vector3 position, Vector3 velocity)
        {
            Position = position;
            Velocity = velocity;
        }
    }

    public class TrackingSystem
    {
        public Vector3 TargetPosition { get; private set; }
        private Vector3 TargetVelocity { get; set; }
        private List<Decoy> decoys;

        public TrackingSystem(Vector3 targetPosition, Vector3 targetVelocity)
        {
            TargetPosition = targetPosition;
            TargetVelocity = targetVelocity;
            decoys = new List<Decoy>();
        }

        public void AddDecoy(Decoy decoy)
        {
            decoys.Add(decoy);
        }

        public void UpdateTargetPosition(float deltaTime)
        {
            TargetPosition += TargetVelocity * deltaTime;

            foreach (var decoy in decoys)
            {
                decoy.Position += decoy.Velocity * deltaTime;
            }

            IgnoreDecoys();
        }

        private void IgnoreDecoys()
        {
            // Example criteria: decoys have erratic velocity changes or different heat signatures
            decoys.RemoveAll(decoy =>
            {
                return decoy.Velocity.Length() < TargetVelocity.Length() * 0.8f || decoy.Velocity.Length() > TargetVelocity.Length() * 1.2f;
            });
        }

        public Vector3 CalculateDirection(Vector3 currentPosition)
        {
            return Vector3.Normalize(TargetPosition - currentPosition);
        }
    }
}