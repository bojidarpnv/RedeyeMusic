using AutoMapper;

namespace RedeyeMusic.Services.Mapping
{
    public interface IHaveCustomMappings
    {

        void CreateMappings(IProfileExpression configuration);

    }
}
