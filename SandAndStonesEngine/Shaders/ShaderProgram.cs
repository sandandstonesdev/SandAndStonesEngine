using System.Text;
using Veldrid;

namespace SandAndStonesEngine.Shaders
{
    internal class ShaderProgram
    {
        private const string entryPoint = "main";
        private string name;
        private ShaderStages stage;

        ShaderDescription shaderDesc;
        public ShaderDescription ShaderDesc
        {
            get { return shaderDesc; }
        }
        public ShaderProgram(string name, ShaderStages stage)
        {
            this.name = name;
            this.stage = stage;
        }

        public void Init()
        {
            string code = ReadShaderCode(name);
            byte[] shaderCodeBytes = Encoding.UTF8.GetBytes(code);
            shaderDesc = new(
                stage,
                shaderCodeBytes,
                entryPoint);
        }

        public void Destroy()
        {

        }

        public string ReadShaderCode(string shaderName)
        {
            const string shaderProgramsDir = "Shaders\\ShaderPrograms";
            string basePath = Path.GetFullPath(@".");
            string shadersPath = Path.Combine(basePath, shaderProgramsDir, shaderName);
            string shaderCode = File.ReadAllText(shadersPath, Encoding.ASCII);
            return shaderCode;
        }
    }
}
