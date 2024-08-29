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
    }
}
