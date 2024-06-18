using Application.Implementations;
using Application.Interfaces;
using Infraestructure.Implementations;
using Infrastructure;
using Services.Implementations;
using Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrossCutting.DTO;
using Infraestructure.Interfaces;


namespace UIMusicApp
{
    public partial class Reproductor : Form
    {
        private bool isPlaying = false;
        private bool isRepeating = false;
        private List<int> randomIndices = new List<int>();
        private int randomIndex = 0;
        private bool isShuffling = false;
        private System.Windows.Forms.Timer timer;
        private IPlaylistApplication _playlistApplication;
        private IWeatherApplication _weatherApplication;
        private ISongApplication _songApplication;
        private IPlaylistSongsApplication _playlistSongsApplication;
        private IAuthorApplication _authorApplication;
        private int userRole;
        private bool isSoundOn = true;
        private string currentWeatherDescription = "";
        private CustomTrackBar customTrackBar1;
        private int previousVolume = 50;
        public int? CurrentPlaylistId { get; private set; }
        public Reproductor(int role)
        {
            InitializeComponent();
            InitializeDependencies();
            btnPlayPause.BackgroundImage = Properties.Resources.boton_de_play;
            btnPlayPause.BackgroundImageLayout = ImageLayout.Zoom;
            btnRepetir.BackgroundImage = Properties.Resources.repetir;
            btnRepetir.BackgroundImageLayout = ImageLayout.Zoom;
            btnAleatorio.BackgroundImage = Properties.Resources.barajar;
            btnAleatorio.BackgroundImageLayout = ImageLayout.Zoom;

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Tick += timer1_Tick;
            LoadPlaylists();
            LoadWeather();
            userRole = role;
            InitializeRoleBasedSettings();
            
        }

        private void InitializeRoleBasedSettings()
        {
            if (userRole == 2)
            {
                btnCancion.Visible = false;
            }
        }
        private void InitializeDependencies()
        {
            var connection = new Connection();
            var weatherSQL = new WeatherSQL(connection);
            _weatherApplication = new WeatherApplication(new WeatherSQL(new Connection()), new WeatherService(new ConnectionAPI()));
            var songSQL = new SongSQL(connection);
            _songApplication = new SongApplication(songSQL);
            var playlistSQL = new PlaylistSQL(connection);
            _playlistApplication = new PlaylistApplication(playlistSQL, _weatherApplication);
            var playlistSongsSQL = new PlaylistSongsSQL(connection);
            _weatherApplication = new WeatherApplication(new WeatherSQL(new Connection()), new WeatherService(new ConnectionAPI()));
            //_playlistSongsApplication = new PlaylistSongsApplication(playlistSongsSQL, _songApplication, _playlistApplication);
            _playlistSongsApplication = new PlaylistSongsApplication(new PlaylistSongsSQL(connection), _songApplication, _playlistApplication);
            var authorSQL = new AutorSQL(connection);
            _authorApplication = new AuthorApplication(authorSQL);


            this.trackBar1 = new CustomTrackBar();
            this.trackBar1.Location = new System.Drawing.Point(50, 50);
            this.trackBar1.Size = new System.Drawing.Size(200, 45);
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);




        }
        public Button BtnCancion
        {
            get { return btnCancion; }
        }
        private async void LoadPlaylists()
        {
            try
            {
                var playlists = await _playlistApplication.GetAllPlaylistsAsync();
                var filteredPlaylists = new List<PlaylistDTO>();

                foreach (var playlist in playlists)
                {
                    var weather = await _weatherApplication.GetWeatherById(playlist.WeatherId);
                    if (weather != null && weather.Code.Equals(currentWeatherDescription, StringComparison.OrdinalIgnoreCase))
                    {
                        filteredPlaylists.Add(playlist);
                    }
                }

                listBox1.DisplayMember = "Name";
                listBox1.ValueMember = "Id";
                listBox1.DataSource = filteredPlaylists;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las playlists: {ex.Message}");
            }
        }
        private async void LoadPlaylistSongs(int playlistId)
        {
            try
            {
                listView1.Items.Clear();
                var songs = await _playlistSongsApplication.GetSongsByPlaylistIdAsync(playlistId);
                TimeSpan totalDuration = TimeSpan.Zero;
                foreach (var song in songs)
                {
                    string songFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Musica", $"{song.Id}.mp3");
                    if (File.Exists(songFilePath))
                    {
                        ListViewItem item = new ListViewItem(song.Name);
                        item.Tag = songFilePath;
                        listView1.Items.Add(item);
                        totalDuration += song.Duration;
                    }
                }
                lblTiempoT.Text = $"Tiempo total: {totalDuration.ToString(@"hh\:mm\:ss")}";
                listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                if (listView1.Items.Count > 0)
                {
                    UpdateSongNameLabel(listView1.Items[0].Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las canciones de la playlist: {ex.Message}");
            }
        }



        private async void button5_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Seleccione una canción de la lista para reproducir.");
                return;
            }

            if (isPlaying)
            {
                axWindowsMediaPlayer1.Ctlcontrols.pause();
                btnPlayPause.BackgroundImage = Properties.Resources.boton_de_play;
                timer.Stop();
            }
            else
            {
                string filePath = listView1.SelectedItems[0].Tag.ToString();
                if (axWindowsMediaPlayer1.URL != filePath)
                {
                    axWindowsMediaPlayer1.URL = filePath;
                }
                axWindowsMediaPlayer1.Ctlcontrols.play();
                btnPlayPause.BackgroundImage = Properties.Resources.pausa_en_la_reproduccion;
                timer.Start();
            }
            isPlaying = !isPlaying;
        }

        private void btnRepetir_Click(object sender, EventArgs e)
        {
            if (isRepeating)
            {
                btnRepetir.BackgroundImage = Properties.Resources.repetir;
            }
            else
            {
                btnRepetir.BackgroundImage = Properties.Resources.repetir_una_vez__1_;
            }
            isRepeating = !isRepeating;
        }

        private void btnAleatorio_Click(object sender, EventArgs e)
        {
            if (isShuffling)
            {
                btnAleatorio.BackgroundImage = Properties.Resources.barajar;
            }
            else
            {
                btnAleatorio.BackgroundImage = Properties.Resources.barajar2;
                GenerateRandomOrder();
                randomIndex = 0;
            }
            isShuffling = !isShuffling;
        }

        private void btnCancion_Click(object sender, EventArgs e)
        {
            Admin adminForm = new Admin();
            adminForm.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (axWindowsMediaPlayer1 != null && axWindowsMediaPlayer1.currentMedia != null)
                {
                    trackBar1.Maximum = (int)axWindowsMediaPlayer1.currentMedia.duration;
                    trackBar1.Value = (int)axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
                    label1.Text = axWindowsMediaPlayer1.Ctlcontrols.currentPositionString;
                    label2.Text = axWindowsMediaPlayer1.currentMedia.durationString;
                }
            }
            catch (System.Runtime.InteropServices.InvalidComObjectException)
            {
                timer.Stop();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (axWindowsMediaPlayer1.currentMedia != null)
            {
                axWindowsMediaPlayer1.Ctlcontrols.currentPosition = trackBar1.Value;
            }
        }

        private void axWindowsMediaPlayer1_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (e.newState == (int)WMPLib.WMPPlayState.wmppsStopped)
            {
                if (isRepeating)
                {
                    axWindowsMediaPlayer1.Ctlcontrols.currentPosition = 0;
                    axWindowsMediaPlayer1.Ctlcontrols.play();
                    btnPlayPause.BackgroundImage = Properties.Resources.pausa_en_la_reproduccion;
                }
                else if (isShuffling)
                {

                }
                else
                {
                    btnPlayPause.BackgroundImage = Properties.Resources.boton_de_play;
                    isPlaying = false;
                }
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            if (listView1.Items.Count == 0)
            {
                MessageBox.Show("No hay canciones en la lista.");
                return;
            }

            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Seleccione una canción de la lista para empezar.");
                return;
            }
            int currentIndex = listView1.SelectedItems[0].Index;
            int nextIndex = currentIndex + 1;
            if (nextIndex >= listView1.Items.Count)
            {
                nextIndex = 0;
            }
            listView1.SelectedItems[0].Selected = false;
            listView1.Items[nextIndex].Selected = true;
            listView1.Items[nextIndex].EnsureVisible();
            string filePath = listView1.Items[nextIndex].Tag.ToString();
            axWindowsMediaPlayer1.URL = filePath;
            axWindowsMediaPlayer1.Ctlcontrols.play();
            btnPlayPause.BackgroundImage = Properties.Resources.pausa_en_la_reproduccion;
            isPlaying = true;
            timer.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count == 0)
            {
                MessageBox.Show("No hay canciones en la lista.");
                return;
            }
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Seleccione una canción de la lista para empezar.");
                return;
            }
            int currentIndex = listView1.SelectedItems[0].Index;
            int previousIndex = currentIndex - 1;
            if (previousIndex < 0)
            {
                previousIndex = listView1.Items.Count - 1;
            }
            listView1.SelectedItems[0].Selected = false;
            listView1.Items[previousIndex].Selected = true;
            listView1.Items[previousIndex].EnsureVisible();
            string filePath = listView1.Items[previousIndex].Tag.ToString();
            axWindowsMediaPlayer1.URL = filePath;
            axWindowsMediaPlayer1.Ctlcontrols.play();
            btnPlayPause.BackgroundImage = Properties.Resources.pausa_en_la_reproduccion;
            isPlaying = true;
            timer.Start();
        }

        private void GenerateRandomOrder()
        {
            randomIndices.Clear();
            Random rnd = new Random();
            List<int> indices = Enumerable.Range(0, listView1.Items.Count).ToList();
            while (indices.Count > 0)
            {
                int index = rnd.Next(indices.Count);
                randomIndices.Add(indices[index]);
                indices.RemoveAt(index);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0 && listBox1.SelectedValue != null)
            {

                var selectedValue = listBox1.SelectedValue;
                // MessageBox.Show($"Selected Value: {selectedValue}, Type: {selectedValue.GetType()}");
                if (selectedValue is int playlistId)
                {
                    CurrentPlaylistId = playlistId;
                    LoadPlaylistSongs(playlistId);


                    //MessageBox.Show($"Playlist: {listBox1.Text}, Id: {playlistId}");
                }
                else
                {
                    MessageBox.Show("Error al convertir el ID de la playlist.");
                }
            }
            else
            {

                // MessageBox.Show("No se ha seleccionado ninguna playlist.");
            }
        }
        private async void LoadWeather()
        {
            try
            {

                var weatherApplication = new WeatherApplication(new WeatherSQL(new Connection()), new WeatherService(new ConnectionAPI()));
                string descripcionClima = await weatherApplication.GetWeatherDescriptionAsync();
                currentWeatherDescription = descripcionClima;
                lblClima.Text = descripcionClima;
                LoadPlaylists();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar la descripción del clima: {ex.Message}");
            }
        }
        private void UpdateSongNameLabel(string songName)
        {
            lblNomM.Text = songName;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                string songName = listView1.SelectedItems[0].Text;
                UpdateSongNameLabel(songName);

            }
        }
        private async Task UpdateAuthorLabel(int authorId)
        {
            try
            {
                var author = await _authorApplication.GetAuthorByIdAsync(authorId);
                //lblAutorM.Text = author?.Name ?? "Desconocido";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el autor: {ex.Message}");
                // lblAutorM.Text = "Desconocido";
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Reproductor_Load(object sender, EventArgs e)
        {

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void btnSonido_Click(object sender, EventArgs e)
        {
            if (isSoundOn)
            {
                btnSonido.BackgroundImage = Properties.Resources.sonido;
                previousVolume = axWindowsMediaPlayer1.settings.volume;
                axWindowsMediaPlayer1.settings.volume = 0;
            }
            else
            {
                btnSonido.BackgroundImage = Properties.Resources.sonido__1_;
                axWindowsMediaPlayer1.settings.volume = previousVolume;
            }
            isSoundOn = !isSoundOn;
        }

        private void mtrackVolumen_Scroll(object sender, EventArgs e)
        {
            
            int volumen = mtrackVolumen.Value;
            axWindowsMediaPlayer1.settings.volume = volumen;
            lblVolumen.Text = volumen.ToString();
        }
    }
}

