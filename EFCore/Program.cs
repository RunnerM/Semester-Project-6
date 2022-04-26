// See https://aka.ms/new-console-template for more information

using Data;
using EFCore.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

await using var context = new Context();


var query = await context.Set<Movie>()
    .Where(m => m.Title.Contains("Star Wars")).ToListAsync();
    
Console.WriteLine(query.Count);

String connectionString = "";

using (SqlConnection connection = new SqlConnection(connectionString))
{
    List<Star> stars = new List<Star>();
    
    connection.Open();
    SqlCommand command = new SqlCommand("SELECT * FROM Movies", connection);
    SqlDataReader reader = command.ExecuteReader();
    Star star = new Star();
    object[] values= new object[reader.FieldCount];
    reader.GetValues(values);
    // Do work here; connection closed on following line.
}