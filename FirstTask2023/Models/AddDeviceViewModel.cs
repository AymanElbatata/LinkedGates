using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FirstTask2023.DAL.Models;

namespace FirstTask2023.Models
{
    public class AddDeviceViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string SerialNo { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime? AcquisitionDate { get; set; }
        [StringLength(50)]
        public string Memo { get; set; }
        public int? DeviceCategoryId { get; set; }

        public int? CurrentUserId { get; set; }
        public int? BrandId { get; set; }
        public List<DevicePropertyDetailViewModel> DevicePropertyDetailViewModel { get; set; }
    }
}
