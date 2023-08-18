using System.ComponentModel.DataAnnotations;

namespace SmartUni.Models
{
	public class Harmonize
	{
		[Key] 
		public int Id { get; set; }
		public bool IsValid { get; set; } = true;
	}
}
