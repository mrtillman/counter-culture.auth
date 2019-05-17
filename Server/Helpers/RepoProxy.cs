using System;
using System.IO;
using System.Reflection;
using System.Threading;
using CounterCulture.Repositories;
using MySql.Data.MySqlClient;

namespace CounterCulture.Helpers
{
    public class RepoProxy<T> : DispatchProxy
    {
      private T _repo;

      public static T Create(T UserRepo){
        object repoProxy = Create<T, RepoProxy<T>>();
        ((RepoProxy<T>)repoProxy).SetParameters(UserRepo);
        return (T)repoProxy;
      }

      private void SetParameters(T repo){
        if(repo == null){
          throw new ArgumentNullException(nameof(repo));
        }
        _repo = repo;
      }

      protected override object Invoke(MethodInfo targetMethod, object[] args) {
        try
        {
          return targetMethod.Invoke(_repo, args);
        } catch (Exception ex) {

          var repo =  _repo as IBaseRepository;

          if(repo.IsDisconnected){
            Thread.Sleep(10000);
            if(_repo.GetType() == typeof(IOAuthRepository)){
              return targetMethod.Invoke(
                ((IOAuthRepository)repo).Reconnect(), args);
            } 
            else 
            if(_repo.GetType() == typeof(IUserRepository)){
              return targetMethod.Invoke(
                ((IUserRepository)repo).Reconnect(), args);
            }
            else{
              throw ex;
            }
          } else {
            throw ex;
          }
        }
      }
    }
}