using System.ComponentModel.DataAnnotations;

namespace PTHome.CommonLayer.Models.House
{
    public class ReadAllHouseResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public List<GetReadAllHouse> readAllHouse { get; set; }
    }

    public class GetReadAllHouse
    {
        public string HouseId { get; set; }
        
        public string Name { get; set; }
        
        public string WardId { get; set; }
        
        public string Address { get; set; }

        public int Status { get; set; }

        public string Image { get; set; }

        public double Longitude { get; set; }

        public double Lastitude { get; set; }
        
        public string HouseTypeId { get; set; }
        
        public string OwnerId { get; set; }
        
        public string CertificateOfOwnership { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdatedTime { get; set; }

        public DateTime DisableTime { get; set; }


    }
}
