using System;
using System.IO;
using System.Reflection;
using System.Threading;
using CounterCulture.Repositories;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace CounterCulture.Helpers
{
    public class RepoProxy<T> : DispatchProxy
    {
      private T _repo;

      public static T Create(T repo){
        object repoProxy = Create<T, RepoProxy<T>>();
        ((RepoProxy<T>)repoProxy).SetParameters(repo);
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
            Thread.Sleep(1000);
            return targetMethod.Invoke(repo.Reconnect(), args);
          } else {
            throw ex;
          }
        }
      }
    }
}