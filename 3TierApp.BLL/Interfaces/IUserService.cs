using _3TierApp.BLL.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3TierApp.BLL.Interfaces
{
    public interface IUserService
    {
        Task CreateUserAsync(UserDTO userDTO);
        Task AddAccessTimeAsync(AccessTimeDTO userDTO);
        Task<IEnumerable<AccessTimeDTO>> ShowAccessTimeAsync(int? id);
        Task DeleteUserAsync(int? id);
        Task UpdateUserAsync(UserDTO user);
        Task<UserDTO> GetUserAsync(int? id);
        Task<IEnumerable<UserDTO>> GetUsersAsync();
        void Dispose();
    }
}
