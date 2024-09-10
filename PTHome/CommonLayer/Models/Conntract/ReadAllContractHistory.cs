namespace PTHome.CommonLayer.Models.Conntract
{
    using System.ComponentModel.DataAnnotations;

    namespace PTHome.CommonLayer.Models.Conntract
    {
        public class ReadAllContractHistoryResponse
        {
            public bool IsSuccess { get; set; }
            public string Message { get; set; }

            public List<GetReadAllContractHistory> readAllContractHistory { get; set; }
        }

        public class GetReadAllContractHistory
        {
            public int HistoryId { get; set; }

            public string ContractId { get; set; }

            public int Price { get; set; }

            public int Status { get; set; }

            public string RenterId { get; set; }

            public string OwnerId { get; set; }

            public string Description { get; set; }

            public string Image { get; set; }

            public DateTime ExpiredTime { get; set; }

            public int PeopleApplied { get; set; }

            public string UpdatedBy { get; set; }

            public DateTime UpdatedTime { get; set; }


        }
    }

}
