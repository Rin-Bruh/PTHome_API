using System.ComponentModel.DataAnnotations;

namespace PTHome.CommonLayer.Models.User
{
    public class AddUserRequest
    {
        public string UserId { get; set; }

        [Required(ErrorMessage = "Email Is Mandetory Field")]
        [RegularExpression("^[0-9a-zA-Z]+([._+-][0-9a-zA-Z]+)*@[0-9a-zA-Z]+.[a-zA-Z]{2,4}([.][a-zA-Z]{2,3})?$", ErrorMessage = "Email Id Not In Currect Formate Example : XinchaoPTHome@gmail.com")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone Is Mandetory Field")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "FullName Is Mandetory Field")]
        public string FullName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        public int Status { get; set; }

        [Required]
        public string RoleId { get; set; }

        public string Image { get; set; }

        [Required(ErrorMessage = "HouseType Is Mandetory Field")]
        public string CitizenNumber { get; set; }

        [Required(ErrorMessage = "Owner Is Mandetory Field")]
        public DateTime CitizenNumberDate { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public int Gender { get; set; }

        public string UniversityId { get; set; }

        public DateTime CreatedTime { get; set; }

        public DateTime LastModified { get; set; }


    }
    public class AddUserResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
