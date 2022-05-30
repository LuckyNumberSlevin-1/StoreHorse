using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Store.CORE.EF_dbContext;
using Store.DATA.Repositories;
using Store.DATA.Models;
using Store.DATA.Dto;
using Store.DATA.Converters;

namespace Store.CORE.Repo_App
{
    public class UserRepository : IUserRepository
    {
        ApplicationContext _context;
        public UserRepository(ApplicationContext context) => _context = context;
        public async Task<UserDto> CreateAsync(UserDto item)
        {
            try
            {
                var result = await _context.StoreUsers.AddAsync(UserConverter.Convert(item));
                await _context.SaveChangesAsync();
                return UserConverter.Convert(result.Entity);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var user = await _context.StoreUsers.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);
                if (user == null)
                    return false;

                foreach (var item in (await _context.Customers.AsNoTracking()
                    .Where(x => x.UserId == user.Id).ToListAsync())) 
                {
                    item.UserId = null;
                    _context.Customers.Update(item);
                }

                await _context.SaveChangesAsync();
                _context.StoreUsers.Remove(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<UserDto>> GetAllAsync()
        {
            try
            {
                return await _context.StoreUsers.AsNoTracking().Select(x => new UserDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Surname = x.Surname
                }).ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<UserDto> GetByIdAsync(Guid id)
        {
            try
            {

                return await _context.StoreUsers.AsNoTracking().Select(x => new UserDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Surname = x.Surname
                }).FirstOrDefaultAsync(y => y.Id == id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> UpdateAsync(UserDto item)
        {
            try
            {
                var user = await _context.StoreUsers.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == item.Id);
                if (user == null)
                    return false;

                _context.StoreUsers.Update(UserConverter.Convert(item));
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
