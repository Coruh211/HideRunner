using Infrastructure.Services;

namespace Logic.Generator
{
    public interface ILevelControllerService: IService
    {
        public void ConstructLevel();
        public void ConstructNewLevel();
    }
}