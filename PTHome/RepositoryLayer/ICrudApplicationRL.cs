using PTHome.CommonLayer.Models.Conntract.PTHome.CommonLayer.Models.Conntract;
using PTHome.CommonLayer.Models.Contract;
using PTHome.CommonLayer.Models.House;
using PTHome.CommonLayer.Models.User;

namespace PTHome.RepositoryLayer
{
    public interface ICrudApplicationRL
    {
        public Task<AddHouseResponse> AddHouse(AddHouseRequest request); 
        public Task<ReadAllHouseResponse> ReadAllHouse();
        public Task<UpdateAllHouseByIdResponse> UpdateAllHouseById(UpdateAllHouseByIdRequest request);
        public Task<DeleteHouseByIdResponse> DeleteHouseById(DeleteHouseByIdRequest request);
        public Task<ReadHouseByIdResponse> ReadHouseById(ReadHouseByIdRequest request);
        public Task<AddUserResponse> AddUser(AddUserRequest request);
        public Task<ReadAllUserResponse> ReadAllUser();
        public Task<UpdateAllUserByIdResponse> UpdateAllUserById(UpdateAllUserByIdRequest request);
        public Task<DeleteUserByIdResponse> DeleteUserById(DeleteUserByIdRequest request);
        public Task<ReadUserByIdResponse> ReadUserById(ReadUserByIdRequest request);
        public Task<AddContractResponse> AddContract(AddContractRequest request);
        public Task<ReadAllContractResponse> ReadAllContract();
        public Task<AddContractHistoryResponse> AddContractHistory(AddContractHistoryRequest request);
        public Task<ReadAllContractHistoryResponse> ReadAllContractHistory();
    }
}
