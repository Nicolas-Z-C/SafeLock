using System.Text.RegularExpressions;

namespace SafeLock.Domain.Common
{
    public static partial class RegexPatterns
    {
        [GeneratedRegex(@"^[\p{L}0-9\s\.,\-_'""!?]+$")]
        public static partial Regex BasicText();

        [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
        public static partial Regex Email();

        [GeneratedRegex(@"^[a-zA-Z0-9_]{3,20}$")]
        public static partial Regex UserName();

        [GeneratedRegex(@"^https?:\/\/[^\s/$.?#].[^\s]*$")]
        public static partial Regex Url();

        [GeneratedRegex(@"^https?:\/\/[^\s/$.?#][^\s]*\.(mp4|webm|mov)(\?[^\s]*)?$", RegexOptions.IgnoreCase)]
        public static partial Regex VideoUrl();
        
        [GeneratedRegex(@"^\$argon2id\$v=\d+\$m=\d+,t=\d+,p=\d+\$[A-Za-z0-9+/]+={0,2}\$[A-Za-z0-9+/]+={0,2}$")]
        public static partial Regex Argon2Hash();
}
}