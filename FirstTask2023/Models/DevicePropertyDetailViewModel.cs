using System.ComponentModel.DataAnnotations;

namespace FirstTask2023.Models
{
    public class DevicePropertyDetailViewModel
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public int DeviceId { get; set; }
        public string PropName { get; set; }
        [StringLength(50)]
        public string Value { get; set; }
    }
}
