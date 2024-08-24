using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SandAndStonesEngine.Assets.Batches;
using SandAndStonesEngine.DataModels;
using SandAndStonesEngine.DataModels.Quads;
using SandAndStonesEngine.DataModels.ScreenDivisions;
using SandAndStonesEngine.GameCamera;
using SandAndStonesEngine.GameInput;
using SandAndStonesEngine.GameTextures;
using SandAndStonesEngine.GraphicAbstractions;
using SandAndStonesEngine.MathModule;
using SandAndStonesEngine.RenderingAbstractions;
using SandAndStonesEngine.Shaders;
using System.Numerics;

namespace SandAndStonesEngine.GameFactories
{
    public class Startup
    {
        public readonly static IServiceProvider ServiceProvider = BuildProvider();

        private static ServiceProvider BuildProvider()
        {
            var serviceCollection = new ServiceCollection();
            
            var dir = Directory.GetCurrentDirectory();

            var configurationRoot = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            serviceCollection.AddSingleton<IConfiguration>(configurationRoot);

            ConfigureServices(serviceCollection);

            return serviceCollection.BuildServiceProvider();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            int x = 50;
            int y = 50;
            int screenWidth = 800;
            int screenHeight = 800;
            int quadCountX = 8;
            int quadCountY = 8;
            int quadCountZ = 8;

            var screenDivision = new ScreenDivisionForQuads(screenWidth, screenHeight, quadCountX, quadCountY, quadCountZ);
            services.AddSingleton(
                new ScreenDivisionForQuads(screenWidth, screenHeight, quadCountX, quadCountY, quadCountZ));
            var gridManager = new QuadGridManager(screenDivision);
            services.AddSingleton(gridManager);
            var gameWindow = new GameWindow();
            gameWindow.Start(x, y, screenWidth, screenHeight, "Sand and Stones Engine Test");
            services.AddSingleton(gameWindow);

            GameGraphicDevice gameGraphicDevice = new GameGraphicDevice(gameWindow);
            gameGraphicDevice.Init();
            services.AddSingleton(gameGraphicDevice);

            var inputDevicesState = new InputDevicesState(new Vector2(gameWindow.SDLWindow.Bounds.X, gameWindow.SDLWindow.Bounds.Y));
            services.AddSingleton(inputDevicesState);

            var inputMotionMapper = new CameraInputMotionMapper(inputDevicesState);
            services.AddSingleton(inputMotionMapper);
            var scrollableViewport = new ScrollableViewport(0, 0, screenWidth, screenHeight);
            services.AddSingleton(scrollableViewport);

            var transformatorData = new TransformatorData(new Vector3(0, 0, 1.0f), new Vector3(0, 0, -1), new Vector3(0, 1, 0), new Vector3(0, 0, 0), 0.03f, 1.0f);
            services.AddSingleton(transformatorData);

            var viewTransformator = new ViewTransformator(scrollableViewport, inputMotionMapper, transformatorData);
            var worldTransformator = new WorldTransformator(inputMotionMapper, transformatorData);
            var matrices = new Matrices(gameGraphicDevice, worldTransformator, viewTransformator);
            matrices.Init();

            services.AddSingleton(worldTransformator);
            services.AddSingleton(viewTransformator);
            services.AddSingleton(matrices);

            services.AddSingleton(new Camera(gameWindow, matrices, scrollableViewport));

            var assetBatchList = new List<GameAssetBatchBase>()
            {
                new GameAssetBatch(gameGraphicDevice, gridManager, viewTransformator, scrollableViewport),
                new GameStatusBarAssetBatch(gameGraphicDevice, gridManager, scrollableViewport)
            };

            assetBatchList.ForEach(e => e.Init(gameGraphicDevice, scrollableViewport));

            var shaderSet = new GameShaderSet(gameGraphicDevice, assetBatchList[0], matrices);
            shaderSet.Init();
            services.AddSingleton(shaderSet);

            var gameTextureSurface = new GameTextureSurface(gameGraphicDevice, 256, 256);
            gameTextureSurface.Init();
            services.AddSingleton(gameTextureSurface);

            var pipelineList = new List<PipelineBase>()
            {
                new GamePipeline(gameGraphicDevice, shaderSet, gameTextureSurface, matrices),
                new StatusBarPipeline(gameGraphicDevice, shaderSet, gameTextureSurface, matrices)
            };
            pipelineList.ForEach(e => e.Init());

            var gameCommandList = new GameCommandList(gameGraphicDevice, matrices, gameTextureSurface, assetBatchList, pipelineList);
            gameCommandList.Init();

            services.AddSingleton(gameCommandList);
        }
    }
}
