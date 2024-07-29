using SandAndStonesEngine.DataModels.Quads;

namespace SandAndStonesEngine.Managers
{
    public class QuadModelManager
    {
        private static readonly Lazy<QuadModelManager> lazyInstance = new Lazy<QuadModelManager>(() => new QuadModelManager());
        public static QuadModelManager Instance => lazyInstance.Value;

        public List<IQuadModel> Models { get; } = new();
        public List<IQuadModel> StatusBarModels { get; } = new();

        public QuadModelManager()
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
