using HansViolinWebApp.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace HansViolinWebApp.Models.Repository
{
    public class AdminUserRepository
    {
        private readonly HansViolinWebContext _context;

        public AdminUserRepository(HansViolinWebContext context)
        {
            _context = context;
        }

        public async Task<AdminUser> getUser()
        {
            return await _context.AdminUsers.FirstOrDefaultAsync();
        }

        public async Task createUser(AdminUser adminUser)
        {
            try
            {
                _context.AdminUsers.Add(adminUser);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
