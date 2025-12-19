using System.Collections.Generic;
using System.Linq;
using System.Text;

using Route4MeSDK.DataTypes.V5;

namespace Route4MeSdkV5UnitTest.V5
{
    /// <summary>
    /// Helper class for V5 unit tests
    /// </summary>
    public static class V5TestHelper
    {
        /// <summary>
        /// Collects all error messages from ResultResponse dictionary values and presents them as one string
        /// </summary>
        /// <param name="resultResponse">The ResultResponse containing error messages</param>
        /// <returns>A single string containing all error messages, or empty string if no messages</returns>
        public static string GetAllErrorMessages(ResultResponse resultResponse)
        {
            if (resultResponse?.Messages == null || !resultResponse.Messages.Any())
            {
                return string.Empty;
            }

            var sb = new StringBuilder();

            foreach (var kvp in resultResponse.Messages)
            {
                var key = kvp.Key;
                var messages = kvp.Value;

                if (messages != null && messages.Length > 0)
                {
                    foreach (var message in messages)
                    {
                        if (!string.IsNullOrWhiteSpace(message))
                        {
                            if (sb.Length > 0)
                            {
                                sb.Append("; ");
                            }

                            // Include the key if it's not empty and not a generic placeholder
                            if (!string.IsNullOrWhiteSpace(key))
                            {
                                sb.Append($"{key}: {message}");
                            }
                            else
                            {
                                sb.Append(message);
                            }
                        }
                    }
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Collects all error messages from ResultResponse dictionary values and presents them as one formatted string with line breaks
        /// </summary>
        /// <param name="resultResponse">The ResultResponse containing error messages</param>
        /// <returns>A formatted string containing all error messages with line breaks, or empty string if no messages</returns>
        public static string GetAllErrorMessagesFormatted(ResultResponse resultResponse)
        {
            if (resultResponse?.Messages == null || !resultResponse.Messages.Any())
            {
                return string.Empty;
            }

            var sb = new StringBuilder();

            foreach (var kvp in resultResponse.Messages)
            {
                var key = kvp.Key;
                var messages = kvp.Value;

                if (messages != null && messages.Length > 0)
                {
                    foreach (var message in messages)
                    {
                        if (!string.IsNullOrWhiteSpace(message))
                        {
                            // Include the key if it's not empty and not a generic placeholder
                            if (!string.IsNullOrWhiteSpace(key))
                            {
                                sb.AppendLine($"{key}: {message}");
                            }
                            else
                            {
                                sb.AppendLine(message);
                            }
                        }
                    }
                }
            }

            return sb.ToString().TrimEnd();
        }
    }
}