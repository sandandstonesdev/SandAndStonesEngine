using SandAndStonesEngine.DataModels.ScreenDivisions;
using System.Numerics;

namespace SandAndStonesEngine.DataModels.Quads
{
    public static class QuadGridCalculator
    {
        private static readonly Vector3 relativePosition = new(-1.00f, -1.00f, 0.0f);

        private static Vector3 GetAbsoluteCoord(ScreenDivisionForQuads screenDivisions, Vector3 quadGridPoint, float quadScale)
        {
            var quadSizeInCoord = GetScaledQuadSizeInCoord(screenDivisions, quadScale);
            var scaledPoint = Vector3.Multiply(quadGridPoint, quadSizeInCoord);
            var res = relativePosition + scaledPoint;
            return res;
        }

        private static Vector3 GetScaledQuadSizeInCoord(ScreenDivisionForQuads screenDivisions, float quadScale)
        {

            Vector3 quadSizeTemp = screenDivisions.GetCoordinateUnitsPerQuad();
            var quadSizeInCoord = Vector3.Multiply(quadSizeTemp, new Vector3(quadScale, quadScale, 1));
            return quadSizeInCoord;
        }

        public static Vector3[] GetQuadAbsoluteCoords(ScreenDivisionForQuads screenDivisions, Vector3[] quadPointsInGrid, float scale)
        {
            var leftUpper = GetAbsoluteCoord(screenDivisions, quadPointsInGrid[0], scale);
            var leftDown = GetAbsoluteCoord(screenDivisions, quadPointsInGrid[1], scale);
            var rightUpper = GetAbsoluteCoord(screenDivisions, quadPointsInGrid[2], scale);
            var rightDown = GetAbsoluteCoord(screenDivisions, quadPointsInGrid[3], scale);

            var quadAbsoluteCoords = new Vector3[4]
            {
                leftDown, leftUpper, rightDown, rightUpper
            };

            return quadAbsoluteCoords;
        }
    }
}
