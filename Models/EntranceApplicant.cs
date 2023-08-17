using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartUni.Models
{
    public class EntranceApplicant
    {
		//Personal Information
		[Key]
		public int Id { get; set; }
		public string StudentId { get; set; } = string.Empty;

		[Display(Name = "Status")]
		public int StatusID { get; set; }
		[ForeignKey(nameof(StatusID))]
		public virtual StatusType? StatusType { get; set; }

		[Display(Name = "Title")]
		public int TitleID { get; set; }
		[ForeignKey(nameof(TitleID))]
		public virtual TitleType? TitleType { get; set; }

		[Display(Name = "Image")]
		public string? ImagePath { get; set; } = string.Empty;

		[Display(Name = "First Name")]
		[Required]
		public string FirstName { get; set; } = String.Empty;

		[Display(Name = "Middle Name")]
		public string? MiddleName { get; set; }

		[Display(Name = "Last Name")]
		public string LastName { get; set; } = String.Empty;

		[Display(Name = "Gender")]
		public int GenderID { get; set; }
		[ForeignKey(nameof(GenderID))]
		public virtual GenderType? GenderType { get; set; }

		[Display(Name = "Date of Birth")]
		public DateTime DateofBirth { get; set; }

		[Display(Name = "Place of Birth")]
		public string? Hometown_State { get; set; }

		[Display(Name = "Nationality")]
		public int? NationalityID { get; set; }
		[ForeignKey(nameof(NationalityID))]
		public virtual NationalityType? NationalityType { get; set; }

		[Display(Name = "Country of Origin")]
		public int? CountryID { get; set; }
		[ForeignKey(nameof(CountryID))]
		public virtual CountryType? CountryType { get; set; }

		[Display(Name = "Religious Affiliation")]
		public int ReligionID { get; set; }
		[ForeignKey(nameof(ReligionID))]
		public virtual ReligionType? ReligionType { get; set; }

		[Display(Name = "Marital Status")]
		public int MaritalStatusID { get; set; }
		[ForeignKey(nameof(MaritalStatusID))]
		public virtual MaritalStatusType? MaritalStatusType { get; set; }

		[Display(Name = "Number of Children")]
		public int? NumberofChildren { get; set; }

		//Address of Applicant

		[Display(Name = "Current Address")]
		public string CurrentAddress { get; set; } = string.Empty;

		[Display(Name = "Phone Number")]
		public string? PhoneNumber { get; set; } = string.Empty;

		[Display(Name = "Email Address")]
		public string? EmailAddress { get; set; } = string.Empty;

		//Permanent Home Address if different from the postal Address
		public string? AddressLine1 { get; set; } = string.Empty;
		public string? AddressLine2 { get; set; } = string.Empty;
		public string? AddressLine3 { get; set; } = string.Empty;

		//Next of Kin Information
		[Display(Name = "Name of Next of Kin")]
		public string? NextofKin { get; set; } = string.Empty;
		public int? OccupationID { get; set; }
		[ForeignKey(nameof(OccupationID))]
		public virtual OccupationType? OccupationType { get; set; }

		public string? EmergencyContact { get; set; } = string.Empty;
		public int RelationshipTypeID { get; set; }
		[ForeignKey(nameof(RelationshipTypeID))]
		public virtual RelationshipType? RelationshipType { get; set; }
		public string? Address { get; set; } = string.Empty;
		public string? Email { get; set; } = string.Empty;
		public string? Telephone { get; set; } = string.Empty;

		//Disability Information
		public int DisabilityTypeID { get; set; }
		[ForeignKey(nameof(DisabilityTypeID))]
		public virtual DisabilityType? DisabilityType { get; set; }

		//Secondary of High School Attended
		[Display(Name = "County of High School Attended")]
		public string? CountyOfHighSchoolAttended { get; set; }
		[Display(Name = "High School Attended")]
		public string? HighSchoolAttendedName { get; set; }
		[Display(Name = "From")]
		public DateTime? StartYear { get; set; }
		[Display(Name = "To")]
		public DateTime? EndYear { get; set; }

		//First University Attended
		[Display(Name = "University Name")]
		public string? NameOfUniversity { get; set; }
		public int? UniversityCountryID { get; set; }
		[ForeignKey(nameof(UniversityCountryID))]
		public virtual CountryType? UniversityCountryType { get;set; }
		public DateTime? UniversityStartYear { get; set; }
		public DateTime? UniversityEndYear { get; set; }

		//Program Information
		public int? OfferingTypeID { get; set; }
		[ForeignKey(nameof(OfferingTypeID))]
		public virtual OfferingType? OfferingType { get; set; }

	}
}
