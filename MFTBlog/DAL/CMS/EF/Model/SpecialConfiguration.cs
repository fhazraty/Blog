using System.ComponentModel.DataAnnotations;

namespace DAL.CMS.EF.Model
{
	/// <summary>
	/// Represents a special configuration setting in the system.
	/// نمایانگر یک تنظیم خاص در سیستم.
	/// </summary>
	public class SpecialConfiguration
	{
		/// <summary>
		/// Gets or sets the unique identifier for the configuration.
		/// کلید اصلی.
		/// </summary>
		[Key]
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets the name of the configuration.
		/// نام تنظیم.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the value of the configuration.
		/// مقدار تنظیم خاص.
		/// </summary>
		public string Value { get; set; }
	}
}
