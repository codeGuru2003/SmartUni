using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using SmartUni.Models;
using SmartUni.Services;
using System.Data;

namespace SmartUni.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private ICurrentUserService _currentUserService;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentUserService currentUserService)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
			: base(options)
        {
            _currentUserService = currentUserService;
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ProcessSave();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void ProcessSave()
        {
            var currentTime = DateTime.Now;
            foreach (var item in ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added && e.Entity is AuditTrail))
            {
                var entity = item.Entity as AuditTrail;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                entity.CreatedOn = currentTime;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                entity.CreatedBy = _currentUserService.GetCurrentUser();
                entity.ModifiedOn = currentTime;
                entity.ModifiedBy = _currentUserService.GetCurrentUser();
            }

            foreach (var item in ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified && e.Entity is AuditTrail))
            {
                var entity = item.Entity as AuditTrail;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                entity.ModifiedOn = currentTime;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                entity.ModifiedBy = _currentUserService.GetCurrentUser();
                item.Property(nameof(entity.CreatedBy)).IsModified = false;
                item.Property(nameof(entity.CreatedOn)).IsModified = false;
            }
        }
        public DbSet<AcademicSemester> AcademicSemesters { get; set; }
        public DbSet<AcademicYearType> AcademicYearTypes { get; set; }
        public DbSet<BillingType> BillingTypes { get; set; }
        public DbSet<College> College { get; set; }
        public DbSet<CollegeType> CollegeTypes { get; set; }
        public DbSet<CountryType> CountryTypes { get; set; }
        public DbSet<CourseType> CourseTypes { get; set; }
        public DbSet<CurrencyType> CurrencyTypes { get; set; }
        public DbSet<DeleteLogs> DeleteLogs { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<DepartmentDegree> DepartmentDegrees { get; set; }
        public DbSet<DepartmentLevel> DepartmentLevels { get; set; }
        public DbSet<DepartmentMajor> DepartmentMajors { get; set; }
        public DbSet<DisabilityType> DisabilityTypes { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<EntranceApplicant> EntranceApplicants { get; set; }
        public DbSet<EntranceApplicantDocument> EntranceApplicantDocuments { get; set; }
        public DbSet<EntranceApplicantReference> EntranceApplicantReferences { get; set; }
        public DbSet<GenderType> Genders { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupRole> GroupRoles { get; set; }
        public DbSet<LevelType> LevelTypes { get; set; }
        public DbSet<LoginLogs> LoginLogs { get; set; }
        public DbSet<MaritalStatusType> MaritalStatusTypes { get; set; }
        public DbSet<MajorMinor> MajorMinors { get; set; }
        public DbSet<NationalityType> NationalityTypes { get; set; }
        public DbSet<OccupationType> OccupationTypes { get; set; }
        public DbSet<OfferingType> OfferingTypes { get; set; }
        public DbSet<RelationshipType> RelationshipTypes { get; set; }
        public DbSet<ReligionType> ReligionTypes { get; set; }
        public DbSet<SectionType> SectionTypes { get; set; }
        public DbSet<SemesterType> SemesterTypes { get; set; }
        public DbSet<StatusType> StatusTypes { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<TestSchedule> TestSchedules { get; set; }
        public DbSet<TitleType> TitleTypes { get; set; }
		public DbSet<Token> Tokens { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }

		//Method to Update EntrantApplicant Educational Background Information
		public async Task<bool> UpdateEducationBackgroundInformation
        ( 
            string StudentId,
            string CountyOfHighSchoolAttended, string HighSchoolAttendedName, DateTime? StartYear,
            DateTime? EndYear, string? NameOfUniversity, int? UniversityCountryID, DateTime? UniversityStartYear,
            DateTime? UniversityEndYear 
        )
		{
			var connString = Database.GetConnectionString();
			using (SqlConnection conn = new SqlConnection(connString))
			{
				await conn.OpenAsync();
				using (SqlCommand cmd = new SqlCommand("_updateEducationBackgroundInformation", conn))
				{
					cmd.CommandType = CommandType.StoredProcedure;

					//Add Parameters

					cmd.Parameters.Add(new SqlParameter("@StudentId", SqlDbType.VarChar) { Value = StudentId ?? null });
					cmd.Parameters.Add(new SqlParameter("@CountyOfHighSchoolAttended", SqlDbType.VarChar) { Value = CountyOfHighSchoolAttended ?? null });
					cmd.Parameters.Add(new SqlParameter("@HighSchoolAttendedName", SqlDbType.VarChar) { Value = HighSchoolAttendedName ?? null });
					cmd.Parameters.Add(new SqlParameter("@StartYear", SqlDbType.DateTime) { Value = StartYear ?? null });
					cmd.Parameters.Add(new SqlParameter("@EndYear", SqlDbType.DateTime) { Value = EndYear ?? null });
					cmd.Parameters.Add(new SqlParameter("@NameOfUniversity", SqlDbType.VarChar) { Value = NameOfUniversity ?? null });
					cmd.Parameters.Add(new SqlParameter("@UniversityCountryID", SqlDbType.Int) { Value = UniversityCountryID ?? null });
					cmd.Parameters.Add(new SqlParameter("@UniversityStartYear", SqlDbType.DateTime) { Value = UniversityStartYear ?? null });
					cmd.Parameters.Add(new SqlParameter("@UniversityEndYear", SqlDbType.DateTime) { Value = UniversityEndYear ?? null });
					var result = cmd.ExecuteReader();
					await conn.DisposeAsync();

					if (result.RecordsAffected <= 0)
					{
						return false;
					}
					else
					{
						return true;
					}
				}
			}
		}
	}
}
