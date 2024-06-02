using System.Data.SqlClient;
using Cwiczenia_5.Controllers;
using Cwiczenia_5.Models;

namespace Cwiczenia_5.Repositories;

public class AnimalRepository : IAnimalRepository
{
    private static readonly string[] orderOptions = { "name", "description", "category", "area" };

    private readonly IConfiguration _configuration; // dodane readonly - gwarancja, ze nikt nie zmieni na null

    public AnimalRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IEnumerable<Animal> GetAnimals(string orderBy)
    {
        if (!orderOptions.Contains(orderBy))
            orderBy = "name";


        using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();

        using var command = new SqlCommand();
        command.Connection = connection;
        command.CommandText =
            "SELECT IdAnimal, Name, Description, Category, Area FROM Animal ORDER BY @orderBy";

        var dataReader = command.ExecuteReader();
        var animals = new List<Animal>();

        while (dataReader.Read())
        {
            var animal = new Animal
            {
                IdAnimal = (int)dataReader["IdAnimal"],
                Name = dataReader["Name"].ToString(),
                Description = dataReader["Description"].ToString(),
                Category = dataReader["Category"].ToString(),
                Area = dataReader["Area"].ToString()
            };
            animals.Add(animal);
        }

        return animals;
    }

    public Animal GetAnimal(int idAnimal)
    {
        using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();

        using var command = new SqlCommand();
        command.Connection = connection;
        command.CommandText =
            "SELECT IdAnimal, Name, Description, Category, Area FROM Animal WHERE IdAnimal = @idAnimal";

        command.Parameters.AddWithValue("IdAnimal", idAnimal);

        var dataReader = command.ExecuteReader();

        if (!dataReader.Read()) return null;

        var animal = new Animal
        {
            IdAnimal = (int)dataReader["IdAnimal"],
            Name = dataReader["Name"].ToString(),
            Description = dataReader["Description"].ToString(),
            Category = dataReader["Category"].ToString(),
            Area = dataReader["Area"].ToString()
        };

        return animal;
    }


    public int CreateAnimal(Animal animal)
    {
        using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();

        using var command = new SqlCommand();
        command.Connection = connection;
        command.CommandText =
            "INSERT INTO Animal(IdAnimal, Name, Description, Category, Area) VALUES (@IdAnimal, @Name, @Description, @Category, @Area)";
        command.Parameters.AddWithValue("IdAnimal", animal.IdAnimal);
        command.Parameters.AddWithValue("Name", animal.Name);
        command.Parameters.AddWithValue("Description", animal.Description);
        command.Parameters.AddWithValue("Category", animal.Category);
        command.Parameters.AddWithValue("Area", animal.Area);

        var affectedCount = command.ExecuteNonQuery();
        return affectedCount;
    }

    public int UpdateAnimal(Animal animal)
    {
        using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();

        using var command = new SqlCommand();
        command.Connection = connection;
        command.CommandText =
            "UPDATE Animal SET Name=@Name, Description=@Description, Category=@Category, Area=@Area WHERE IdAnimal = @idAnimal";
        command.Parameters.AddWithValue("Name", animal.Name);
        command.Parameters.AddWithValue("Description", animal.Description);
        command.Parameters.AddWithValue("Category", animal.Category);
        command.Parameters.AddWithValue("Area", animal.Area);

        var affectedCount = command.ExecuteNonQuery();
        return affectedCount;
    }

    public int DeleteAnimal(int idAnimal) //int zmienic na void
    {
        using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();

        using var command = new SqlCommand();
        command.Connection = connection;
        command.CommandText =
            "DELETE FROM Animal WHERE IdAnimal = @IdAnimal";

        command.Parameters.AddWithValue("@IdAnimal", idAnimal);

        var affectionCount = command.ExecuteNonQuery();
        return affectionCount;
    }
}