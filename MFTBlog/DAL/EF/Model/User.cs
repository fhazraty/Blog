using System.ComponentModel.DataAnnotations;

namespace DAL.EF.Model
{
	public class User
	{
		[Key]
		public int Id { get; set; }
		[Required, MaxLength(50)]
		public string FirstName { get; set; }
		[Required, MaxLength(50)]
		public string LastName { get; set; }
		[Required, MaxLength(10)]
		public string NationalCode { get; set; }
		[Required, MaxLength(50)]
		public string Username { get; set; }
		[Required]
		public string PasswordHash { get; set; }
		[Required]
		public string Salt { get; set; }
		public DateTime BirthDate { get; set; }
		public DateTime PasswordCreatedAt { get; set; }
		public DateTime PasswordUpdatedAt { get; set; }
		public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
		public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
	}
}
