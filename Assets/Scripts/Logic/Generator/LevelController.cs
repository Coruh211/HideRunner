using Infrastructure.Services;
using Logic.Input;
using Logic.NavMesh;
using Logic.Player;

namespace Logic.Generator
{
    public class LevelController : ILevelControllerService
    {
        private readonly ILevelConstructorService _levelConstructorService;
        private readonly IModelConstructor _modelConstructor;

        public LevelController(ILevelConstructorService levelConstructorService, IModelConstructor modelConstructor)
        {
            _levelConstructorService = levelConstructorService;
            _modelConstructor = modelConstructor;
        }
        
        public void ConstructLevel()
        {
            _levelConstructorService.Construct();
            NavMeshBaker.Instance.Init();
            _modelConstructor.Construct();
            EventManager.OnEndGame.Subscribe(ClearLevel);
        }

        public void ConstructNewLevel()
        {
            _levelConstructorService.Construct();
            NavMeshBaker.Instance.Init();
            _modelConstructor.Construct();
        }
        
        private void ClearLevel(EndGameStatus obj)
        {
            _modelConstructor.ClearLevel();
            _levelConstructorService.ClearLevel();
            AllServices.Container.Single<INoiseController>().ResetNoise();
            GlobalInputState.Instance.ChangeInputState();
        }

        
    }
}