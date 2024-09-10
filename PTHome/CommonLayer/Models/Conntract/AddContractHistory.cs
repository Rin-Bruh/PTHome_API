using System.ComponentModel.DataAnnotations;

namespace PTHome.CommonLayer.Models.Contract
{
    public class AddContractHistoryRequest
    {
        public int HistoryId { get; set; }

        public string ContractId { get; set; }

        [Required]
        public int Price { get; set; }

        public int Status { get; set; }

        [Required]
        public string RenterId { get; set; }

        [Required]
        public string OwnerId { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public DateTime ExpiredTime { get; set; }

        public int PeopleApplied { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime UpdatedTime { get; set; }


    }
    public class AddContractHistoryResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
