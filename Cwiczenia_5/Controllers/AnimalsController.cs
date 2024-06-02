using Cwiczenia_5.Models;
using Cwiczenia_5.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Cwiczenia_5.Controllers;

[ApiController]
[Route("[controller]")]
public class AnimalsController : ControllerBase
{
    //wstrzykuje repozytorium, ktore stworzylem`

    private readonly IAnimalRepository _animalRepository;
    
    public AnimalsController(IAnimalRepository animalRepository)
    {
        _animalRepository = animalRepository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Animal>>
        GetAnimals(string? orderBy = "name") // dzieki temu w przypadku nulla sortuje po name
    {
        return Ok(_animalRepository.GetAnimals(orderBy!)); //!, aby orderBy nie byl nigdy nullem
    }

    [HttpPost]
    public ActionResult CreateAnimal(Animal animal) // wywalilem operator diamentu, bo zwracam id
    {
        return CreatedAtAction(null, null, new {Id =_animalRepository.CreateAnimal(animal) });// zwraca json z utworzonym id
        //
    }

    [HttpPut("{id}")]
    public IActionResult UpdateAnimal(int id, Animal animal)
    {
        animal.IdAnimal = id;

        _animalRepository.UpdateAnimal(animal);

        return Ok();
    }
    
    [HttpDelete("{id}")]
    public IActionResult DeleteAnimal(int id)
    {

        _animalRepository.DeleteAnimal(id);

        return NoContent(); // zasób został usunięty, nie masz co zwrócić
    }
}