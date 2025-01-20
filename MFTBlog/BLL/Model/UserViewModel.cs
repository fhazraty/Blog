using DAL.EF.Model;

namespace BLL.Model
{
	public class UserViewModel
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string NationalCode { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public DateTime BirthDate { get; set; }
		public ICollection<Role> Roles { get; set; } = new List<Role>();
	}
}
