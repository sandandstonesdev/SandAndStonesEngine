using System.Numerics;

namespace SandAndStonesEngine.DataModels
{
    public class QuadTransformations
    {
        Matrix4x4 translationMatrix;
        Matrix4x4 scaleMatrix;
        Matrix4x4 rotationQuaternion;

        public QuadTransformations()
        {
            Translate(new Vector3(0, 0, 0));
            Rotate(new Quaternion(0, 0, 0, 1));
            Scale(new Vector2(1, 1));
        }
        public void Translate(Vector3 translation)
        {
            translationMatrix = Matrix4x4.CreateTranslation(translation);
        }
        public void Rotate(Quaternion rotation)
        {
            rotationQuaternion = Matrix4x4.CreateFromYawPitchRoll(rotation.X, rotation.Y, rotation.Z);
        }
        public void Scale(Vector2 scale)
        {
            scaleMatrix = Matrix4x4.CreateScale(scale.X, scale.Y, 1);
        }
        public Matrix4x4 Get()
        {
            return translationMatrix * rotationQuaternion * scaleMatrix;
        }
    }
}
