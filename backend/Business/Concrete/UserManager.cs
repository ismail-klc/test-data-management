using Business.Abstract;
using Core.DataAccess.MongoDb;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IMongoRepository<User> _userRepository;
        public UserManager(IMongoRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        public void Add(User user)
        {
            try
            {
                _userRepository.InsertOneAsync(user);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Task<User> GetByMail(string email)
        {
            try
            {
                return  _userRepository.FindOneAsync(u => u.Email == email);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
