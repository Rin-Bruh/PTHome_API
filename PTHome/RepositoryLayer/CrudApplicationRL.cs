using MySqlConnector;
using PTHome.Common_Utility;
using PTHome.CommonLayer.Models.House;
using PTHome.CommonLayer.Models.User;

namespace PTHome.RepositoryLayer
{
    public class CrudApplicationRL : ICrudApplicationRL
    {
        public readonly IConfiguration _configuration;
        public readonly ILogger<CrudApplicationRL> _logger;
        public readonly MySqlConnection _mySqlConnection;
        public CrudApplicationRL(IConfiguration configuration, ILogger<CrudApplicationRL> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _mySqlConnection = new MySqlConnection(_configuration["ConnectionStrings:MySqlDBConnection"]);
        }

        public async Task<AddHouseResponse> AddHouse(AddHouseRequest request)
        {
            _logger.LogInformation("AddHouse Method calling in Repository Layer");
            AddHouseResponse response = new AddHouseResponse();
            response.IsSuccess = true;
            response.Message = "Success";
            try
            {
                if (_mySqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _mySqlConnection.OpenAsync();
                }

                using (MySqlCommand sqlCommand = new MySqlCommand(SqlQueries.AddHouse, _mySqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@HouseId", request.HouseId);
                    sqlCommand.Parameters.AddWithValue("@Name", request.Name);
                    sqlCommand.Parameters.AddWithValue("@Address", request.Address);
                    sqlCommand.Parameters.AddWithValue("@WardId", request.WardId);
                    sqlCommand.Parameters.AddWithValue("@Image", request.Image);
                    sqlCommand.Parameters.AddWithValue("@Longitude", request.Longitude);
                    sqlCommand.Parameters.AddWithValue("@Lastitude", request.Lastitude);
                    sqlCommand.Parameters.AddWithValue("@HouseTypeId", request.HouseTypeId);
                    sqlCommand.Parameters.AddWithValue("@OwnerId", request.OwnerId);
                    sqlCommand.Parameters.AddWithValue("@CertificateOfOwnership", request.CertificateOfOwnership);
                    sqlCommand.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                    sqlCommand.Parameters.AddWithValue("@CreateTime", request.CreateTime);
                    sqlCommand.Parameters.AddWithValue("@UpdatedTime", request.UpdatedTime);
                    sqlCommand.Parameters.AddWithValue("@DisableTime", request.DisableTime);
                    int Status = await sqlCommand.ExecuteNonQueryAsync();
                    if (Status <= 0)
                    {
                        response.IsSuccess = false;
                        response.Message = "AddHouse Query Not Executed";
                        _logger.LogError("AddHouse Query Not Executed");
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                _logger.LogError($"AddHouse Repository Layer Error : {ex.Message}");
            }
            finally
            {
                await _mySqlConnection.CloseAsync();
                await _mySqlConnection.DisposeAsync();

            }
            return response;
        }

        public async Task<ReadAllHouseResponse> ReadAllHouse()
        {
            _logger.LogInformation("ReadAllInformation Repository Layer Calling");
            ReadAllHouseResponse response = new ReadAllHouseResponse();
            response.IsSuccess = true;
            response.Message = "Successful";
            try
            {
                if (_mySqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _mySqlConnection.OpenAsync();
                }
                using (MySqlCommand sqlCommand = new MySqlCommand(SqlQueries.ReadAllHouse, _mySqlConnection))
                {
                    try
                    {
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlCommand.CommandTimeout = 180;
                        using (MySqlDataReader dataReader = await sqlCommand.ExecuteReaderAsync())
                        {
                            if (dataReader.HasRows)
                            {
                                response.readAllHouse = new List<GetReadAllHouse>();
                                while (await dataReader.ReadAsync())
                                {
                                    GetReadAllHouse getData = new GetReadAllHouse();
                                    getData.HouseId = dataReader["HouseId"] != DBNull.Value ? Convert.ToString(dataReader["HouseId"]) : string.Empty;
                                    getData.Name = dataReader["Name"] != DBNull.Value ? Convert.ToString(dataReader["Name"]) : string.Empty;
                                    getData.WardId = dataReader["WardId"] != DBNull.Value ? Convert.ToString(dataReader["WardId"]) : string.Empty;
                                    getData.Address = dataReader["Address"] != DBNull.Value ? Convert.ToString(dataReader["Address"]) : string.Empty;
                                    getData.Status = dataReader["Status"] != DBNull.Value ? Convert.ToInt32(dataReader["Status"]) : 0;
                                    getData.Image = dataReader["Image"] != DBNull.Value ? Convert.ToString(dataReader["Image"]) : string.Empty;
                                    getData.Longitude = dataReader["Longitude"] != DBNull.Value ? Convert.ToDouble(dataReader["Longitude"]) : 0;
                                    getData.Lastitude = dataReader["Lastitude"] != DBNull.Value ? Convert.ToDouble(dataReader["Lastitude"]) : 0;
                                    getData.HouseTypeId = dataReader["HouseTypeId"] != DBNull.Value ? Convert.ToString(dataReader["HouseTypeId"]) : string.Empty;
                                    getData.OwnerId = dataReader["OwnerId"] != DBNull.Value ? Convert.ToString(dataReader["OwnerId"]) : string.Empty;
                                    getData.CertificateOfOwnership = dataReader["CertificateOfOwnership"] != DBNull.Value ? Convert.ToString(dataReader["CertificateOfOwnership"]) : string.Empty;
                                    getData.CreatedBy = dataReader["CreatedBy"] != DBNull.Value ? Convert.ToString(dataReader["CreatedBy"]) : string.Empty;
                                    getData.CreateTime = dataReader["CreateTime"] != DBNull.Value ? Convert.ToDateTime(dataReader["CreateTime"]) : DateTime.MinValue;
                                    getData.UpdatedTime = dataReader["UpdatedTime"] != DBNull.Value ? Convert.ToDateTime(dataReader["UpdatedTime"]) : DateTime.MinValue;
                                    getData.DisableTime = dataReader["DisableTime"] != DBNull.Value ? Convert.ToDateTime(dataReader["DisableTime"]) : DateTime.MinValue;

                                    response.readAllHouse.Add(getData);
                                }
                            }
                            else
                            {
                                response.IsSuccess = true;
                                response.Message = "No Record At DataBase";
                                _logger.LogWarning("No Record At DataBase");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        response.IsSuccess = false;
                        response.Message = ex.Message;
                        _logger.LogError($"ReadAllInformation Repository Layer Error : {ex.Message}");
                    }
                    finally
                    {
                        await sqlCommand.DisposeAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                _logger.LogError($"ReadAllInformation Repository Layer Error : {ex.Message}");
            }
            finally
            {
                await _mySqlConnection.CloseAsync();
                await _mySqlConnection.DisposeAsync();
            }
            return response;
        }

        public async Task<UpdateAllHouseByIdResponse> UpdateAllHouseById(UpdateAllHouseByIdRequest request)
        {
            _logger.LogInformation("UpdateAllHouseById Method calling in Repository Layer");
            UpdateAllHouseByIdResponse response = new UpdateAllHouseByIdResponse();
            response.IsSuccess = true;
            response.Message = "Success";
            try
            {
                if (_mySqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _mySqlConnection.OpenAsync();
                }

                using (MySqlCommand sqlCommand = new MySqlCommand(SqlQueries.UpdateAllHouseById, _mySqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@HouseId", request.HouseId);
                    sqlCommand.Parameters.AddWithValue("@Name", request.Name);
                    sqlCommand.Parameters.AddWithValue("@Address", request.Address);
                    sqlCommand.Parameters.AddWithValue("@WardId", request.WardId);
                    sqlCommand.Parameters.AddWithValue("@Image", request.Image);
                    sqlCommand.Parameters.AddWithValue("@Longitude", request.Longitude);
                    sqlCommand.Parameters.AddWithValue("@Lastitude", request.Lastitude);
                    sqlCommand.Parameters.AddWithValue("@HouseTypeId", request.HouseTypeId);
                    sqlCommand.Parameters.AddWithValue("@OwnerId", request.OwnerId);
                    sqlCommand.Parameters.AddWithValue("@CertificateOfOwnership", request.CertificateOfOwnership);
                    sqlCommand.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                    //sqlCommand.Parameters.AddWithValue("@CreateTime", request.CreateTime);
                    sqlCommand.Parameters.AddWithValue("@UpdatedTime", request.UpdatedTime);
                    sqlCommand.Parameters.AddWithValue("@DisableTime", request.DisableTime);
                    int Status = await sqlCommand.ExecuteNonQueryAsync();
                    if (Status <= 0)
                    {
                        response.IsSuccess = false;
                        response.Message = "UpdateAllHouseById Query Not Executed";
                        _logger.LogError("UpdateAllHouseById Query Not Executed");
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                _logger.LogError($"UpdateAllHouseById Repository Layer Error : {ex.Message}");
            }
            finally
            {
                await _mySqlConnection.CloseAsync();
                await _mySqlConnection.DisposeAsync();

            }
            return response;
        }

        public async Task<DeleteHouseByIdResponse> DeleteHouseById(DeleteHouseByIdRequest request)
        {
            _logger.LogInformation("DeleteHouseById Method calling in Repository Layer");
            DeleteHouseByIdResponse response = new DeleteHouseByIdResponse();
            response.IsSuccess = true;
            response.Message = "Success";
            try
            {
                if (_mySqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _mySqlConnection.OpenAsync();
                }

                using (MySqlCommand sqlCommand = new MySqlCommand(SqlQueries.DeleteHouseById, _mySqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@HouseId", request.HouseId);                   
                    int Status = await sqlCommand.ExecuteNonQueryAsync();
                    if (Status <= 0)
                    {
                        response.IsSuccess = false;
                        response.Message = "DeleteHouseById Query Not Executed";
                        _logger.LogError("DeleteHouseById Query Not Executed");
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                _logger.LogError($"DeleteHouseById Repository Layer Error : {ex.Message}");
            }
            finally
            {
                await _mySqlConnection.CloseAsync();
                await _mySqlConnection.DisposeAsync();

            }
            return response;
        }

        public async Task<ReadHouseByIdResponse> ReadHouseById(ReadHouseByIdRequest request)
        {
            _logger.LogInformation("ReadHouseById Repository Layer Calling");
            ReadHouseByIdResponse response = new ReadHouseByIdResponse();
            response.IsSuccess = true;
            response.Message = "Successful";
            try
            {
                if (_mySqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _mySqlConnection.OpenAsync();
                }
                using (MySqlCommand sqlCommand = new MySqlCommand(SqlQueries.ReadHouseById, _mySqlConnection))
                {
                    try
                    {
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlCommand.CommandTimeout = 180;
                        sqlCommand.Parameters.AddWithValue("@HouseId", request.HouseId);
                        using (MySqlDataReader dataReader = await sqlCommand.ExecuteReaderAsync())
                        {
                            if (dataReader.HasRows)
                            {
                                response.Data = new CommonLayer.Models.House.ReadHouseById();
                                if (await dataReader.ReadAsync())
                                {
                                    response.Data.HouseId = dataReader["HouseId"] != DBNull.Value ? Convert.ToString(dataReader["HouseId"]) : string.Empty;
                                    response.Data.Name = dataReader["Name"] != DBNull.Value ? Convert.ToString(dataReader["Name"]) : string.Empty;
                                    response.Data.WardId = dataReader["WardId"] != DBNull.Value ? Convert.ToString(dataReader["WardId"]) : string.Empty;
                                    response.Data.Address = dataReader["Address"] != DBNull.Value ? Convert.ToString(dataReader["Address"]) : string.Empty;
                                    response.Data.Status = dataReader["Status"] != DBNull.Value ? Convert.ToInt32(dataReader["Status"]) : 0;
                                    response.Data.Image = dataReader["Image"] != DBNull.Value ? Convert.ToString(dataReader["Image"]) : string.Empty;
                                    response.Data.Longitude = dataReader["Longitude"] != DBNull.Value ? Convert.ToDouble(dataReader["Longitude"]) : 0;
                                    response.Data.Lastitude = dataReader["Lastitude"] != DBNull.Value ? Convert.ToDouble(dataReader["Lastitude"]) : 0;
                                    response.Data.HouseTypeId = dataReader["HouseTypeId"] != DBNull.Value ? Convert.ToString(dataReader["HouseTypeId"]) : string.Empty;
                                    response.Data.OwnerId = dataReader["OwnerId"] != DBNull.Value ? Convert.ToString(dataReader["OwnerId"]) : string.Empty;
                                    response.Data.CertificateOfOwnership = dataReader["CertificateOfOwnership"] != DBNull.Value ? Convert.ToString(dataReader["CertificateOfOwnership"]) : string.Empty;
                                    response.Data.CreatedBy = dataReader["CreatedBy"] != DBNull.Value ? Convert.ToString(dataReader["CreatedBy"]) : string.Empty;
                                    response.Data.CreateTime = dataReader["CreateTime"] != DBNull.Value ? Convert.ToDateTime(dataReader["CreateTime"]) : DateTime.MinValue;
                                    response.Data.UpdatedTime = dataReader["UpdatedTime"] != DBNull.Value ? Convert.ToDateTime(dataReader["UpdatedTime"]) : DateTime.MinValue;
                                    response.Data.DisableTime = dataReader["DisableTime"] != DBNull.Value ? Convert.ToDateTime(dataReader["DisableTime"]) : DateTime.MinValue;                                   
                                }
                            }
                            else
                            {
                                response.IsSuccess = true;
                                response.Message = "No Record At DataBase";
                                _logger.LogWarning("No Record At DataBase");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        response.IsSuccess = false;
                        response.Message = ex.Message;
                        _logger.LogError($"ReadHouseById Repository Layer Error : {ex.Message}");
                    }
                    finally
                    {
                        await sqlCommand.DisposeAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                _logger.LogError($"ReadHouseById Repository Layer Error : {ex.Message}");
            }
            finally
            {
                await _mySqlConnection.CloseAsync();
                await _mySqlConnection.DisposeAsync();
            }
            return response;
        }
        public async Task<AddUserResponse> AddUser(AddUserRequest request)
        {
            _logger.LogInformation("AddUser Method calling in Repository Layer");
            AddUserResponse response = new AddUserResponse();
            response.IsSuccess = true;
            response.Message = "Success";
            try
            {
                if (_mySqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _mySqlConnection.OpenAsync();
                }

                using (MySqlCommand sqlCommand = new MySqlCommand(SqlQueries.AddUser, _mySqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@UserId", request.UserId);
                    sqlCommand.Parameters.AddWithValue("@Email", request.Email);
                    sqlCommand.Parameters.AddWithValue("@Phone", request.Phone);
                    sqlCommand.Parameters.AddWithValue("@FullName", request.FullName);
                    sqlCommand.Parameters.AddWithValue("@DateOfBirth", request.DateOfBirth);
                    sqlCommand.Parameters.AddWithValue("@RoleId", request.RoleId);
                    sqlCommand.Parameters.AddWithValue("@Image", request.Image);
                    sqlCommand.Parameters.AddWithValue("@CitizenNumber", request.CitizenNumber);
                    sqlCommand.Parameters.AddWithValue("@CitizenNumberDate", request.CitizenNumberDate);
                    sqlCommand.Parameters.AddWithValue("@Address", request.Address);
                    sqlCommand.Parameters.AddWithValue("@Gender", request.Gender);
                    sqlCommand.Parameters.AddWithValue("@UniversityId", request.UniversityId);
                    sqlCommand.Parameters.AddWithValue("@CreatedTime", request.CreatedTime);
                    sqlCommand.Parameters.AddWithValue("@LastModified", request.LastModified);
                    int Status = await sqlCommand.ExecuteNonQueryAsync();
                    if (Status <= 0)
                    {
                        response.IsSuccess = false;
                        response.Message = "AddUser Query Not Executed";
                        _logger.LogError("AddUser Query Not Executed");
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                _logger.LogError($"AddUser Repository Layer Error : {ex.Message}");
            }
            finally
            {
                await _mySqlConnection.CloseAsync();
                await _mySqlConnection.DisposeAsync();

            }
            return response;
        }

        
    }
}
