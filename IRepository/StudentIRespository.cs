using SmartUni.Models;

namespace SmartUni.IRepository
{
	public interface StudentIRespository
	{
		public List<Student> GetStudents();

		Student GetStudent(int id);

		Student AddStudent(Student student);
		Student UpdateStudent(Student student);
		bool DeleteStudent(int id);


	}
}
