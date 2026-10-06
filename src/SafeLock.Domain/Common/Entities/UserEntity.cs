using SafeLock.Domain.Common.ValueObjects;
using SafeLock.Domain.Common.Results;
using SafeLock.Domain.Common.Events;
using SafeLock.Domain.Common.Errors;

namespace SafeLock.Domain.Common.Entities
{
    public abstract class UserEntity : AuditableEntity
    {
        public Name Name { get; protected set; } = null!;
        public Email Email { get; protected set; } = null!;
        public PasswordHash PasswordHash { get; protected set; } = null!;
        public bool IsActive { get; protected set; }

        protected UserEntity() { } 

        protected UserEntity(Name name, Email email, PasswordHash passwordHash)
        {
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            IsActive = true;
        }

        protected ResultGen<bool> RequestPasswordChange(string token, TimeSpan duration)
            {
                if(!IsActive)
                    return ResultGen<bool>.Failure(BaseUserErrors.UserInactive);
                
                if(string.IsNullOrWhiteSpace(token))
                    return ResultGen<bool>.Failure(BaseUserErrors.UserEmptyToken);
                
                var expiration = DateTime.UtcNow.Add(duration);

                AddDomainEvent(new PasswordReset(Id, Email, token, expiration, DateTime.UtcNow));

                return ResultGen<bool>.Success(true);
                
            }

        protected ResultGen<bool> Desactivar()
        {
            if (!IsActive)
                return ResultGen<bool>.Failure(BaseUserErrors.UserInactive);

            IsActive = false;
            return ResultGen<bool>.Success(true);
        }

        protected ResultGen<bool> Reactivar()
        {
            if (IsActive)
                return ResultGen<bool>.Failure(new Error("Usuario.Activo","El usuario se encuentra activo"));

            IsActive = true;
            return ResultGen<bool>.Success(true);
        }

        protected ResultGen<bool> ChangePassword(PasswordHash newHash)
        {
            PasswordHash = newHash;
            return ResultGen<bool>.Success(true);
        }
    }
}