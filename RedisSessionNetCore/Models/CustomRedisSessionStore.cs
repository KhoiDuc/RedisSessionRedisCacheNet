namespace RedisSessionNetCore.Models
{
    using Microsoft.AspNetCore.Session;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Diagnostics.CodeAnalysis;

    public class DistributedSessionStore : ISessionStore
    {
        private readonly string _prefix;
        private readonly IDistributedCache _cache;
        private readonly ILoggerFactory _loggerFactory;

        /// <summary>
        /// Initializes a new instance of <see cref="DistributedSessionStore"/>.
        /// </summary>
        /// <param name="cache">The <see cref="IDistributedCache"/> used to store the session data.</param>
        /// <param name="loggerFactory">The <see cref="ILoggerFactory"/>.</param>
        public DistributedSessionStore(IDistributedCache cache, ILoggerFactory loggerFactory, string prefix = "MyApp:Session:")
        {
            ArgumentNullException.ThrowIfNull(cache);
            ArgumentNullException.ThrowIfNull(loggerFactory);

            _cache = cache;
            _loggerFactory = loggerFactory; 
            _prefix = prefix;
        }

        /// <inheritdoc />
        public ISession Create(string sessionKey, TimeSpan idleTimeout, TimeSpan ioTimeout, Func<bool> tryEstablishSession, bool isNewSessionKey)
        {
            ArgumentException.ThrowIfNullOrEmpty(sessionKey);
            ArgumentNullException.ThrowIfNull(tryEstablishSession);

            return new DistributedSession(_cache, $"{_prefix}{sessionKey}", idleTimeout, ioTimeout, tryEstablishSession, _loggerFactory, isNewSessionKey);
        }
    }

}
