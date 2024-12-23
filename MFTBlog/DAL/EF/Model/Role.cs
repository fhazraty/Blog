using System.ComponentModel.DataAnnotations;

namespace DAL.EF.Model
{
	public class Role
	{
		[Key]
		public int Id { get; set; }

		[Required, MaxLength(50)]
		public string Name { get; set; }

		public ICollection<User> Users { get; set; } = new List<User>();
	}
}
