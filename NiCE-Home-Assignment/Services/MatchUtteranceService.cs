using System.Runtime.CompilerServices;

namespace NiCE_Home_Assignment.Services
{
    public class MatchUtteranceService
    {
        static HashSet<string> BuildWordSet(string taskKey)
        {
            var hashSet = new HashSet<string>(taskKey.ToLowerInvariant()
                .Split((char[])null, StringSplitOptions.RemoveEmptyEntries),
                StringComparer.OrdinalIgnoreCase);
            return hashSet;
        }

        public bool IsMatching(string utterance, string taskKey)
        {
            // Build a dictionary of words from the taskKey
            var taskWords = BuildWordSet(taskKey);

            // Split the utterance into words
            var utteranceWords = BuildWordSet(utterance);

            // Check if all words in the taskKey are present in the utterance
            foreach (var word in taskWords)
            {
                if (!utteranceWords.Contains(word))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
