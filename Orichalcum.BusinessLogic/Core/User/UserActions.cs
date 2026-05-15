using Orichalcum.DataAccess.Context;
using Orichalcum.Domains.Entities.User;
using Orichalcum.Domains.Enums;
using Orichalcum.Domains.Models.User;

namespace Orichalcum.BusinessLogic.Core.User
{
    public class UserActions
    {
        public List<UserData> ExecuteGetAllUsersAction()
        {
            using (var db = new DatabaseContext())
            {
                return db.Users.ToList();
            }
        }

        public UserData? ExecuteGetUserByIdAction(int id)
        {
            using (var db = new DatabaseContext())
            {
                return db.Users.FirstOrDefault(x => x.Id == id);
            }
        }

        public UserData? ExecuteCreateUserAction(UserData user)
        {
            using (var db = new DatabaseContext())
            {
                if (db.Users.Any(x => x.Email == user.Email))
                    return null;

                var _newUser = new UserData()
                {
                    UserName = user.UserName,
                    Password = user.Password,
                    Email = user.Email,
                    Bio = user.Bio,
                    AvatarUrl = user.AvatarUrl,
                    Role = user.Role,
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };
                db.Users.Add(_newUser);
                db.SaveChanges();
                return _newUser;
            }
        }

        public bool ExecuteDeleteUserAction(int id)
        {
            using (var db = new DatabaseContext())
            {
                var _user = db.Users.FirstOrDefault(x => x.Id == id && x.IsActive == true);
                if (_user == null) return false;

                _user.IsActive = false;
                db.SaveChanges();
                return true;
            }
        }

        public UserData? ExecuteUpdateUserAction(int id, UpdateUserDto dto)
        {
            using (var db = new DatabaseContext())
            {
                var _user = db.Users.FirstOrDefault(x => x.Id == id && x.IsActive == true);
                if (_user == null) return null;

                if (dto.Email != null && db.Users.Any(u => u.Email == dto.Email && u.Id != id))
                    return null;

                if (dto.UserName != null) _user.UserName = dto.UserName;
                if (dto.Email != null) _user.Email = dto.Email;
                if (dto.Bio != null) _user.Bio = dto.Bio;
                if (dto.AvatarUrl != null) _user.AvatarUrl = dto.AvatarUrl;
                if (dto.FirstName != null) _user.FirstName = dto.FirstName;
                if (dto.LastName != null) _user.LastName = dto.LastName;
                if (!string.IsNullOrEmpty(dto.Password)) _user.Password = dto.Password;
                if (dto.Role.HasValue) _user.Role = (UserRole)dto.Role.Value;

                _user.UpdatedAt = DateTime.Now;
                db.SaveChanges();
                return _user;
            }
        }

        public bool ExecuteHardDeleteUserAction(int id)
        {
            using (var db = new DatabaseContext())
            {
                var _user = db.Users.FirstOrDefault(x => x.Id == id);
                if (_user == null) return false;
                db.Users.Remove(_user);
                db.SaveChanges();
                return true;
            }
        }
    }
}