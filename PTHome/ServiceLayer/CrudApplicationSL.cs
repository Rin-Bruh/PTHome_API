using PTHome.CommonLayer.Models.House;
using PTHome.CommonLayer.Models.User;
using PTHome.RepositoryLayer;

namespace PTHome.ServiceLayer
{
    public class CrudApplicationSL : ICrudApplicationSL
    {
        public readonly ICrudApplicationRL _crudApplicationRL;
        public readonly ILogger<CrudApplicationSL> _logger;
        //public readonly string EmailRegex = @"^[0-9a-zA-Z]+([._+-][0-9a-zA-Z]+)*@[0-9a-zA-Z]+.[a-zA-Z]{2,4}([.][a-zA-Z]{2,3})?$";
        //public readonly string MobileRegex = @"([1-9]{1}[0-9]{9})$";
        //public readonly string GenderRegex = @"^(?:m|male|f|female)$";
        public CrudApplicationSL(ICrudApplicationRL crudApplicationRL, ILogger<CrudApplicationSL> logger)
        {
            _crudApplicationRL = crudApplicationRL;
            _logger = logger;
        }

        public async Task<AddHouseResponse> AddHouse(AddHouseRequest request)
        {
            _logger.LogInformation("AddHouse Method calling in Service Layer");
            return await _crudApplicationRL.AddHouse(request);
        }
        public async Task<ReadAllHouseResponse> ReadAllHouse()
        {
            _logger.LogInformation("ReadAllHouse Method calling in Service Layer");
            return await _crudApplicationRL.ReadAllHouse();
        }
        public async Task<UpdateAllHouseByIdResponse> UpdateAllHouseById(UpdateAllHouseByIdRequest request)
        {
            _logger.LogInformation("UpdateAllHouseById Method calling in Service Layer");
            return await _crudApplicationRL.UpdateAllHouseById(request);
        }
        public async Task<DeleteHouseByIdResponse> DeleteHouseById(DeleteHouseByIdRequest request)
        {
            _logger.LogInformation("DeleteHouseById Method calling in Service Layer");
            return await _crudApplicationRL.DeleteHouseById(request);
        }
        public async Task<ReadHouseByIdResponse> ReadHouseById(ReadHouseByIdRequest request)
        {
            _logger.LogInformation("ReadHouseById Method calling in Service Layer");
            return await _crudApplicationRL.ReadHouseById(request);
        }

        public async Task<AddUserResponse> AddUser(AddUserRequest request)
        {
            _logger.LogInformation("AddUser Method calling in Service Layer");
            return await _crudApplicationRL.AddUser(request);
        }

        
    }
}
