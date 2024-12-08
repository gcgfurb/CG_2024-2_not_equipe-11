using OpenTK.Mathematics;
using System;

namespace CG_Biblioteca
{
    public class Camera
    {
        //TODO: usar Ponto4D no lugar de Vector3
        private Vector3 _front = -Vector3.UnitZ;
        private Vector3 _up = Vector3.UnitY;
        private Vector3 _right = Vector3.UnitX;
        private float _pitch;
        private float _yaw = -MathHelper.PiOver2; // Without this, you would be started rotated 90 degrees right.
        private float _fov = MathHelper.PiOver2;

        public Camera(Vector3 position, float aspectRatio)
        {
            Position = position;
            AspectRatio = aspectRatio;
        }

        public Vector3 Position { get; set; }

        public float AspectRatio { private get; set; }

        public Vector3 Front => _front;

        public Vector3 Up => _up;

        public Vector3 Right => _right;

        public float Pitch
        {
            get => MathHelper.RadiansToDegrees(_pitch);
            set
            {
                var angle = MathHelper.Clamp(value, -89f, 89f);
                _pitch = MathHelper.DegreesToRadians(angle);
                UpdateVectors();
            }
        }

        public float Yaw
        {
            get => MathHelper.RadiansToDegrees(_yaw);
            set
            {
                _yaw = MathHelper.DegreesToRadians(value);
                UpdateVectors();
            }
        }

        public float Fov
        {
            get => MathHelper.RadiansToDegrees(_fov);
            set
            {
                var angle = MathHelper.Clamp(value, 1f, 90f);
                _fov = MathHelper.DegreesToRadians(angle);
            }
        }

        public Matrix4 GetViewMatrix()
        {
            return Matrix4.LookAt(Position, Position + _front, _up);
        }

        public Matrix4 GetProjectionMatrix()
        {
            return Matrix4.CreatePerspectiveFieldOfView(_fov, AspectRatio, 0.01f, 100f);
        }

        private void UpdateVectors()
        {
            _front.X = MathF.Cos(_pitch) * MathF.Cos(_yaw);
            _front.Y = MathF.Sin(_pitch);
            _front.Z = MathF.Cos(_pitch) * MathF.Sin(_yaw);

            _front = Vector3.Normalize(_front);

            _right = Vector3.Normalize(Vector3.Cross(_front, Vector3.UnitY));
            _up = Vector3.Normalize(Vector3.Cross(_right, _front));
        }

        public void Orbit(Vector3 target, float radius, float yaw, float pitch)
        {
            pitch = Math.Clamp(pitch, -89.0f, 89.0f);

            float yawRad = MathHelper.DegreesToRadians(yaw);
            float pitchRad = MathHelper.DegreesToRadians(pitch);

            Position = target + new Vector3(
                radius * MathF.Cos(pitchRad) * MathF.Cos(yawRad),
                radius * MathF.Sin(pitchRad),
                radius * MathF.Cos(pitchRad) * MathF.Sin(yawRad)
            );

            _front = Vector3.Normalize(target - Position);

            _right = Vector3.Normalize(Vector3.Cross(Front, Vector3.UnitY));
            _up = Vector3.Normalize(Vector3.Cross(Right, Front));
        }
    }
}