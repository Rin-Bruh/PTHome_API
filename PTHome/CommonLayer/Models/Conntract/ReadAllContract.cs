namespace PTHome.CommonLayer.Models.Conntract
{
    using System.ComponentModel.DataAnnotations;

    namespace PTHome.CommonLayer.Models.Conntract
    {
        public class ReadAllContractResponse
        {
            public bool IsSuccess { get; set; }
            public string Message { get; set; }

            public List<GetReadAllContract> readAllContract { get; set; }
        }

        public class GetReadAllContract
        {
            public string ContractId { get; set; }

            public string RentEntityId { get; set; }

            public DateTime DateSigned { get; set; }

            public int Status { get; set; }

            public DateTime StartRentDate { get; set; }

            public DateTime CreatedTime { get; set; }

            public DateTime UpdateTime { get; set; }


        }
    }

}
