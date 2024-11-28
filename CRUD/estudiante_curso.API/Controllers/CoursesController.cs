using Microsoft.AspNetCore.Mvc;
using estudiante_curso.Domain;
using MySql.Data.MySqlClient;

namespace MiProyectoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly string _connectionString = "Server=localhost;Database=LibraryDB;User=root;Password=1234;";

        // Obtener todos los cursos
        [HttpGet]
        public IActionResult GetCourses()
        {
            List<Course> courses = new List<Course>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("SELECT * FROM Courses;", connection);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int _id;
                    Int32.TryParse(reader["Id"].ToString(), out _id);
                    courses.Add(new Course
                    {
                        id = _id,
                        name = reader["Name"].ToString(),
                        description = reader["Description"].ToString()
                    });
                }
            }

            return Ok(courses);
        }

        // Obtener un curso por ID
        [HttpGet("{id}")]
        public IActionResult GetCourseById(int id)
        {
            Course course = new Course();

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("SELECT * FROM Courses WHERE Id = @Id;", connection);
                command.Parameters.AddWithValue("@Id", id);
                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    int _id;
                    Int32.TryParse(reader["Id"].ToString(), out _id);
                    course = new Course
                    {
                        id = _id,
                        name = reader["Name"].ToString(),
                        description = reader["Description"].ToString()
                    };
                }
            }

            if (course == null)
            {
                return NotFound("Course not found");
            }

            return Ok(course);
        }

        // Crear un nuevo curso
        [HttpPost]
        public IActionResult CreateCourse([FromBody] Course course)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("INSERT INTO Courses (Name, Description) VALUES (@Name, @Description);", connection);
                command.Parameters.AddWithValue("@Name", course.name);
                command.Parameters.AddWithValue("@Description", course.description);
                command.ExecuteNonQuery();
            }

            return Ok("Course created successfully");
        }

        // Actualizar un curso existente
        [HttpPut("{id}")]
        public IActionResult UpdateCourse(int id, [FromBody] Course course)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("UPDATE Courses SET Name = @Name, Description = @Description WHERE Id = @Id;", connection);
                command.Parameters.AddWithValue("@Name", course.name);
                command.Parameters.AddWithValue("@Description", course.description);
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }

            return Ok("Course updated successfully");
        }

        // Eliminar un curso
        [HttpDelete("{id}")]
        public IActionResult DeleteCourse(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("DELETE FROM Courses WHERE Id = @Id;", connection);
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }

            return Ok("Course deleted successfully");
        }
    }
}
