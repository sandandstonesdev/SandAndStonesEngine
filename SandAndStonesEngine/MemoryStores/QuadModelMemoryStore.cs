using SandAndStonesEngine.DataModels.Quads;

namespace SandAndStonesEngine.MemoryStore
{
    public class QuadModelMemoryStore
    {
        public List<IQuadModel> Models { get; } = [];
        public List<IQuadModel> StatusBarModels { get; } = [];

        public QuadModelMemoryStore()
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
