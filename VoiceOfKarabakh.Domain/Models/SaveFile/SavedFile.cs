using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace VoiceOfKarabakh.Domain.Models
{
    public class SavedFile
    {
        public int Id { get; set; }

        public string FilePath { get; set; }

        public string FileName
        {
            get
            {
                return FilePath.Split('/').Last();
            }
        }
    }
}
