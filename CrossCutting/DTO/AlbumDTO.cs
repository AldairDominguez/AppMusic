using CrossCutting.DTO;
using System.Collections.Generic;


namespace CrossCutting
{
    public class AlbumDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SongDTO> Songs { get; set; }
        public string WeatherCode { get; set; }
    }


    //cosas alternativas como por ejemplo numero aleatorios

}
