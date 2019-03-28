using System;

namespace CounterCulture.Auth.Helpers
{
    public class UserRepoProxy<T>
    {
        public static T Create(T UserRepo){
          return UserRepo;
        }
    }
}