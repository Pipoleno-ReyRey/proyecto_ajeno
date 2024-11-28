using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using estudiante_curso.Domain;

[Route("api/[controller]")]
[ApiController]
public class StudentController : ControllerBase
{
    private readonly string _connectionString;

    public StudentController(string connectionString)
    {
        _connectionString = connectionString;
    }

    [HttpPost]
    public IActionResult CreateStudent([FromBody] Student student)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        string query = "INSERT INTO Student (Name, Email, Phone, CourseId) VALUES (@Name, @Email, @Phone, @CourseId)";
        using var command = new MySqlCommand(query, connection);

        command.Parameters.AddWithValue("@Name", student.name);
        command.Parameters.AddWithValue("@Email", student.email);
        command.Parameters.AddWithValue("@Phone", student.phone);
        command.Parameters.AddWithValue("@CourseId", student.courseId);

        command.ExecuteNonQuery();
        return Ok("Student created successfully");
    }

    [HttpGet]
    public IActionResult GetStudents()
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        string query = "SELECT * FROM Student";
        using var command = new MySqlCommand(query, connection);
        using var reader = command.ExecuteReader();

        var students = new List<Student>();
        while (reader.Read())
        {
            students.Add(new Student
            {
                id = reader.GetInt32("Id"),
                name = reader.GetString("Name"),
                email = reader.GetString("Email"),
                phone = reader.GetString("Phone"),
                courseId = reader.GetInt32("CourseId")
            });
        }

        return Ok(students);
    }

    [HttpGet("{id}")]
    public IActionResult GetStudentById(int id)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        string query = "SELECT * FROM Student WHERE Id = @Id";
        using var command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@Id", id);

        using var reader = command.ExecuteReader();
        if (reader.Read())
        {
            var student = new Student
            {
                id = reader.GetInt32("Id"),
                name = reader.GetString("Name"),
                email = reader.GetString("Email"),
                phone = reader.GetString("Phone"),
                courseId = reader.GetInt32("CourseId")
            };
            return Ok(student);
        }

        return NotFound("Student not found");
    }

    [HttpPut("{id}")]
    public IActionResult UpdateStudent(int id, [FromBody] Student student)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        string query = "UPDATE Student SET Name = @Name, Email = @Email, Phone = @Phone, CourseId = @CourseId WHERE Id = @Id";
        using var command = new MySqlCommand(query, connection);

        command.Parameters.AddWithValue("@Name", student.name);
        command.Parameters.AddWithValue("@Email", student.email);
        command.Parameters.AddWithValue("@Phone", student.phone);
        command.Parameters.AddWithValue("@CourseId", student.courseId);
        command.Parameters.AddWithValue("@Id", id);

        command.ExecuteNonQuery();
        return Ok("Student updated successfully");
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteStudent(int id)
    {
        using var connection = new MySqlConnection(_connectionString);
        connection.Open();

        string query = "DELETE FROM Student WHERE Id = @Id";
        using var command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@Id", id);

        command.ExecuteNonQuery();
        return Ok("Student deleted successfully");
    }
}
