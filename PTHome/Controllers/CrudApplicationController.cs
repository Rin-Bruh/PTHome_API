using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PTHome.CommonLayer.Models.House;
using PTHome.CommonLayer.Models.User;
using PTHome.ServiceLayer;

namespace PTHome.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class CrudApplicationController : ControllerBase
    {
        public readonly ICrudApplicationSL _crudApplicationSL;
        public readonly ILogger<CrudApplicationController> _logger;

        public CrudApplicationController(ICrudApplicationSL crudApplicationSL, ILogger<CrudApplicationController> logger)
        {
            _crudApplicationSL = crudApplicationSL;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> AddHouse(AddHouseRequest request)
        {
            AddHouseResponse response = new AddHouseResponse();
            _logger.LogInformation($"AddHouse Api Calling {JsonConvert.SerializeObject(request)}");
            try
            {
                response = await _crudApplicationSL.AddHouse(request);
                if (!response.IsSuccess)
                {
                    return BadRequest(new { IsSuccess = response.IsSuccess, Message = response.Message });
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                _logger.LogError($"AddHouse Controller Error => {ex.Message}");
                return BadRequest(new { IsSuccess = response.IsSuccess, Message = ex.Message });
            }
            return Ok(new { IsSuccess = response.IsSuccess, Message = response.Message });
        }

        [HttpGet]
        public async Task<IActionResult> ReadAllHouse()
        {
            ReadAllHouseResponse response = new ReadAllHouseResponse();
            _logger.LogInformation($"ReadAllHouse Api Calling ");
            try
            {
                response = await _crudApplicationSL.ReadAllHouse();
                if (!response.IsSuccess)
                {
                    return BadRequest(new { IsSuccess = response.IsSuccess, Message = response.Message, Data = response.readAllHouse});
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                _logger.LogError($"ReadAllHouse Controller Error => {ex.Message}");
                return BadRequest(new { IsSuccess = response.IsSuccess, Message = ex.Message });
            }
            return Ok(new { IsSuccess = response.IsSuccess, Message = response.Message, Data = response.readAllHouse });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAllHouseById(UpdateAllHouseByIdRequest request)
        {
            UpdateAllHouseByIdResponse response = new UpdateAllHouseByIdResponse();
            _logger.LogInformation($"UpdateAllHouseById Api Calling {JsonConvert.SerializeObject(request)}");
            try
            {
                response = await _crudApplicationSL.UpdateAllHouseById(request);
                if (!response.IsSuccess)
                {
                    return BadRequest(new { IsSuccess = response.IsSuccess, Message = response.Message });
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                _logger.LogError($"UpdateAllHouseById Controller Error => {ex.Message}");
                return BadRequest(new { IsSuccess = response.IsSuccess, Message = ex.Message });
            }
            return Ok(new { IsSuccess = response.IsSuccess, Message = response.Message });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteHouseById(DeleteHouseByIdRequest request)
        {
            DeleteHouseByIdResponse response = new DeleteHouseByIdResponse();
            _logger.LogInformation($"DeleteHouseById Api Calling ..");
            try
            {
                response = await _crudApplicationSL.DeleteHouseById(request);
                if (!response.IsSuccess)
                {
                    return BadRequest(new { IsSuccess = response.IsSuccess, Message = response.Message });
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                _logger.LogError($"DeleteHouseById Controller Error => {ex.Message}");
                return BadRequest(new { IsSuccess = response.IsSuccess, Message = ex.Message });
            }
            return Ok(new { IsSuccess = response.IsSuccess, Message = response.Message });
        }

        [HttpPost]
        public async Task<IActionResult> ReadHouseById(ReadHouseByIdRequest request)
        {
            ReadHouseByIdResponse response = new ReadHouseByIdResponse();
            _logger.LogInformation($"ReadHouseById Api Calling {JsonConvert.SerializeObject(request)}");
            try
            {
                response = await _crudApplicationSL.ReadHouseById(request);
                if (!response.IsSuccess)
                {
                    return BadRequest(new { IsSuccess = response.IsSuccess, Message = response.Message });
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                _logger.LogError($"ReadHouseById Controller Error => {ex.Message}");
                return BadRequest(new { IsSuccess = response.IsSuccess, Message = ex.Message });
            }
            return Ok(new { IsSuccess = response.IsSuccess, Message = response.Message, data=response.Data });
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserRequest request)
        {
            AddUserResponse response = new AddUserResponse();
            _logger.LogInformation($"AddUser Api Calling {JsonConvert.SerializeObject(request)}");
            try
            {
                response = await _crudApplicationSL.AddUser(request);
                if (!response.IsSuccess)
                {
                    return BadRequest(new { IsSuccess = response.IsSuccess, Message = response.Message });
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                _logger.LogError($"AddUser Controller Error => {ex.Message}");
                return BadRequest(new { IsSuccess = response.IsSuccess, Message = ex.Message });
            }
            return Ok(new { IsSuccess = response.IsSuccess, Message = response.Message });
        }
    }
}
