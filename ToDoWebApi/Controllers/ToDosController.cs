using Microsoft.AspNetCore.Mvc;
using ToDoWebApi.Context;
using ToDoWebApi.Entities;
using ToDoWebApi.Models;

namespace ToDoWebApi.Controllers
{
    // WebApi için genellikle çoğul isimde controller açılır.
    // ToDoController şeklinde tekil açılmasında da bir sıkıntı yok.

    [Route("api/[controller]")] // api/actionadı şeklinde istek atılacak. { değil [] kullanıyorum aman dikkat !

    [ApiController] // Bunun bir api controller olduğunu belirtmek için gerekli.

    public class ToDosController : Controller
    {
        // GET -> Kayıt ve kayıtları çekmek.
        // POST -> Yeni bir kayıt eklemek.
        // PATCH -> Olan bir kaydı güncellemek.
        // PUT -> Olan bir kaydı değiştirmek.
        // DELETE -> Olan bir kaydı silmek.


        private readonly ToDoContext _context;

        public ToDosController(ToDoContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AddToDo(AddToDoRequest request)
        {
            var entity = new ToDoEntity()
            {
                Title = request.Title,
                Content = request.Content
            };

            _context.ToDos.Add(entity);

            try
            {
                _context.SaveChanges();
                return Ok(); // statuscode -> 200
                // return StatusCode(200); aynı işlem
            }
            catch (Exception)
            {
                return StatusCode(500); // ServerError
            }

        }

        [HttpGet]
        public IActionResult GetAllToDos()
        {
            // YAPILACAKLAR -> _context.ToDos.Where(x => x.isDone == false)
            // HEPSI -> _context.ToDos

            var entites = _context.ToDos.ToList();
            return Ok(entites);

        }

        [HttpGet("{id}")]
        public IActionResult GetToDo(int id)
        {
            var entity = _context.ToDos.Find(id);

            if (entity is null)
                return NotFound(); // return StatusCode(404);


            return Ok(entity);
        }

        [HttpPatch("{id}")]
        public IActionResult CheckToDo(int id)
        {
            var entity = _context.ToDos.Find(id);

            if (entity is null)
                return NotFound();

            entity.IsDone = !entity.IsDone;
            // Yapıldı ise Yapılmadıya
            // Yapılmadı ise Yapıldıya çevir.

            _context.ToDos.Update(entity);

            try
            {
                _context.SaveChanges();
                return Ok(); // Success
            }
            catch (Exception)
            {

                return StatusCode(500); // ServerError
            }

        }

        [HttpPut("{id}")]
        public IActionResult UpdateToDo(int id, UpdateToDoRequest request)
        {
            var entity = _context.ToDos.Find(id);

            if (entity is null)
                return NotFound(); // StatusCode(404)

            entity.Title = request.Title;
            entity.Content = request.Content;

            _context.ToDos.Update(entity);
        
            try
            {
                _context.SaveChanges();
                return Ok(); // Success
            }
            catch (Exception)
            {

                return StatusCode(500); // ServerError
            }

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteToDo(int id)
        {
            var entity = _context.ToDos.Find(id);

            if (entity is null)
                return NotFound();

            _context.ToDos.Remove(entity);
            // DbContext üzerinde çalışan CRUD işlemi metotları entity ister.
            // Şimdiye kadar id ile silme işlemi yapabilmemizin nedeni, repository içerisinde kendi özel metodumuzu tanımlamamızdı.

            try
            {
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
                
            }

        }
    }
}

/*

_context.ToDos.FirstOrDefault(x => x.Id == 5);
_context.ToDos.SingleOrDefault(x => x.Id == 5);
_context.ToDos.Find(5);

FirstOrDefault -> İlk uyan veriyi bulana kadar arar.
SingeOrDefault -> ilk uyan veriyi bulur sonra başka var mı diye tüm listeyi tarar.
Find -> Direkt eşleşen Id'yi bulur.

O zaman performans açısından

Find > FirstOrDefault > SingleOrDefault


*/