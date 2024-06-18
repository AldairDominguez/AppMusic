using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.DTO
{
    public class PlaylistDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int WeatherId { get; set; }
        public string WeatherDescription { get; set; }
    }
}
