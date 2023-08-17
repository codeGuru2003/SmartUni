using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

namespace SmartUni.Models
{
	public class EntranceApplicantDocument : AuditTrail
	{
		[Key]
		public int Id { get; set; }
		public int EntranceApplicantId { get; set; }
		[ForeignKey(nameof(EntranceApplicantId))]
		public virtual EntranceApplicant? EntranceApplicant { get; set;}
		public int DocumentTypeId { get; set; }
		[ForeignKey(nameof(DocumentTypeId))]
		public virtual DocumentType? DocumentType { get; set; }
		public string? FilePath { get; set; }
	}
}
