using Microsoft.AspNetCore.Mvc;
using TodoAppMVC.Data;
using TodoAppMVC.Models;
using System.Linq;

namespace TodoAppMVC.Controllers
{
    public class TodoController : Controller
    {
        // Veritabanı köprümüzü tanımlıyoruz.
        private readonly AppDbContext _context;

        // Constructor (yapıcı metot) ile bağlantıyı içeriye alıyoruz.
        public TodoController(AppDbContext context)
        {
            _context = context;
        }

        // --- LİSTELEME (READ) ---
        public IActionResult Index()
        {
            var todos = _context.Todos.ToList();
            return View(todos);
        }

        // --- EKLEME (CREATE) ---

        // 1. GET: Form Sayfasını Ekrana Getiren Metot
        public IActionResult Create()
        {
            return View();
        }

        // 2. POST: Formdan Gelen Veriyi Veritabanına Kaydeden Metot
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Todo todo)
        {
            if (ModelState.IsValid)
            {
                _context.Todos.Add(todo);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(todo);
        }

        // --- DÜZENLEME (EDIT) ---

        // GET: Todo/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todo = _context.Todos.Find(id);

            if (todo == null)
            {
                return NotFound();
            }
            return View(todo);
        }
        // POST: Formdan Gelen Güncel Veriyi Veritabanına Kaydeden Metot
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Todo todo)
        {
            // URL'deki ID ile güncellenmek istenen kaydın ID'si eşleşiyor mu kontrolü
            if (id != todo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Veritabanındaki kaydı güncelliyoruz
                _context.Todos.Update(todo);
                _context.SaveChanges();

                // Başarılı olursa listeye geri dön
                return RedirectToAction(nameof(Index));
            }
            return View(todo);
        }
    
    // GET: Görevi Veritabanından Silen Metot
        public IActionResult Delete(int? id)
        {
            // 1. Silinecek ID bize gönderilmiş mi?
            if (id == null)
            {
                return NotFound();
            }

            // 2. Veritabanına git ve o ID'ye sahip görevi bul
            var todo = _context.Todos.Find(id);

            // 3. Eğer öyle bir görev bulunamazsa hata ver
            if (todo == null)
            {
                return NotFound();
            }

            // 4. Görevi veritabanından tamamen sil
            _context.Todos.Remove(todo);

            // 5. Değişiklikleri kaydet
            _context.SaveChanges();

            // 6. İşlem başarıyla bitince listeleme (Index) sayfasına geri dön
            return RedirectToAction(nameof(Index));
        }
    }
}

