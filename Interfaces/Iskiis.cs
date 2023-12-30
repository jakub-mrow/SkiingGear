using Core;


namespace Interfaces
{
    public interface ISkiis
    {
        int Id {  get; set; }
        string Model { get; set; }
        ISkiBrand Brand { get; set; }
        SkiType Type { get; set; }
        int Length { get; set; }
        int Price { get; set; }

    }
}
