using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Student_CRUD_Razor_SP.Data;
using Student_CRUD_Razor_SP.Models;

namespace Student_CRUD_Razor_SP.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public IndexModel(ApplicationDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _db = context;
            _hostingEnvironment = hostingEnvironment;
        }

        public IList<Student> StudentList { get; set; } = default!;

        public async Task OnGetAsync()
        {
            //StudentList = await _db.Students.ToListAsync();
            StudentList = await _db.Students.FromSqlRaw("EXEC GetStudentList").ToListAsync();
        }
    }
}
