using System.ComponentModel.DataAnnotations;

namespace PTHome.CommonLayer.Models.Contract
{
    public class AddContractRequest
    {
        public string ContractId { get; set; }

        public string RentEntityId { get; set; }

        [Required]
        public DateTime DateSigned { get; set; }

        public int Status { get; set; }

        public DateTime StartRentDate { get; set; }

        public DateTime CreatedTime { get; set; }

        public DateTime UpdateTime { get; set; }


    }
    public class AddContractResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
