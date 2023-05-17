using _3TierApp.BLL.DTO;
using _3TierApp.BLL.Infrastructure;
using _3TierApp.BLL.Interfaces;
using _3TierApp.DAL.Entities;
using _3TierApp.DAL.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3TierApp.BLL.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork Database { get; set; }

        public UserService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public async Task CreateUserAsync(UserDTO userDTO)
        {
            if (userDTO == null)
                throw new ValidationException("userDTO is null", "");
            if (userDTO.Name == null)
                throw new ValidationException("Name is null", "");
            //if (userDTO.Birth == null)
            //    throw new ValidationException("Birth is null", "");
            if (userDTO.Role != 0 & userDTO.Role != 1)
                throw new ValidationException("Role is null", "");

            User user = new User
            {
                Name = userDTO.Name,
                Birth = userDTO.Birth,
                Role = userDTO.Role
            };
            await Database.Users.CreateAsync(user);
            Database.Save();
        }

        public async Task AddAccessTimeAsync(AccessTimeDTO accessTimeDTO)
        {
            if (accessTimeDTO == null)
                throw new ValidationException("userDTO is null", "");
            User user = await Database.Users.GetAsync(accessTimeDTO.UserId);
            if (user == null)
                throw new ValidationException("user not found", "");

            //Console.WriteLine(accessTimeDTO.UserId + " " + accessTimeDTO.AccessDateTime);
            AccessTime accessTime = new AccessTime
            {
                UserId = user.ID,
                AccessDateTime = accessTimeDTO.AccessDateTime
            };
            await Database.AccessTimes.CreateAsync(accessTime);
            Database.Save();
        }

        public async Task UpdateUserAsync(UserDTO userDTO)
        {
            if (userDTO == null)
                throw new ValidationException("userDTO is null", "");
            if (userDTO.Name == null)
                throw new ValidationException("Name is null", "");

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, User>()).CreateMapper();
            var user = mapper.Map<UserDTO, User>(userDTO);

            await Database.Users.UpdateAsync(user);
            Database.Save();
        }

        public async Task DeleteUserAsync(int? id)
        {
            if (id == null)
                throw new ValidationException("id not set", "");

            await Database.Users.DeleteAsync(id ?? default(int));
            Database.Save();
        }

        public async Task<IEnumerable<AccessTimeDTO>> ShowAccessTimeAsync(int? id)
        {
            if (id == null)
                throw new ValidationException("id not set", "");
            
            var accessTime = await Database.AccessTimes.FindAsync(x => x.UserId == id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<AccessTime, AccessTimeDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<AccessTime>, List<AccessTimeDTO>>(accessTime);
        }

        

        public async Task<UserDTO> GetUserAsync(int? id)
        {
            if (id == null)
                throw new ValidationException("id is null", "");

            var user = await Database.Users.GetAsync(id.Value);

            if (user == null)
                throw new ValidationException("User not found", "");

            return new UserDTO { ID = user.ID, Name = user.Name, Birth = user.Birth,
                Role = user.Role };
        }

        public async Task<IEnumerable<UserDTO>> GetUsersAsync()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()).CreateMapper();
            var users = await Database.Users.GetAllAsync();
            return mapper.Map<IEnumerable<User>, List<UserDTO>>(users);
        }
        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
