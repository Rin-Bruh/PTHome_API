using System.ComponentModel.DataAnnotations;

namespace PTHome.CommonLayer.Models.House
{
    public class DeleteHouseByIdRequest
    {
        [Required(ErrorMessage = "HouseId Is Mandetory Field")]
        public string HouseId { get; set; }
        
    }
    public class DeleteHouseByIdResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
