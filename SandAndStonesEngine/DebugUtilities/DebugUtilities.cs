using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SandAndStonesEngine.DebugUtilities
{
    public static class DebugUtilities
    {
        public static void DisplayVector2(Vector2 vector, string valueName)
        {
            Debug.WriteLine($"Vector {valueName}");
            Debug.WriteLine("--------------------");
            Debug.WriteLine($"{vector.X}");
            Debug.WriteLine($"{vector.Y}");
            Debug.WriteLine("--------------------");
        }

        public static void DisplayMatrix4x4(Matrix4x4 matrix, string matrixName)
        {
            Debug.WriteLine($"Matrix {matrixName}");
            Debug.WriteLine("--------------------");
            Debug.WriteLine($"{matrix.M11} {matrix.M12} {matrix.M13} {matrix.M14}");
            Debug.WriteLine($"{matrix.M21} {matrix.M22} {matrix.M23} {matrix.M24}");
            Debug.WriteLine($"{matrix.M31} {matrix.M32} {matrix.M33} {matrix.M34}");
            Debug.WriteLine($"{matrix.M41} {matrix.M42} {matrix.M43} {matrix.M44}");
            Debug.WriteLine("--------------------");
        }

        public static void DisplayDecomposedMatrix4x4(Matrix4x4 matrix)
        {
            Vector3 scale, translation;
            Quaternion rotation;
            Matrix4x4.Decompose(matrix, out scale, out rotation, out translation);

            Debug.WriteLine("Decomposed matrix");
            Debug.WriteLine("--------------------");
            Debug.WriteLine("Scale");
            Debug.WriteLine($"{scale.X} {scale.Y} {scale.Z}");
            Debug.WriteLine("Translate");
            Debug.WriteLine($"{translation.X} {translation.Y} {translation.Z}");
            Debug.WriteLine("Rotation");
            Debug.WriteLine($"{rotation.X} {rotation.Y} {rotation.Z}");
            Debug.WriteLine("--------------------");
        }
    }
}
