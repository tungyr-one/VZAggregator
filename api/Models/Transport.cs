using System.ComponentModel.DataAnnotations;

namespace VZAggregator.Entities
{
    public class Transport
    {
        [Key]
        public int TransportId { get; set; }
        public string Name { get; set; }
        public string NumberPlate { get; set; }
        public int PassengersCapacity { get; set; }
    }
}