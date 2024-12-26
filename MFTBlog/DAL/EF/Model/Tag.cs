using System.ComponentModel.DataAnnotations;

namespace DAL.EF.Model
{
	public class Tag
	{
		[Key]
		public int Id { get; set; }

		[Required, MaxLength(50)]
		public string Name { get; set; }

		public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
	}
}
