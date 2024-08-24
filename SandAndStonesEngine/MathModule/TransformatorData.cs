using System.Numerics;

namespace SandAndStonesEngine.MathModule
{
    public class TransformatorData
    {
        public Vector3 Position;
        public Vector3 Movement;
        public Vector3 Forward;
        public Vector3 Up;
        public Vector2 ScrollMovement;
        public float MoveSpeed;
        public float ScrollSpeedPixels;
        public Vector3 Rotation;

        public Vector3 LookDir
        {
            get
            {
                Quaternion lookRotation = Quaternion.CreateFromYawPitchRoll(Rotation.X, Rotation.Y, Rotation.Z);

                Vector3 lookDir = Vector3.Transform(Forward, lookRotation);
                return lookDir;
            }
        }
        public Vector3 Target
        {
            get { return Position + LookDir; }
        }

        public TransformatorData(Vector3 position, Vector3 forward, Vector3 up, Vector3 rotation, float moveSpeed, float scrollSpeedPixels)
        {
            SetMovement(new Vector3(0, 0, 0));
            SetScrollMovement(new Vector2(0, 0));
            SetDirections(position, forward, up);
            SetSpeed(moveSpeed);
            SetScrollSpeedPixels(scrollSpeedPixels);
            SetRotation(rotation);
        }

        public void SetDirections(Vector3 position, Vector3 forward, Vector3 up)
        {
            Position = position;
            Forward = forward;
            Up = up;
        }

        public void SetMovement(Vector3 movement)
        {
            Movement = movement;
        }

        public void SetSpeed(float moveSpeed)
        {
            MoveSpeed = moveSpeed;
        }

        public void SetScrollMovement(Vector2 scrollMovement)
        {
            ScrollMovement = scrollMovement;
        }

        public void SetScrollSpeedPixels(float scrollSpeedPixels)
        {
            ScrollSpeedPixels = scrollSpeedPixels;
        }

        public void SetRotation(Vector3 rotation)
        {
            Rotation = rotation;
        }
    }
}
