using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Student_CRUD_Razor_SP.Data;
using Student_CRUD_Razor_SP.Models;

namespace Student_CRUD_Razor_SP.Pages.Students
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public DetailsModel(ApplicationDbContext context)
        {
            _db = context;
        }

        public Student Std { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //var student = await _db.Students.FirstOrDefaultAsync(s => s.Id == id);
            var student = await _db.Students.FromSqlRaw("EXEC GetStudentById @Id", new SqlParameter("@Id", id)).ToListAsync();
            if (student == null)
            {
                return NotFound();
            }
            Std = student.First();
            return Page();
        }
    }
}
