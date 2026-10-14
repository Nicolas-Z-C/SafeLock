using SafeLock.Domain.Common.ValueObjects;
using SafeLock.Domain.Common.Results;
using SafeLock.Domain.Common.Events;
using SafeLock.Domain.Common.Errors;

namespace SafeLock.Domain.Common.Entities
{
    public abstract class UserEntity : AuditableEntity
    {
        public ImageUrl PFP {get; private set;}
        public Name Name { get; protected set; } = null!;
        public Email Email { get; protected set; } = null!;
        public PasswordHash PasswordHash { get; protected set; } = null!;
        public bool IsActive { get; protected set; }

        protected UserEntity() { } 

        protected UserEntity(Name name, Email email, PasswordHash passwordHash, ImageUrl pfp)
        {
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            PFP = pfp;
            IsActive = true;
        }

        protected Result RequestPasswordChange(string token, TimeSpan duration)
            {
                if(!IsActive)
                    return Result.Failure(BaseUserErrors.UserInactive);
                
                if(string.IsNullOrWhiteSpace(token))
                    return Result.Failure(BaseUserErrors.UserEmptyToken);
                
                var expiration = DateTime.UtcNow.Add(duration);

                AddDomainEvent(new PasswordReset(Id, Email, token, expiration, DateTime.UtcNow));

                return Result.Success();
                
            }

        protected Result Desactivar()
        {
            if (!IsActive)
                return Result.Failure(BaseUserErrors.UserInactive);

            IsActive = false;
            return Result.Success();
        }

        protected Result Reactivar()
        {
            if (IsActive)
                return Result.Failure(new Error("Usuario.Activo","El usuario se encuentra activo"));

            IsActive = true;
            return Result.Success();
        }

        protected Result ChangePassword(PasswordHash newHash)
        {
            PasswordHash = newHash;
            return Result.Success();
        }
    }
}