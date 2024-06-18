using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.DTO
{
    public class SongDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string Album { get; set; }
        public TimeSpan Duration { get; set; }

        public string FilePath { get; set; }
    }
}
