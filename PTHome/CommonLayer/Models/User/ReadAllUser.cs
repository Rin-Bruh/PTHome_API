using PTHome.CommonLayer.Models.House;
using System.ComponentModel.DataAnnotations;

namespace PTHome.CommonLayer.Models.User
{
    public class ReadAllUserResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public List<GetReadAllUser> readAllUser { get; set; }
    }
    public class GetReadAllUser
    {
        public string UserId { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string FullName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int Status { get; set; }

        public string RoleId { get; set; }

        public string Image { get; set; }

        public string CitizenNumber { get; set; }

        public DateTime CitizenNumberDate { get; set; }

        public string Address { get; set; }

        public int Gender { get; set; }

        public string UniversityId { get; set; }

        public DateTime CreatedTime { get; set; }

        public DateTime LastModified { get; set; }
    }
}
