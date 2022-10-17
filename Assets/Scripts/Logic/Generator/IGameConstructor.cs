using Infrastructure.Services;

namespace Logic.Generator
{
    public interface IGameConstructor: IService
    {
        public void Construct();
    }
}