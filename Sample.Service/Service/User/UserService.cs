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