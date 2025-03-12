using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Student_CRUD_Razor_SP.Data;
using Student_CRUD_Razor_SP.Models;

namespace Student_CRUD_Razor_SP.Pages.Students
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public EditModel(ApplicationDbContext context)
        {
            _db = context;
        }

        [BindProperty]
        public Student Std { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // way 1
            var student = await _db.Students.FromSqlRaw("EXEC GetStudentById @Id", new SqlParameter("@Id", id)).ToListAsync();
            if (student == null || student.Count == 0)
            {
                return NotFound();
            }
            Std = student.First(); // Assigning the first student from the result

            // way 2
            //Std = await _db.Students.FromSqlRaw("EXEC GetStudentById @Id", new SqlParameter("@Id", id))
            //                        .AsNoTracking()
            //                        .SingleOrDefaultAsync();
            //if (Std == null)
            //{
            //    return NotFound();
            //}

            // way 3 with EF Core
            //var student = await _db.Students.FirstOrDefaultAsync(s => s.Id == id);
            //if (student == null)
            //{
            //    return NotFound();
            //}
            //Std = student;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                await _db.Database.ExecuteSqlRawAsync("EXEC UpdateStudent @Id, @Name, @Email, @Phone, @Subscribed",
                    new SqlParameter("@Id", Std.Id),
                    new SqlParameter("@Name", Std.Name),
                    new SqlParameter("@Email", Std.Email),
                    new SqlParameter("@Phone", Std.Phone),
                    new SqlParameter("@Subscribed", Std.Subscribed));
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                ModelState.AddModelError("", "An error occurred while updating the student record.");
                return Page();
            }
            return RedirectToPage("./Index");
        }
    }
}
