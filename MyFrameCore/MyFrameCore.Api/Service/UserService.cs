using MyFrameCore.Api.Common;
using MyFrameCore.Model.Extend;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyFrameCore.Model;

namespace MyFrameCore.Api.Service
{
    public class UserService
    {
        public UserExtend GetUserById(string KeyId)
        {
            using (var db = DapperDB.MySqlDB)
            {
                return db.QueryFirst<UserExtend>("select * from sys_user where KeyId=@KeyId", new { KeyId = KeyId });
            }
        }

        public sys_user GetUserInfo(string Account, string PassWord)
        {
            using (var db = DapperDB.MySqlDB)
            {
                return db.QueryFirst<sys_user>("select * from sys_user where Account=@Account and PassWord=@PassWord", new { Account = Account, PassWord = PassWord });
            }
        }
    }
}
