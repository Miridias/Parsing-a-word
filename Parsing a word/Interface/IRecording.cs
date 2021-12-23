using System.Collections.Generic;

namespace Parsing_a_word.Interface
{
    interface IRecording
    {
        public void RecordingToFile(List<string> allRaightTestWords, string pathToFolderRecording);
    }
}
