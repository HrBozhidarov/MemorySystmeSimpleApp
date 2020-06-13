namespace MemorySystemApp.Infrastructures.AutomapperSettings
{
    using AutoMapper;

    public interface IHaveCustomMappings
    {
        void CreateMappings(IProfileExpression configuration);
    }
}
