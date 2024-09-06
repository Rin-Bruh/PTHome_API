using System.ComponentModel.DataAnnotations;

namespace PTHome.CommonLayer.Models.User
{
    public class DeleteUserByIdRequest
    {
        [Required(ErrorMessage = "UserId Is Mandetory Field")]
        public string UserId { get; set; }

    }
    public class DeleteUserByIdResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
