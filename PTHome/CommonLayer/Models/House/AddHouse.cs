using System.ComponentModel.DataAnnotations;

namespace PTHome.CommonLayer.Models.House
{
    public class AddHouseRequest
    {
        public string HouseId { get; set; }
        [Required(ErrorMessage = "Name Is Mandetory Field")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Ward Is Mandetory Field")]
        public string WardId { get; set; }
        [Required(ErrorMessage = "Address Is Mandetory Field")]
        public string Address { get; set; }
        public int Status { get; set; }
        public string Image { get; set; }
        public double Longitude { get; set; }
        public double Lastitude { get; set; }
        [Required(ErrorMessage = "HouseType Is Mandetory Field")]
        public string HouseTypeId { get; set; }
        [Required(ErrorMessage = "Owner Is Mandetory Field")]
        public string OwnerId { get; set; }
        [Required(ErrorMessage = "CertificateOfOwnership Is Mandetory Field")]
        public string CertificateOfOwnership { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdatedTime { get; set; }
        public DateTime DisableTime { get; set; }


    }
    public class AddHouseResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
