using SafeLock.Domain.Common.ValueObjects;
using SafeLock.Domain.Common.Results;
using SafeLock.Domain.Common.Events;
using SafeLock.Domain.Common.Errors;
using System.Security.Cryptography;
using System.Text;

namespace SafeLock.Domain.Common.Entities
{
    public abstract class UserEntity : AuditableEntity
    {
        public ImageUrl PFP {get; private set;}
        public Name Name { get; protected set; } = null!;
        public Email Email { get; protected set; } = null!;
        public PasswordHash PasswordHash { get; protected set; } = null!;
        public bool IsActive { get; protected set; }
        public string? PasswordResetTokenHash { get; private set; }
        public DateTime? PasswordResetExpiresUtc { get; private set; }

        protected UserEntity() { } 

        protected UserEntity(Name name, Email email, PasswordHash passwordHash, ImageUrl pfp)
        {
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            PFP = pfp;
            IsActive = true;
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
                return Result.Failure(BaseUserErrors.UserAlreadyActive);

            IsActive = true;
            return Result.Success();
        }

        protected Result RequestPasswordReset(string token, string tokenHash, TimeSpan duration)
        {
            if (!IsActive)
                return Result.Failure(BaseUserErrors.UserInactive);

            if (string.IsNullOrWhiteSpace(token) || string.IsNullOrWhiteSpace(tokenHash))
                return Result.Failure(BaseUserErrors.UserEmptyToken);

            var expiration = DateTime.UtcNow.Add(duration);

            PasswordResetTokenHash = tokenHash;     
            PasswordResetExpiresUtc = expiration;

            AddDomainEvent(new PasswordResetRequested(Id, Email, token, expiration, DateTime.UtcNow));
            return Result.Success();
        }

        protected Result ResetPassword(string tokenHash, PasswordHash newHash)
        {
            var valid = IsActive
                && PasswordResetTokenHash is not null
                && PasswordResetExpiresUtc > DateTime.UtcNow
                && CryptographicOperations.FixedTimeEquals(
                    Encoding.UTF8.GetBytes(PasswordResetTokenHash),
                    Encoding.UTF8.GetBytes(tokenHash));

            if (!valid)
                return Result.Failure(BaseUserErrors.InvalidResetToken);

            PasswordHash = newHash;
            ClearPasswordReset();    
            return Result.Success();
        }

        protected Result ChangePassword(PasswordHash newHash)
        {
            PasswordHash = newHash;
            ClearPasswordReset();   
            return Result.Success();
        }

        private void ClearPasswordReset()
        {
            PasswordResetTokenHash = null;
            PasswordResetExpiresUtc = null;
        }
    }
}