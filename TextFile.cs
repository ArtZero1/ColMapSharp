using System.IO;
using System.Collections.Generic;

namespace brab.colmap
{
    public class TextFile
    {
        string Path;

        public TextFile(string path)
        {
            this.Path = path;
        }

        ///
        /// Lazy read lines in the text file,
        /// skipping comment lines.
        ///
        public IEnumerable<string> Lines()
        {
            var file = new StreamReader(Path);
            string line;

            while ((line = file.ReadLine()) !=  null)
            {
                if (line.StartsWith("#"))
                {
                    /* comment, skip it */
                    continue;
                }

                yield return line;
            }

            file.Close();
        }
    }
}
