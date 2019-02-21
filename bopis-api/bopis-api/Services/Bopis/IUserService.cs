using bopis_api.Models.Bopis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bopis_api.Services.Bopis
{
    interface IUserService
    {
        User login(User user);

        User updatePasswordByEmailAndStatusEqualToOne(User user);

        User findByEmailAndStatusEqualToOne(string email);

        int findAllCount();

        List<User> findAll(string filter, int sort, string column);

        List<User> findAllPaged(int page, string filter, int sort, string column);

        User updateStatusFindById(User user);

        User updatePasswordByIdAndStatusEqualToOne(User user);

        User updatebyIdAndStatusEqualToOne(User user);

        User create(User user);

        User findByRunAndEmail(string run, string email);

        List<User> findAllStatusEqualToOne();
    }
}
