using SandAndStonesEngine.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SandAndStonesEngine.GameCamera
{
    public class QuadInputMotionMapper : InputMotionMapperBase
    {
        public QuadInputMotionMapper()
        {
        }
        public override Vector3 GetMotionDir()
        {
            throw new NotImplementedException();
        }

        public override Vector3 GetRotatedMotionDir(float yaw, float pitch)
        {
            throw new NotImplementedException();
        }

        public override Vector2 GetYawPitchVector()
        {
            throw new NotImplementedException();
        }
    }
}
