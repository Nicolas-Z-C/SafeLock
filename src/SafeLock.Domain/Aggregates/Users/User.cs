using SafeLock.Domain.Common.Entities;
using SafeLock.Domain.Common.Result;
using SafeLock.Domain.Common.ValueObjects;

namespace SafeLock.Domain.Aggregates.Users
{
    public class User : UserEntity
    {
        public ImageUrl PFP {get; private set;}

        private User () {}
        private User(Name name,
        Email email,
        PasswordHash password,
        ImageUrl image) :base(name,email,password)
        {
            PFP = image;
        }

        public static ResultGen<User> Create(
            string name,
            string email,
            string password,
            string pfpurl
        )
        {
            List<Error> errors = [];
            List<Result> results = [];

            var resultName = Name.Create(name);
            results.Add(resultName);
            var resultEmail = Email.Create(email);
            results.Add(resultEmail);
            var resultPassword = PasswordHash.Create(password);
            results.Add(resultPassword);
            var resultPFP = ImageUrl.Create(pfpurl);
            results.Add(resultPFP);

            foreach (var result in results)
            {
                if(result.IsFailure)
                    foreach (var error in result.Errors)
                    {
                        errors.Add(error);
                    }
            }

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