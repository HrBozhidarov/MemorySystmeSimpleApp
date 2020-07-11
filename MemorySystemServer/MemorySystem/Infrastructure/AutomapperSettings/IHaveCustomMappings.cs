namespace MemorySystem.Infrastructure.AutomapperSettings
{
    using AutoMapper;

    public interface IHaveCustomMappings
    {
        void CreateMappings(IProfileExpression configuration);
    }
}
