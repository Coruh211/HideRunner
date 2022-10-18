using Infrastructure.Services;

namespace Logic.Generator
{
    public interface IModelConstructor: IService
    {
        public void Construct();

        public void ClearLevel();
    }
}