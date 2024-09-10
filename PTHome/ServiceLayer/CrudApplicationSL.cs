using PTHome.CommonLayer.Models.Conntract.PTHome.CommonLayer.Models.Conntract;
using PTHome.CommonLayer.Models.Contract;
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

        public async Task<ReadAllUserResponse> ReadAllUser()
        {
            _logger.LogInformation("ReadAllUser Method calling in Service Layer");
            return await _crudApplicationRL.ReadAllUser();
        }

        public async Task<UpdateAllUserByIdResponse> UpdateAllUserById(UpdateAllUserByIdRequest request)
        {
            _logger.LogInformation("UpdateAllUserById Method calling in Service Layer");
            return await _crudApplicationRL.UpdateAllUserById(request);
        }

        public async Task<DeleteUserByIdResponse> DeleteUserById(DeleteUserByIdRequest request)
        {
            _logger.LogInformation("DeleteUserById Method calling in Service Layer");
            return await _crudApplicationRL.DeleteUserById(request);
        }

        public async Task<ReadUserByIdResponse> ReadUserById(ReadUserByIdRequest request)
        {
            _logger.LogInformation("ReadUserById Method calling in Service Layer");
            return await _crudApplicationRL.ReadUserById(request);
        }

        public async Task<AddContractResponse> AddContract(AddContractRequest request)
        {
            _logger.LogInformation("AddContract Method calling in Service Layer");
            return await _crudApplicationRL.AddContract(request);
        }

        public async Task<ReadAllContractResponse> ReadAllContract()
        {
            _logger.LogInformation("ReadAllContract Method calling in Service Layer");
            return await _crudApplicationRL.ReadAllContract();
        }

        public async Task<AddContractHistoryResponse> AddContractHistory(AddContractHistoryRequest request)
        {
            _logger.LogInformation("AddContractHistory Method calling in Service Layer");
            return await _crudApplicationRL.AddContractHistory(request);
        }

        public async Task<ReadAllContractHistoryResponse> ReadAllContractHistory()
        {
            _logger.LogInformation("ReadAllContractHistory Method calling in Service Layer");
            return await _crudApplicationRL.ReadAllContractHistory();
        }
    }
}
