namespace estudiante_curso.Domain{

public class Student
{
    public int id { get; set; }
    public string? name { get; set; }
    public string? email { get; set; }
    public string? phone { get; set; }
    public int courseId { get; set; }
}

public class Course
{
    public int id { get; set; }
    public string? name { get; set; }
    public string? description { get; set; }
}

public class DBcontext{
    public string dbContext = "password=xxxxx; user=root; database=school; host=127.0.0.1";
}

};
