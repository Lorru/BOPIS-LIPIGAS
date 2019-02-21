using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bopis_api.Models.Bopis;
using bopis_api.Services.Others;
using PagedList;

namespace bopis_api.Services.Bopis
{
    public class UserServiceImpl : IUserService
    {

        private ModelContext modelContext = new ModelContext();

        private ConfigurationServiceImpl configurationServiceImpl = new ConfigurationServiceImpl();

        private EmailServiceImpl emailServiceImpl = new EmailServiceImpl();

        private Random random = new Random();

        private string key = "SYSTEM";

        private string key2 = "BD";

        public UserServiceImpl()
        {

        }

        public User create(User user)
        {
            string passwordBCrypt = BCrypt.Net.BCrypt.HashPassword(user.Password);

            List<Configuration> configurations = configurationServiceImpl.findByKeyAndStatusEqualToOne(key2);

            long Id = Convert.ToInt64(configurations[0].Value);

            user.Password = passwordBCrypt;
            user.Status = true;
            user.Id = Id;

            modelContext.User.Add(user);
            modelContext.SaveChanges();

            Configuration configuration = new Configuration();

            configuration.Id = configurations[0].Id;
            configuration.Value = (Id + 1).ToString();

            configurationServiceImpl.updateValueByIdAndStatusEqualToOne(configuration);

            return user;
        }

        public List<User> findAll(string filter, int sort, string column)
        {
            List<User> users = null;

            if (filter != null)
            {
                users = (from u in modelContext.User
                         join p in modelContext.Profile on u.ProfileId equals p.Id
                         where (u.FullName.Contains(filter) || u.Run.Contains(filter))
                         select new User()
                         {
                             Email = u.Email,
                             FullName = u.FullName,
                             Id = u.Id,
                             Password = u.Password,
                             Profile = new Profile()
                             {
                                 Description = p.Description,
                                 Id = p.Id,
                                 Status = p.Status
                             },
                             ProfileId = u.ProfileId,
                             Run = u.Run,
                             Status = u.Status,
                             LocalId = u.LocalId
                         }).ToList();
            }
            else
            {
                users = (from u in modelContext.User
                         join p in modelContext.Profile on u.ProfileId equals p.Id
                         select new User()
                         {
                             Email = u.Email,
                             FullName = u.FullName,
                             Id = u.Id,
                             Password = u.Password,
                             Profile = new Profile()
                             {
                                 Description = p.Description,
                                 Id = p.Id,
                                 Status = p.Status
                             },
                             ProfileId = u.ProfileId,
                             Run = u.Run,
                             Status = u.Status,
                             LocalId = u.LocalId
                         }).ToList();
            }

            if (sort == 1 && column == "Id")
            {
                users = users.OrderBy(u => u.Id).ToList();
            }
            else if (sort == 0 && column == "Id")
            {
                users = users.OrderByDescending(u => u.Id).ToList();
            }
            else if (sort == 1 && column == "FullName")
            {
                users = users.OrderBy(u => u.FullName).ToList();
            }
            else if (sort == 0 && column == "FullName")
            {
                users = users.OrderByDescending(u => u.FullName).ToList();
            }
            else if (sort == 1 && column == "Run")
            {
                users = users.OrderBy(u => u.Run).ToList();
            }
            else if (sort == 0 && column == "Run")
            {
                users = users.OrderByDescending(u => u.Run).ToList();
            }
            else if (sort == 1 && column == "Email")
            {
                users = users.OrderBy(u => u.Email).ToList();
            }
            else if (sort == 0 && column == "Email")
            {
                users = users.OrderByDescending(u => u.Email).ToList();
            }
            else if (sort == 1 && column == "Profile")
            {
                users = users.OrderBy(u => u.Profile.Description).ToList();
            }
            else if (sort == 0 && column == "Profile")
            {
                users = users.OrderByDescending(u => u.Profile.Description).ToList();
            }

            return users;
        }

        public int findAllCount()
        {
            List<User> users = (from u in modelContext.User
                                select u).ToList();

            return users.Count;
        }

        public List<User> findAllPaged(int page, string filter, int sort, string column)
        {
            List<Configuration> configurations = configurationServiceImpl.findByKeyAndStatusEqualToOne(key);

            int size = Convert.ToInt32(configurations[3].Value);

            List<User> users = null;

            if (filter != null)
            {
                users = (from u in modelContext.User
                         join p in modelContext.Profile on u.ProfileId equals p.Id
                         where (u.FullName.Contains(filter) || u.Run.Contains(filter))
                         select new User()
                         {
                             Email = u.Email,
                             FullName = u.FullName,
                             Id = u.Id,
                             Password = u.Password,
                             Profile = new Profile()
                             {
                                 Description = p.Description,
                                 Id = p.Id,
                                 Status = p.Status
                             },
                             ProfileId = u.ProfileId,
                             Run = u.Run,
                             Status = u.Status,
                             LocalId = u.LocalId
                         }).ToList();
            }
            else
            {
                users = (from u in modelContext.User
                         join p in modelContext.Profile on u.ProfileId equals p.Id
                         select new User()
                         {
                             Email = u.Email,
                             FullName = u.FullName,
                             Id = u.Id,
                             Password = u.Password,
                             Profile = new Profile()
                             {
                                 Description = p.Description,
                                 Id = p.Id,
                                 Status = p.Status
                             },
                             ProfileId = u.ProfileId,
                             Run = u.Run,
                             Status = u.Status,
                             LocalId = u.LocalId
                         }).ToList();
            }

            if (sort == 1 && column == "Id")
            {
                users = users.OrderBy(u => u.Id).ToList();
            }
            else if (sort == 0 && column == "Id")
            {
                users = users.OrderByDescending(u => u.Id).ToList();
            }
            else if (sort == 1 && column == "FullName")
            {
                users = users.OrderBy(u => u.FullName).ToList();
            }
            else if (sort == 0 && column == "FullName")
            {
                users = users.OrderByDescending(u => u.FullName).ToList();
            }
            else if (sort == 1 && column == "Run")
            {
                users = users.OrderBy(u => u.Run).ToList();
            }
            else if (sort == 0 && column == "Run")
            {
                users = users.OrderByDescending(u => u.Run).ToList();
            }
            else if (sort == 1 && column == "Email")
            {
                users = users.OrderBy(u => u.Email).ToList();
            }
            else if (sort == 0 && column == "Email")
            {
                users = users.OrderByDescending(u => u.Email).ToList();
            }
            else if (sort == 1 && column == "Profile")
            {
                users = users.OrderBy(u => u.Profile.Description).ToList();
            }
            else if (sort == 0 && column == "Profile")
            {
                users = users.OrderByDescending(u => u.Profile.Description).ToList();
            }

            users = users.ToPagedList(page, size).ToList();

            return users;
        }

        public List<User> findAllStatusEqualToOne()
        {
            List<User> users = (from u in modelContext.User
                                where u.Status == true
                                select u).ToList();

            return users;
        }

        public User findByEmailAndStatusEqualToOne(string email)
        {
            User user = (from u in modelContext.User
                         where u.Email == email && u.Status == true
                         select u).FirstOrDefault();

            return user;
        }

        public User findByRunAndEmail(string run, string email)
        {
            User user = (from u in modelContext.User
                         where u.Run == run || u.Email == email
                         select u).FirstOrDefault();

            return user;
        }

        public User login(User user)
        {
            User userExisting = (from u in modelContext.User
                                 where u.Run == user.Run && u.Status == true
                                 select u).FirstOrDefault();

            if (userExisting != null)
            {
                bool validatePassword = BCrypt.Net.BCrypt.Verify(user.Password, userExisting.Password);

                if (validatePassword)
                {
                    return userExisting;
                }
                else
                {
                    userExisting = null;

                    return userExisting;
                }
            }

            return userExisting;
        }

        public User updatebyIdAndStatusEqualToOne(User user)
        {
            User userExisting = (from u in modelContext.User
                                 where u.Id == user.Id && u.Status == true
                                 select u).FirstOrDefault();

            userExisting.Email = user.Email;
            userExisting.FullName = user.FullName;
            userExisting.LocalId = user.LocalId;
            userExisting.ProfileId = user.ProfileId;
            userExisting.Run = user.Run;

            modelContext.SaveChanges();

            return userExisting;
        }

        public User updatePasswordByEmailAndStatusEqualToOne(User user)
        {
            List<Configuration> configurations = configurationServiceImpl.findByKeyAndStatusEqualToOne(key);

            string characters = configurations[2].Value;
            int length = Convert.ToInt32(configurations[1].Value);

            string passwordRandom = new string(Enumerable.Repeat(characters, length).Select(s => s[random.Next(s.Length)]).ToArray());
            string passwordBCrypt = BCrypt.Net.BCrypt.HashPassword(passwordRandom);

            User userExisting = (from u in modelContext.User
                                 where u.Email == user.Email && u.Status == true
                                 select u).FirstOrDefault();

            userExisting.Password = passwordBCrypt;

            modelContext.SaveChanges();

            userExisting.Password = passwordRandom;

            emailServiceImpl.sendRecoveryPassword(userExisting);

            return userExisting;

        }

        public User updatePasswordByIdAndStatusEqualToOne(User user)
        {
            User userExisting = (from u in modelContext.User
                                 where u.Id == user.Id && u.Status == true
                                 select u).FirstOrDefault();

            string passwordBCrypt = BCrypt.Net.BCrypt.HashPassword(user.Password);

            userExisting.Password = passwordBCrypt;

            modelContext.SaveChanges();

            return userExisting;
        }

        public User updateStatusFindById(User user)
        {
            User userExisting = (from u in modelContext.User
                                 where u.Id == user.Id
                                 select u).FirstOrDefault();

            userExisting.Status = user.Status;

            modelContext.SaveChanges();

            return userExisting;
        }
    }
}
