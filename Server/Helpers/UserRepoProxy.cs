using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using CounterCulture.Repositories;
using MySql.Data.MySqlClient;

namespace CounterCulture.Helpers
{
    public class UserRepoProxy<T> : DispatchProxy
    {
      private T _userRepo;

      public static T Create(T UserRepo){
        object userRepoProxy = Create<T, UserRepoProxy<T>>();
        ((UserRepoProxy<T>)userRepoProxy).SetParameters(UserRepo);
        return (T)userRepoProxy;
      }

      private void SetParameters(T userRepo){
        if(userRepo == null){
          throw new ArgumentNullException(nameof(userRepo));
        }
        _userRepo = userRepo;
      }

      protected override object Invoke(MethodInfo targetMethod, object[] args) {
        try
        {
          return targetMethod.Invoke(_userRepo, args);
        } catch (Exception ex) {
          IUserRepository repo = _userRepo as UserRepository;
          if(repo.IsDisconnected){
            Thread.Sleep(1000);
            repo = repo.Reconnect();
            return targetMethod.Invoke(repo, args);
          } else {
            throw ex;
          }
        }
      }
    }
}