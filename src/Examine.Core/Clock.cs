using System;

namespace Examine
{
    /// <summary>
    /// Examine Clock
    /// </summary>
    public static class ExamineClock
    {
        /// <summary>
        /// Gets the current time.
        /// </summary>
        /// <remarks>By default, this is UTC</remarks>
        public static Func<DateTimeOffset> CurrentTime { get; set; } = () => DateTimeOffset.UtcNow;
    }
}
