using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SYTD_4_Examples.DatabaseRaw;

const string ConnectionString = @"Data Source=C:\temp\sytd\raw.sqlite;Version=3;";

var student = new Student
{
    FirstName = "Max",
    LastName = "Mustermann",
    Birthday = DateTime.Now
};

var students = AddToDatabaseSimple(student);
//var students = AddToDatabaseEF(student);

foreach(var item in students)
    Console.WriteLine($"{item.Id}. {item.FirstName} {item.LastName} {item.Birthday}");


IEnumerable<Student> AddToDatabaseSimple(Student student)
{
    using var connection = new SqliteConnection(ConnectionString);
    using var insertCommand = connection.CreateCommand();

    // TODO delete database

    insertCommand.CommandText = "INSERT INTO Students (FirstName, LastName, Birthday) VALUES "
        + $" ({student.Id}, '{student.FirstName}', {student.LastName}, {student.Birthday}";

    insertCommand.ExecuteNonQuery();

    using var selectCommand = connection.CreateCommand();

    selectCommand.CommandText = "SELECT * FROM Students WHERE Birthday < NOW()";

    var reader = selectCommand.ExecuteReader();

    while (reader.Read())
    {
        yield return new Student
        {
            Id = reader.GetInt32(0),
            FirstName = reader.GetString(1),
            LastName = reader.GetString(2),
            Birthday = reader.GetDateTime(3)
        };
    }
}

IEnumerable<Student> AddToDatabaseEF(Student student)
{
    var options = new DbContextOptionsBuilder().UseSqlite(ConnectionString).Options;

    using var context = new StudentContext(options);

    context.Students.Add(student);

    context.SaveChanges();

    return context.Students.Where(s => s.Birthday < DateTime.Now.Date);
}