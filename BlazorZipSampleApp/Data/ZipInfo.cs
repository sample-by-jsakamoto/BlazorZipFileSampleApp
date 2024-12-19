using System.Text;

namespace BlazorZipSampleApp.Data
{
    public class ZipInfo
    {
        public string Name { get; set; }

        public List<ZipContent> Contents { get; private set; } = new List<ZipContent>();
        public List<string> Directories { get; private set; } = new List<string>();
        public void Append(ZipContent content)
        {
            Contents.Add(content);
        }
        public void Append(string directory)
        {
            Directories.Add(directory);
        }

        public string ToCsv()
        {
            var header = ZipContent.Header() + Environment.NewLine;
            return header + string.Join(Environment.NewLine, Contents.Select(x => x.ToString()));
        }
    }

    public class ZipContent
    {
        public byte[] Bytes { get; set; }
        public string Name { get; set; }

        public string FullName { get; set; }
        public long Size { get; set; }

        public override string ToString()
        {
            return $"{Name},{Size},{FullName}";
        }

        public static string Header()
        {
            return $"Name,Size,FullName";
        }
    }
}
