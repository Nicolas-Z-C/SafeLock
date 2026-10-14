using SafeLock.Domain.Common.Entities;
using SafeLock.Domain.Common.Results;
using SafeLock.Domain.Common.ValueObjects;

namespace SafeLock.Domain.Aggregates.Users
{
    public class User : UserEntity
    {
        private User () {}
        private User(Name name,
        Email email,
        PasswordHash password,
        ImageUrl image) :base(name,email,password,image) {}
        public static ResultGen<User> Create(
            string name,
            string email,
            string password,
            string pfpurl
        )
        {
    
            List<Result> all = [];

            var resultName = Name.Create(name);
            all.Add(resultName);
            var resultEmail = Email.Create(email);
            all.Add(resultEmail);
            var resultPassword = PasswordHash.Create(password);
            all.Add(resultPassword);
            var resultPFP = ImageUrl.Create(pfpurl);
            all.Add(resultPFP);

            var combined = Result.Combine([.. all]);

            var errors = new List<Error>(combined.Errors);

            if(errors.Count != 0)
                return ResultGen<User>.Failure(errors);
            

            return ResultGen<User>.Success(new User(
                resultName.Value,
                resultEmail.Value,
                resultPassword.Value,
                resultPFP.Value
            ));
        }
    }
}