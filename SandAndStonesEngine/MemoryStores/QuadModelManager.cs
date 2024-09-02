using SandAndStonesEngine.DataModels.Quads;

namespace SandAndStonesEngine.Managers
{
    public class QuadModelStore
    {
        public List<IQuadModel> Models { get; } = [];
        public List<IQuadModel> StatusBarModels { get; } = [];

        public QuadModelStore()
        {

        }

        public void AddRange(List<IQuadModel> models)
        {
            Models.AddRange(models);
        }

        public void AddStatusBarModelsRange(List<IQuadModel> models)
        {
            StatusBarModels.AddRange(models);
        }
    }
}
