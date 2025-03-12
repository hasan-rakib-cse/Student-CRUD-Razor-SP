using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Student_CRUD_Razor_SP.Data;
using Student_CRUD_Razor_SP.Models;

namespace Student_CRUD_Razor_SP.Pages.Students
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public CreateModel(ApplicationDbContext context)
        {
            _db = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Student Std { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _db.Database.ExecuteSqlRawAsync("EXEC CreateStudent @Name, @Email, @Phone, @Subscribed",
                //Std.Name, Std.Email, Std.Phone, Std.Subscribed);
                new SqlParameter("@Name", Std.Name),
                new SqlParameter("@Email", Std.Email),
                new SqlParameter("@Phone", Std.Phone),
                new SqlParameter("@Subscribed", Std.Subscribed));

            return RedirectToPage("./Index");
        }
    }
}
