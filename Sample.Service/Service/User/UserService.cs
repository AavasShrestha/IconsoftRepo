using CBS.Data.DTO;
using CBS.Data.TenantDB;
using CBS.Repository;

namespace CBS.Service
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    
        // New CRUD methods
        public UserDetail CreateUser(int userId, UserDetail userDetail)
        {
            var userEntity = new User
            {
                Username = userDetail.Username,
                Password = userDetail.Password,
                ConfirmPassword = userDetail.ConfirmPassword,
                CreatedBy = userId,
                CreatedDate = DateTime.Now,
                Remarks = userDetail.Remarks,
                Phone = userDetail.Phone,
                FullName = userDetail.FullName,
                IsActive = userDetail.IsActive,
                Branchid = userDetail.Branchid
            };

            _unitOfWork.UserRepository.Add(userEntity);
            _unitOfWork.Commit();

            userDetail.Id = userEntity.Id;
            return userDetail;
        }

        public UserDetail UpdateUser(int id, UserDetail userDetail)
        {
            var existingUser = _unitOfWork.UserRepository.GetQuerable(u => u.Id == id).FirstOrDefault();
            if (existingUser == null)
                return null;

            existingUser.Username = userDetail.Username;
            if (!string.IsNullOrEmpty(userDetail.Password))
                existingUser.Password = userDetail.Password;
            existingUser.FullName = userDetail.FullName;
            existingUser.IsActive = userDetail.IsActive;
            existingUser.Branchid = userDetail.Branchid;

            _unitOfWork.UserRepository.Update(existingUser);
            _unitOfWork.Commit();

            userDetail.Id = existingUser.Id;
            return userDetail;
        }

        public bool DeleteUser(int id)
        {
            var existingUser = _unitOfWork.UserRepository.GetQuerable(u => u.Id == id).FirstOrDefault();
            if (existingUser == null)
                return false;

            _unitOfWork.UserRepository.Delete(existingUser);
            _unitOfWork.Commit();

            return true;
        }


        public IEnumerable<UserDetail> GetAllUsers()
        {
            return _unitOfWork.UserRepository.GetQuerable()
                .Select(u => new UserDetail
                {
                    Id = u.Id,
                    Username = u.Username,
                    FullName = u.FullName,
                    IsActive = u.IsActive,
                    Branchid = u.Branchid
                }).ToList();
        }

        public UserDetail GetUserById(int id)
        {
            var user = _unitOfWork.UserRepository
                    .GetQuerable(u => u.Id == id)
         .FirstOrDefault();

            if (user == null) return null;

            return new UserDetail
            {
                Id = user.Id,
                Username = user.Username,
                FullName = user.FullName,
                IsActive = user.IsActive,
                Branchid = user.Branchid
            };
        }

        // To login for user (avash and password).

        public UserDetail GetLoggedInUserDetail(string userName, string encryptedPassword)
        {
            var userData = (from u in _unitOfWork.UserRepository.GetQuerable(q => q.Username == userName && q.Password == encryptedPassword)
                            select
                    new UserDetail()
                    {
                        Id = u.Id,
                        IsActive = u.IsActive,
                        //Address = u.Address,
                        Branchid = u.Branchid,
                        Username = u.Username,
                        FullName = u.FullName
                    }).FirstOrDefault();

            return userData;
        }

      
        public bool UserAuthentication(int userId, string password)
        {
            return _unitOfWork.UserRepository.GetQuerable(q => q.Id == userId && q.Password == password).Any();
        }




    }
}