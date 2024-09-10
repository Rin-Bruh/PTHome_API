using MySqlConnector;
using PTHome.Common_Utility;
using PTHome.CommonLayer.Models.Conntract.PTHome.CommonLayer.Models.Conntract;
using PTHome.CommonLayer.Models.Contract;
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
            _logger.LogInformation("ReadAllHouse Repository Layer Calling");
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
                        _logger.LogError($"ReadAllHouse Repository Layer Error : {ex.Message}");
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
                _logger.LogError($"ReadAllHouse Repository Layer Error : {ex.Message}");
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

        public async Task<ReadAllUserResponse> ReadAllUser()
        {
            _logger.LogInformation("ReadAllUser Repository Layer Calling");
            ReadAllUserResponse response = new ReadAllUserResponse();
            response.IsSuccess = true;
            response.Message = "Successful";
            try
            {
                if (_mySqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _mySqlConnection.OpenAsync();
                }
                using (MySqlCommand sqlCommand = new MySqlCommand(SqlQueries.ReadAllUser, _mySqlConnection))
                {
                    try
                    {
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlCommand.CommandTimeout = 180;
                        using (MySqlDataReader dataReader = await sqlCommand.ExecuteReaderAsync())
                        {
                            if (dataReader.HasRows)
                            {
                                response.readAllUser = new List<GetReadAllUser>();
                                while (await dataReader.ReadAsync())
                                {
                                    GetReadAllUser getData = new GetReadAllUser();
                                    getData.UserId = dataReader["UserId"] != DBNull.Value ? Convert.ToString(dataReader["UserId"]) : string.Empty;
                                    getData.Email = dataReader["Email"] != DBNull.Value ? Convert.ToString(dataReader["Email"]) : string.Empty;
                                    getData.Phone = dataReader["Phone"] != DBNull.Value ? Convert.ToString(dataReader["Phone"]) : string.Empty;
                                    getData.FullName = dataReader["FullName"] != DBNull.Value ? Convert.ToString(dataReader["FullName"]) : string.Empty;
                                    getData.DateOfBirth = dataReader["DateOfBirth"] != DBNull.Value ? Convert.ToDateTime(dataReader["DateOfBirth"]) : DateTime.MinValue;
                                    getData.Status = dataReader["Status"] != DBNull.Value ? Convert.ToInt32(dataReader["Status"]) : 0;
                                    getData.RoleId = dataReader["RoleId"] != DBNull.Value ? Convert.ToString(dataReader["RoleId"]) : string.Empty;
                                    getData.Image = dataReader["Image"] != DBNull.Value ? Convert.ToString(dataReader["Image"]) : string.Empty;
                                    getData.CitizenNumber = dataReader["CitizenNumber"] != DBNull.Value ? Convert.ToString(dataReader["CitizenNumber"]) : string.Empty;
                                    getData.CitizenNumberDate = dataReader["CitizenNumberDate"] != DBNull.Value ? Convert.ToDateTime(dataReader["CitizenNumberDate"]) : DateTime.MinValue;
                                    getData.Address = dataReader["Address"] != DBNull.Value ? Convert.ToString(dataReader["Address"]) : string.Empty;
                                    getData.Gender = dataReader["Gender"] != DBNull.Value ? Convert.ToInt32(dataReader["Gender"]) : 0;
                                    getData.UniversityId = dataReader["UniversityId"] != DBNull.Value ? Convert.ToString(dataReader["UniversityId"]) : string.Empty;
                                    getData.CreatedTime = dataReader["CreatedTime"] != DBNull.Value ? Convert.ToDateTime(dataReader["CreatedTime"]) : DateTime.MinValue;
                                    getData.LastModified = dataReader["LastModified"] != DBNull.Value ? Convert.ToDateTime(dataReader["LastModified"]) : DateTime.MinValue;
                                    
                                    response.readAllUser.Add(getData);
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
                        _logger.LogError($"ReadAllUser Repository Layer Error : {ex.Message}");
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
                _logger.LogError($"ReadAllUser Repository Layer Error : {ex.Message}");
            }
            finally
            {
                await _mySqlConnection.CloseAsync();
                await _mySqlConnection.DisposeAsync();
            }
            return response;
        }

        public async Task<UpdateAllUserByIdResponse> UpdateAllUserById(UpdateAllUserByIdRequest request)
        {
            _logger.LogInformation("UpdateAllUserById Method calling in Repository Layer");
            UpdateAllUserByIdResponse response = new UpdateAllUserByIdResponse();
            response.IsSuccess = true;
            response.Message = "Success";
            try
            {
                if (_mySqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _mySqlConnection.OpenAsync();
                }

                using (MySqlCommand sqlCommand = new MySqlCommand(SqlQueries.UpdateAllUserById, _mySqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@UserId", request.UserId);
                    //sqlCommand.Parameters.AddWithValue("@Email", request.Email);
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
                    //sqlCommand.Parameters.AddWithValue("@CreatedTime", request.CreatedTime);
                    sqlCommand.Parameters.AddWithValue("@LastModified", request.LastModified);
                    int Status = await sqlCommand.ExecuteNonQueryAsync();
                    if (Status <= 0)
                    {
                        response.IsSuccess = false;
                        response.Message = "UpdateAllUserById Query Not Executed";
                        _logger.LogError("UpdateAllUserById Query Not Executed");
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                _logger.LogError($"UpdateAllUserById Repository Layer Error : {ex.Message}");
            }
            finally
            {
                await _mySqlConnection.CloseAsync();
                await _mySqlConnection.DisposeAsync();

            }
            return response;
        }

        public async Task<DeleteUserByIdResponse> DeleteUserById(DeleteUserByIdRequest request)
        {
            _logger.LogInformation("DeleteUserById Method calling in Repository Layer");
            DeleteUserByIdResponse response = new DeleteUserByIdResponse();
            response.IsSuccess = true;
            response.Message = "Success";
            try
            {
                if (_mySqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _mySqlConnection.OpenAsync();
                }

                using (MySqlCommand sqlCommand = new MySqlCommand(SqlQueries.DeleteUserById, _mySqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@UserId", request.UserId);
                    int Status = await sqlCommand.ExecuteNonQueryAsync();
                    if (Status <= 0)
                    {
                        response.IsSuccess = false;
                        response.Message = "DeleteUserById Query Not Executed";
                        _logger.LogError("DeleteUserById Query Not Executed");
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                _logger.LogError($"DeleteUserById Repository Layer Error : {ex.Message}");
            }
            finally
            {
                await _mySqlConnection.CloseAsync();
                await _mySqlConnection.DisposeAsync();

            }
            return response;
        }

        public async Task<ReadUserByIdResponse> ReadUserById(ReadUserByIdRequest request)
        {
            _logger.LogInformation("ReadUserById Repository Layer Calling");
            ReadUserByIdResponse response = new ReadUserByIdResponse();
            response.IsSuccess = true;
            response.Message = "Successful";
            try
            {
                if (_mySqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _mySqlConnection.OpenAsync();
                }
                using (MySqlCommand sqlCommand = new MySqlCommand(SqlQueries.ReadUserById, _mySqlConnection))
                {
                    try
                    {
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlCommand.CommandTimeout = 180;
                        sqlCommand.Parameters.AddWithValue("@UserId", request.UserId);
                        using (MySqlDataReader dataReader = await sqlCommand.ExecuteReaderAsync())
                        {
                            if (dataReader.HasRows)
                            {
                                response.Data = new CommonLayer.Models.User.ReadUserById();
                                if (await dataReader.ReadAsync())
                                {
                                    response.Data.UserId = dataReader["UserId"] != DBNull.Value ? Convert.ToString(dataReader["UserId"]) : string.Empty;
                                    response.Data.Email = dataReader["Email"] != DBNull.Value ? Convert.ToString(dataReader["Email"]) : string.Empty;
                                    response.Data.Phone = dataReader["Phone"] != DBNull.Value ? Convert.ToString(dataReader["Phone"]) : string.Empty;
                                    response.Data.FullName = dataReader["FullName"] != DBNull.Value ? Convert.ToString(dataReader["FullName"]) : string.Empty;
                                    response.Data.DateOfBirth = dataReader["DateOfBirth"] != DBNull.Value ? Convert.ToDateTime(dataReader["DateOfBirth"]) : DateTime.MinValue;
                                    response.Data.Status = dataReader["Status"] != DBNull.Value ? Convert.ToInt32(dataReader["Status"]) : 0;
                                    response.Data.RoleId = dataReader["RoleId"] != DBNull.Value ? Convert.ToString(dataReader["RoleId"]) : string.Empty;
                                    response.Data.Image = dataReader["Image"] != DBNull.Value ? Convert.ToString(dataReader["Image"]) : string.Empty;
                                    response.Data.CitizenNumber = dataReader["CitizenNumber"] != DBNull.Value ? Convert.ToString(dataReader["CitizenNumber"]) : string.Empty;
                                    response.Data.CitizenNumberDate = dataReader["CitizenNumberDate"] != DBNull.Value ? Convert.ToDateTime(dataReader["CitizenNumberDate"]) : DateTime.MinValue;
                                    response.Data.Address = dataReader["Address"] != DBNull.Value ? Convert.ToString(dataReader["Address"]) : string.Empty;
                                    response.Data.Gender = dataReader["Gender"] != DBNull.Value ? Convert.ToInt32(dataReader["Gender"]) : 0;
                                    response.Data.UniversityId = dataReader["UniversityId"] != DBNull.Value ? Convert.ToString(dataReader["UniversityId"]) : string.Empty;
                                    response.Data.CreatedTime = dataReader["CreatedTime"] != DBNull.Value ? Convert.ToDateTime(dataReader["CreatedTime"]) : DateTime.MinValue;
                                    response.Data.LastModified = dataReader["LastModified"] != DBNull.Value ? Convert.ToDateTime(dataReader["LastModified"]) : DateTime.MinValue;
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
                        _logger.LogError($"ReadUserById Repository Layer Error : {ex.Message}");
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
                _logger.LogError($"ReadUserById Repository Layer Error : {ex.Message}");
            }
            finally
            {
                await _mySqlConnection.CloseAsync();
                await _mySqlConnection.DisposeAsync();
            }
            return response;
        }

        public async Task<AddContractResponse> AddContract(AddContractRequest request)
        {
            _logger.LogInformation("AddContract Method calling in Repository Layer");
            AddContractResponse response = new AddContractResponse();
            response.IsSuccess = true;
            response.Message = "Success";
            try
            {
                if (_mySqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _mySqlConnection.OpenAsync();
                }

                using (MySqlCommand sqlCommand = new MySqlCommand(SqlQueries.AddContract, _mySqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@ContractId", request.ContractId);
                    sqlCommand.Parameters.AddWithValue("@RentEntityId", request.RentEntityId);
                    sqlCommand.Parameters.AddWithValue("@DateSigned", request.DateSigned);
                    sqlCommand.Parameters.AddWithValue("@StartRentDate", request.StartRentDate);
                    sqlCommand.Parameters.AddWithValue("@CreatedTime", request.CreatedTime);
                    sqlCommand.Parameters.AddWithValue("@UpdateTime", request.UpdateTime);
                    int Status = await sqlCommand.ExecuteNonQueryAsync();
                    if (Status <= 0)
                    {
                        response.IsSuccess = false;
                        response.Message = "AddContract Query Not Executed";
                        _logger.LogError("AddContract Query Not Executed");
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                _logger.LogError($"AddContract Repository Layer Error : {ex.Message}");
            }
            finally
            {
                await _mySqlConnection.CloseAsync();
                await _mySqlConnection.DisposeAsync();

            }
            return response;
        }

        public async Task<ReadAllContractResponse> ReadAllContract()
        {
            _logger.LogInformation("ReadAllContract Repository Layer Calling");
            ReadAllContractResponse response = new ReadAllContractResponse();
            response.IsSuccess = true;
            response.Message = "Successful";
            try
            {
                if (_mySqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _mySqlConnection.OpenAsync();
                }
                using (MySqlCommand sqlCommand = new MySqlCommand(SqlQueries.ReadAllContract, _mySqlConnection))
                {
                    try
                    {
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlCommand.CommandTimeout = 180;
                        using (MySqlDataReader dataReader = await sqlCommand.ExecuteReaderAsync())
                        {
                            if (dataReader.HasRows)
                            {
                                response.readAllContract = new List<GetReadAllContract>();
                                while (await dataReader.ReadAsync())
                                {
                                    GetReadAllContract getData = new GetReadAllContract();
                                    getData.ContractId = dataReader["ContractId"] != DBNull.Value ? Convert.ToString(dataReader["ContractId"]) : string.Empty;
                                    getData.RentEntityId = dataReader["RentEntityId"] != DBNull.Value ? Convert.ToString(dataReader["RentEntityId"]) : string.Empty;
                                    getData.DateSigned = dataReader["DateSigned"] != DBNull.Value ? Convert.ToDateTime(dataReader["DateSigned"]) : DateTime.MinValue;
                                    getData.Status = dataReader["Status"] != DBNull.Value ? Convert.ToInt32(dataReader["Status"]) : 0;
                                    getData.StartRentDate = dataReader["StartRentDate"] != DBNull.Value ? Convert.ToDateTime(dataReader["StartRentDate"]) : DateTime.MinValue;
                                    getData.CreatedTime = dataReader["CreatedTime"] != DBNull.Value ? Convert.ToDateTime(dataReader["CreatedTime"]) : DateTime.MinValue;
                                    getData.UpdateTime = dataReader["UpdateTime"] != DBNull.Value ? Convert.ToDateTime(dataReader["UpdateTime"]) : DateTime.MinValue;

                                    response.readAllContract.Add(getData);
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
                        _logger.LogError($"ReadAllContract Repository Layer Error : {ex.Message}");
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
                _logger.LogError($"ReadAllContract Repository Layer Error : {ex.Message}");
            }
            finally
            {
                await _mySqlConnection.CloseAsync();
                await _mySqlConnection.DisposeAsync();
            }
            return response;
        }

        public async Task<AddContractHistoryResponse> AddContractHistory(AddContractHistoryRequest request)
        {
            _logger.LogInformation("AddContractHistory Method calling in Repository Layer");
            AddContractHistoryResponse response = new AddContractHistoryResponse();
            response.IsSuccess = true;
            response.Message = "Success";
            try
            {
                if (_mySqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _mySqlConnection.OpenAsync();
                }

                using (MySqlCommand sqlCommand = new MySqlCommand(SqlQueries.AddContractHistory, _mySqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@HistoryId", request.HistoryId);
                    sqlCommand.Parameters.AddWithValue("@ContractId", request.ContractId);
                    sqlCommand.Parameters.AddWithValue("@Price", request.Price);
                    sqlCommand.Parameters.AddWithValue("@RenterId", request.RenterId);
                    sqlCommand.Parameters.AddWithValue("@OwnerId", request.OwnerId);
                    sqlCommand.Parameters.AddWithValue("@Description", request.Description);
                    sqlCommand.Parameters.AddWithValue("@Image", request.Image);
                    sqlCommand.Parameters.AddWithValue("@ExpiredTime", request.ExpiredTime);
                    sqlCommand.Parameters.AddWithValue("@PeopleApplied", request.PeopleApplied);
                    sqlCommand.Parameters.AddWithValue("@UpdatedBy", request.UpdatedBy);
                    sqlCommand.Parameters.AddWithValue("@UpdatedTime", request.UpdatedTime);
                    int Status = await sqlCommand.ExecuteNonQueryAsync();
                    if (Status <= 0)
                    {
                        response.IsSuccess = false;
                        response.Message = "AddContractHistory Query Not Executed";
                        _logger.LogError("AddContractHistory Query Not Executed");
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                _logger.LogError($"AddContractHistory Repository Layer Error : {ex.Message}");
            }
            finally
            {
                await _mySqlConnection.CloseAsync();
                await _mySqlConnection.DisposeAsync();

            }
            return response;
        }

        public async Task<ReadAllContractHistoryResponse> ReadAllContractHistory()
        {
            _logger.LogInformation("ReadAllContractHistory Repository Layer Calling");
            ReadAllContractHistoryResponse response = new ReadAllContractHistoryResponse();
            response.IsSuccess = true;
            response.Message = "Successful";
            try
            {
                if (_mySqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _mySqlConnection.OpenAsync();
                }
                using (MySqlCommand sqlCommand = new MySqlCommand(SqlQueries.ReadAllContractHistory, _mySqlConnection))
                {
                    try
                    {
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlCommand.CommandTimeout = 180;
                        using (MySqlDataReader dataReader = await sqlCommand.ExecuteReaderAsync())
                        {
                            if (dataReader.HasRows)
                            {
                                response.readAllContractHistory = new List<GetReadAllContractHistory>();
                                while (await dataReader.ReadAsync())
                                {
                                    GetReadAllContractHistory getData = new GetReadAllContractHistory();
                                    getData.HistoryId = dataReader["HistoryId"] != DBNull.Value ? Convert.ToInt32(dataReader["HistoryId"]) : 0;
                                    getData.ContractId = dataReader["ContractId"] != DBNull.Value ? Convert.ToString(dataReader["ContractId"]) : string.Empty;
                                    getData.Price = dataReader["Price"] != DBNull.Value ? Convert.ToInt32(dataReader["Price"]) : 0;
                                    getData.Status = dataReader["Status"] != DBNull.Value ? Convert.ToInt32(dataReader["Status"]) : 0;
                                    getData.RenterId = dataReader["RenterId"] != DBNull.Value ? Convert.ToString(dataReader["RenterId"]) : string.Empty;
                                    getData.OwnerId = dataReader["OwnerId"] != DBNull.Value ? Convert.ToString(dataReader["OwnerId"]) : string.Empty;
                                    getData.Description = dataReader["Description"] != DBNull.Value ? Convert.ToString(dataReader["Description"]) : string.Empty;
                                    getData.Image = dataReader["Image"] != DBNull.Value ? Convert.ToString(dataReader["Image"]) : string.Empty;
                                    getData.ExpiredTime = dataReader["ExpiredTime"] != DBNull.Value ? Convert.ToDateTime(dataReader["ExpiredTime"]) : DateTime.MinValue;
                                    getData.PeopleApplied = dataReader["PeopleApplied"] != DBNull.Value ? Convert.ToInt32(dataReader["PeopleApplied"]) : 0;
                                    getData.UpdatedBy = dataReader["UpdatedBy"] != DBNull.Value ? Convert.ToString(dataReader["UpdatedBy"]) : string.Empty;
                                    getData.UpdatedTime = dataReader["UpdatedTime"] != DBNull.Value ? Convert.ToDateTime(dataReader["UpdatedTime"]) : DateTime.MinValue;

                                    response.readAllContractHistory.Add(getData);
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
                        _logger.LogError($"ReadAllContractHistory Repository Layer Error : {ex.Message}");
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
                _logger.LogError($"ReadAllContractHistory Repository Layer Error : {ex.Message}");
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
