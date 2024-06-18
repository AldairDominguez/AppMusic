using Application.Implementations;
using Application.Interfaces;
using CrossCutting.DTO;
using Infraestructure.Implementations;
using Infraestructure.Interfaces;
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


namespace UIMusicApp
{
    public partial class Admin : Form
    {

        ///// Autor ///////

        private AutorSQL _autorSQL;

        //// Musica/////////

        private ISongApplication _songApplication;
        private IAuthorApplication _authorApplication;
        private string _selectedFilePath;
        private IWeatherApplication _weatherApplication;

        ////Playlist ////////
        private IPlaylistApplication _playlistApplication;
        private string oldPlaylistName;

        ////PlaylistSong ////////
        private readonly IPlaylistSongsApplication _playlistSongsApplication;

        public Admin()
        {
            InitializeComponent();
            ////Autor/////
            var connection = new Connection();
            _autorSQL = new AutorSQL(connection);
            InitializeDataGridView();
            LoadNextIdAsync();
            LoadAuthorsAsync();
            txtAutorA.KeyPress += txtAutorA_KeyPress;
            btnEliminarA.Enabled = false;
            btnActualizarA.Enabled = false;

            ///Musica////
            _songApplication = new SongApplication(new SongSQL(connection));
            _authorApplication = new AuthorApplication(new AutorSQL(connection));
            InitializeCategoryComboBox();
            LoadAuthorIdsIntoComboBox();
            LoadNextSongIdAsync();
            LoadSongsAsync();
            btnEliminarM.Enabled = false;
            btnActualizarM.Enabled = false;


            ///Clima////
            _weatherApplication = new WeatherApplication(new WeatherSQL(new Connection()), new WeatherService(new ConnectionAPI()));
            LoadWeathers();
            LoadNextWeatherIdAsync();
            LoadCurrentWeatherDescription();
            btnEliminarC.Enabled = false;
            btnActualizarC.Enabled = false;


            ///Playlist////
            _playlistApplication = new PlaylistApplication(new PlaylistSQL(connection), _weatherApplication);
            InitializeDataGridView2();
            LoadNextPlaylistIdAsync();
            LoadWeatherIdsAsync();
            LoadPlaylistsAsync();
            btnActualizarP.Enabled = false;
            btnEliminarP.Enabled = false;


            ///PlaylistSong////
            _playlistSongsApplication = new PlaylistSongsApplication(new PlaylistSongsSQL(connection), _songApplication, _playlistApplication);

            LoadPlaylistsIntoComboBox();

            Task.Run(() => LoadAllSongsIntoListBox2());
        }
        /////////////////////////////////////////////AUTOR///////////////////////////////////////////////////
        private async void LoadNextIdAsync()
        {
            int nextId = await _autorSQL.GetNextIdAsync();
            lblIdA.Text = nextId.ToString();
        }

        private async void LoadAuthorsAsync()
        {
            var authors = await _autorSQL.GetAllAuthorsAsync();
            dataGridView1.DataSource = authors;

            dataGridView1.Columns["Id"].HeaderText = "ID";
            dataGridView1.Columns["Name"].HeaderText = "Autor";
        }
        private void InitializeDataGridView()
        {
            dataGridView1.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn idColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Id",
                HeaderText = "ID",
                Name = "Id"
            };
            dataGridView1.Columns.Add(idColumn);

            DataGridViewTextBoxColumn nameColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Name",
                HeaderText = "Autor",
                Name = "Name"
            };
            dataGridView1.Columns.Add(nameColumn);

            dataGridView1.CellClick += new DataGridViewCellEventHandler(dataGridView1_CellClick);
        }
        private async void btnGuardarA_Click(object sender, EventArgs e)
        {
            string nombreAutor = txtAutorA.Text;
            if (!string.IsNullOrEmpty(nombreAutor))
            {
                int id = int.Parse(lblIdA.Text);
                var autor = new AuthorDTO { Id = id, Name = nombreAutor };
                await _autorSQL.Add(autor);

                MessageBox.Show("Autor guardado correctamente.");
                txtAutorA.Text = string.Empty;
                LoadAuthorIdsIntoComboBox();
                LoadNextIdAsync();
                LoadAuthorsAsync();
            }
            else
            {
                MessageBox.Show("Por favor, ingrese un nombre de autor.");
            }
        }

        private async void btnEliminarA_Click(object sender, EventArgs e)
        {
            if (int.TryParse(lblIdA.Text, out int id))
            {
                var result = MessageBox.Show("¿Está seguro de que desea eliminar este autor?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    await _autorSQL.Remove(id);

                    MessageBox.Show("Autor eliminado correctamente.");
                    LoadAuthorIdsIntoComboBox();
                    LoadAuthorsAsync();
                    LoadNextIdAsync();
                    txtAutorA.Text = string.Empty;
                    btnEliminarA.Enabled = false;
                    btnActualizarA.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un autor para eliminar.");
            }
        }

        private async void btnActualizarA_Click(object sender, EventArgs e)
        {
            if (int.TryParse(lblIdA.Text, out int id))
            {
                string nombreAutor = txtAutorA.Text;
                if (!string.IsNullOrEmpty(nombreAutor))
                {
                    var autor = new AuthorDTO { Id = id, Name = nombreAutor };
                    await _autorSQL.Update(autor);

                    MessageBox.Show("Autor actualizado correctamente.");
                    LoadAuthorIdsIntoComboBox();
                    LoadAuthorsAsync();
                    LoadNextIdAsync();
                    txtAutorA.Text = string.Empty;
                    btnEliminarA.Enabled = false;
                    btnActualizarA.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Por favor, ingrese un nombre de autor.");
                }
            }
        }

        private void btnLimpiarA_Click(object sender, EventArgs e)
        {
            txtAutorA.Text = string.Empty;
            btnActualizarA.Enabled = false;
            LoadNextIdAsync();
            btnEliminarA.Enabled = false;
            btnGuardarA.Enabled = true;

        }
        private void txtAutorA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                lblIdA.Text = row.Cells["Id"].Value.ToString();
                txtAutorA.Text = row.Cells["Name"].Value.ToString();
                btnActualizarA.Enabled = true;
                btnEliminarA.Enabled = true;
                btnGuardarA.Enabled = false;
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        /////////////////////////////////////////////MUSICA///////////////////////////////////////////////////

        private void InitializeCategoryComboBox()
        {
            cbxCategoria.Items.AddRange(new string[] { "Rock", "Pop", "Hip-Hop", "Jazz", "Clásica", "Electrónica", "Reggae", "Blues", "Salsa", "Bachata" });
            cbxCategoria.SelectedIndex = 0;
        }
        private async void LoadNextSongIdAsync()
        {
            int nextId = await _songApplication.GetNextIdAsync();
            lblIdM.Text = nextId.ToString();
        }
        private async void LoadSongsAsync()
        {
            var songs = await _songApplication.GetAllSongsAsync();
            dataGridView2.DataSource = songs.ToList();
            dataGridView2.Columns["Id"].HeaderText = "ID";
            dataGridView2.Columns["Name"].HeaderText = "Nombre";
            dataGridView2.Columns["Category"].HeaderText = "Categoría";
            dataGridView2.Columns["AuthorId"].HeaderText = "Autor ID";
            dataGridView2.Columns["Album"].HeaderText = "Álbum";
            dataGridView2.Columns["Duration"].HeaderText = "Duración";
        }
        private async void LoadAuthorIdsIntoComboBox()
        {
            var authors = await _authorApplication.GetAllAuthors();
            cbxAutorM.DataSource = authors;
            cbxAutorM.DisplayMember = "Name";
            cbxAutorM.ValueMember = "Id";
        }
        private void LimpiarCampos()
        {
            txtNombreM.Text = string.Empty;
            cbxCategoria.SelectedIndex = 0;
            cbxAutorM.SelectedIndex = 0;//-1
            txtAlbumM.Text = string.Empty;
            lblDura.Text = string.Empty;
            LoadNextSongIdAsync();

        }
        private void btnSlecM_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Music Files|*.mp3;*.m4a;*.wav";
                openFileDialog.Title = "Seleccionar uno o varios archivos";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    _selectedFilePath = openFileDialog.FileName;
                    TagLib.File tagFile = TagLib.File.Create(_selectedFilePath);
                    txtNombreM.Text = !string.IsNullOrEmpty(tagFile.Tag.Title) ? tagFile.Tag.Title : Path.GetFileNameWithoutExtension(_selectedFilePath);
                    lblDura.Text = tagFile.Properties.Duration.ToString(@"mm\:ss");
                }
            }
        }
        private async void button6_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedFilePath))
            {
                MessageBox.Show("Seleccione una canción primero.");
                return;
            }
            if (string.IsNullOrEmpty(txtNombreM.Text))
            {
                MessageBox.Show("Ingrese el nombre de la canción.");
                return;
            }
            if (cbxCategoria.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione una categoría.");
                return;
            }
            if (cbxAutorM.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un autor.");
                return;
            }
            if (string.IsNullOrEmpty(txtAlbumM.Text))
            {
                MessageBox.Show("Ingrese el nombre del álbum.");
                return;
            }
            if (string.IsNullOrEmpty(lblDura.Text))
            {
                MessageBox.Show("Ingrese la duración de la canción.");
                return;
            }

            try
            {
                int songId = await _songApplication.GetNextIdAsync();
                //MessageBox.Show($"Siguiente ID de canción: {songId}");
                string fileName = $"{songId}.mp3";
                string musicFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Musica");
                string destinationPath = Path.Combine(musicFolderPath, fileName);

                if (!Directory.Exists(musicFolderPath))
                {
                    Directory.CreateDirectory(musicFolderPath);
                }
                if (!File.Exists(_selectedFilePath))
                {
                    MessageBox.Show("El archivo de origen no existe.");
                    return;
                }
                /*if (File.Exists(destinationPath))
                {
                    MessageBox.Show("El archivo ya existe en la carpeta de destino.");
                    return;
                }*/
                File.Copy(_selectedFilePath, destinationPath, true);
                // MessageBox.Show("Archivo copiado exitosamente.");

                int authorId = (int)cbxAutorM.SelectedValue;
                var existingSongs = await _songApplication.GetAllSongsAsync();
                if (existingSongs.Any(s => s.Name.Equals(txtNombreM.Text, StringComparison.OrdinalIgnoreCase) && s.AuthorId == authorId))
                {
                    MessageBox.Show("Ya existe una canción con el mismo nombre y autor.");
                    File.Delete(destinationPath);
                    return;
                }
                SongDTO song = new SongDTO
                {
                    Id = songId,
                    Name = txtNombreM.Text,
                    Category = cbxCategoria.SelectedItem.ToString(),
                    AuthorId = authorId,
                    Album = txtAlbumM.Text,
                    Duration = TimeSpan.ParseExact(lblDura.Text, @"mm\:ss", null),
                    FilePath = destinationPath
                };
                await _songApplication.AddSongAsync(song);
                // MessageBox.Show("Canción agregada a la base de datos.");
                var allSongs = await _songApplication.GetAllSongsAsync();
                var addedSong = allSongs.FirstOrDefault(s => s.Id == songId);
                if (addedSong != null)
                {
                    // MessageBox.Show("Canción verificada en la base de datos.");
                }
                else
                {
                    //  MessageBox.Show("Error: La canción no se encontró en la base de datos.");
                }

                // MessageBox.Show("Canción guardada exitosamente.");
                LoadAllSongsIntoListBox2();
                LoadNextSongIdAsync();
                LoadSongsAsync();
                LimpiarCampos();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar la canción: {ex.Message}");
            }
        }
        private async void btnEliminarM_Click(object sender, EventArgs e)
        {
            if (int.TryParse(lblIdM.Text, out int songId))
            {
                var result = MessageBox.Show("¿Está seguro de que desea eliminar esta Canción?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        var song = await _songApplication.GetSongAsync(songId);
                        if (song != null)
                        {
                            string musicFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Musica");
                            string filePath = Path.Combine(musicFolderPath, $"{song.Name}.mp3");

                            if (File.Exists(filePath))
                            {
                                File.Delete(filePath);
                            }
                        }
                        await _songApplication.RemoveSongAsync(songId);
                        MessageBox.Show("Canción eliminada exitosamente.");
                        LoadAllSongsIntoListBox2();
                        LoadSongsAsync();
                        LoadNextSongIdAsync();
                        LimpiarCampos();
                        btnEliminarM.Enabled = false;
                        btnActualizarM.Enabled = false;
                        BtnGuardarM.Enabled = true;
                        btnSlecM.Enabled = true;
                        
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al eliminar la canción: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione una canción válida para eliminar.");
            }
        }

        private async void btnActualizarM_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombreM.Text) ||
                cbxCategoria.SelectedIndex == -1 ||
                cbxAutorM.SelectedIndex == -1 ||
                string.IsNullOrWhiteSpace(txtAlbumM.Text) ||
                string.IsNullOrWhiteSpace(lblDura.Text))
            {
                MessageBox.Show("Por favor, asegúrese de que todos los campos estén llenos antes de guardar.", "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (int.TryParse(lblIdM.Text, out int songId))
            {
                try
                {
                    TimeSpan duration;
                    if (!TimeSpan.TryParseExact(lblDura.Text, @"hh\:mm\:ss", null, out duration) &&
                        !TimeSpan.TryParseExact(lblDura.Text, @"mm\:ss", null, out duration))
                    {
                        throw new FormatException("Formato de duración inválido.");
                    }

                    var currentSong = await _songApplication.GetSongAsync(songId);
                    SongDTO song = new SongDTO
                    {
                        Id = songId,
                        Name = txtNombreM.Text,
                        Category = cbxCategoria.SelectedItem.ToString(),
                        AuthorId = (int)cbxAutorM.SelectedValue,
                        Album = txtAlbumM.Text,
                        Duration = duration
                    };

                    await _songApplication.UpdateSongAsync(song);

                    if (currentSong != null && currentSong.Name != song.Name)
                    {
                        string musicFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Musica");
                        string oldFilePath = Path.Combine(musicFolderPath, $"{currentSong.Name}.mp3");
                        string newFilePath = Path.Combine(musicFolderPath, $"{song.Name}.mp3");

                        if (File.Exists(oldFilePath))
                        {
                            File.Move(oldFilePath, newFilePath);
                        }
                    }

                    MessageBox.Show("Canción actualizada exitosamente.");
                    LoadAllSongsIntoListBox2();
                    LoadSongsAsync();
                    LimpiarCampos();
                    btnEliminarM.Enabled = false;
                    btnActualizarM.Enabled = false;
                    btnSlecM.Enabled = true;
                    BtnGuardarM.Enabled = true;
                    

                }
                catch (FormatException ex)
                {
                    MessageBox.Show($"Error de formato: {ex.Message}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al actualizar la canción: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("ID de canción inválido.");
            }
        }

        private void btnLimpiarM_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            btnEliminarM.Enabled = false;
            btnActualizarM.Enabled = false;
            BtnGuardarM.Enabled = true;
            btnSlecM.Enabled = true;
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cbxCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var selectedRow = dataGridView2.Rows[e.RowIndex];
                lblIdM.Text = selectedRow.Cells["ID"].Value.ToString();
                txtNombreM.Text = selectedRow.Cells["Name"].Value.ToString();
                cbxCategoria.SelectedItem = selectedRow.Cells["Category"].Value.ToString();
                cbxAutorM.SelectedValue = selectedRow.Cells["AuthorId"].Value;
                txtAlbumM.Text = selectedRow.Cells["Album"].Value.ToString();
                lblDura.Text = selectedRow.Cells["Duration"].Value.ToString();
                btnEliminarM.Enabled = true;
                btnActualizarM.Enabled = true;
                BtnGuardarM.Enabled = false;
                btnSlecM.Enabled = false;
            }
        }
        /////////////////////////////////////////////CLIMA///////////////////////////////////////////////////
        private async void LoadWeathers()
        {
            var weathers = await _weatherApplication.GetAllWeathers();
            dataGridView3.DataSource = weathers.ToList();
        }
        private async Task LoadNextWeatherIdAsync()
        {
            int nextId = await _weatherApplication.GetNextWeatherIdAsync();
            lblIDC.Text = nextId.ToString();
        }
        private async void LoadCurrentWeatherDescription()
        {
            var weatherApplication = new WeatherApplication(new WeatherSQL(new Connection()), new WeatherService(new ConnectionAPI()));
            string descripcionClima = await weatherApplication.GetWeatherDescriptionAsync();
            lblClima.Text = descripcionClima;
        }

        private async void LimpiarFormulario()
        {
            lblIDC.Text = "";
            txtClima.Text = "";
            textDecrip.Text = "";
            btnEliminarC.Enabled = false;
            btnActualizarC.Enabled = false;
            btnGuardarC.Enabled = true;

            await LoadNextWeatherIdAsync();
            var weatherApplication = new WeatherApplication(new WeatherSQL(new Connection()), new WeatherService(new ConnectionAPI()));
            string descripcionClima = await weatherApplication.GetWeatherDescriptionAsync();
            txtClima.Text = descripcionClima;
        }

        private async void btnGuardarC_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtClima.Text) || string.IsNullOrEmpty(textDecrip.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos antes de guardar.");
                return;
            }

            WeatherDTO weather = new WeatherDTO
            {
                Code = txtClima.Text,
                Description = textDecrip.Text
            };

            await _weatherApplication.AddWeather(weather);
            MessageBox.Show("Clima agregado exitosamente!");
            LoadWeathers();
            LimpiarFormulario();
        }

        private async void btnEliminarC_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("¿Está seguro de que desea eliminar este clima?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                int id = int.Parse(lblIDC.Text);
                await _weatherApplication.DeleteWeather(id);
                LoadWeathers();
                LimpiarFormulario();
                btnGuardarC.Enabled = true;
            }
        }

        private async void btnActualizarC_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtClima.Text) || string.IsNullOrEmpty(textDecrip.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos antes de actualizar.");
                return;
            }

            WeatherDTO weather = new WeatherDTO
            {
                Id = int.Parse(lblIDC.Text),
                Code = txtClima.Text,
                Description = textDecrip.Text
            };

            await _weatherApplication.UpdateWeather(weather);
            MessageBox.Show("Clima actualizado exitosamente!");
            LoadWeathers();
            LimpiarFormulario();
            btnGuardarC.Enabled = true;
        }

        private void btnLimpiarC_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var selectedRow = dataGridView3.Rows[e.RowIndex];
                lblIDC.Text = selectedRow.Cells["Id"].Value.ToString();
                txtClima.Text = selectedRow.Cells["Code"].Value.ToString();
                textDecrip.Text = selectedRow.Cells["Description"].Value.ToString();

                btnEliminarC.Enabled = true;
                btnActualizarC.Enabled = true;
                btnGuardarC.Enabled = false;
            }
        }

        /////////////////////////////////////////////PLAYLIST///////////////////////////////////////////////////

        private async Task LoadNextPlaylistIdAsync()
        {
            lblIDP.Text = (await _playlistApplication.GetNextPlaylistIdAsync()).ToString();
        }
        private async void LoadWeatherIdsAsync()
        {
            try
            {
                var weatherData = await _weatherApplication.GetWeatherIdsAndCodesAsync();
                var weatherList = weatherData.Select(w => new { Key = w.Key, Value = w.Value }).ToList();

                cbxTiempo.DataSource = new BindingSource(weatherList, null);
                cbxTiempo.DisplayMember = "Value";
                cbxTiempo.ValueMember = "Key";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar IDs de clima: {ex.Message}");
            }
        }

        private async void LoadPlaylistsAsync()
        {
            try
            {
                var playlists = await _playlistApplication.GetAllPlaylistsAsync();
                dataGridView4.DataSource = playlists;

                if (dataGridView4.Columns["Id"] != null) dataGridView4.Columns["Id"].HeaderText = "ID";
                if (dataGridView4.Columns["Name"] != null) dataGridView4.Columns["Name"].HeaderText = "Nombre de la Playlist";
                if (dataGridView4.Columns["WeatherId"] != null) dataGridView4.Columns["WeatherId"].HeaderText = "Tiempo";

                foreach (DataGridViewColumn column in dataGridView4.Columns)
                {
                    column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                dataGridView4.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las playlists: {ex.Message}");
            }
        }
        private async Task LimpiarFormulario2()
        {
            lblIDP.Text = "";
            txtPlaylist.Text = "";
            cbxTiempo.SelectedIndex = 0;
            await LoadNextPlaylistIdAsync();
            LoadPlaylistsAsync();
        }

        private async void btnGuardarP_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbxTiempo.SelectedValue != null)
                {
                    string playlistName = txtPlaylist.Text;

                    if (await _playlistApplication.PlaylistNameExistsAsync(playlistName))
                    {
                        MessageBox.Show("Este nombre de playlist ya está guardado.");
                        await LimpiarFormulario2();
                        return;
                    }

                    int weatherId = Convert.ToInt32(cbxTiempo.SelectedValue);

                    var playlist = new PlaylistDTO
                    {
                        Name = txtPlaylist.Text,
                        WeatherId = weatherId
                    };

                    int newId = await _playlistApplication.Add(playlist);
                    lblIDP.Text = newId.ToString();

                    MessageBox.Show("Playlist guardada exitosamente.");
                    await LimpiarFormulario2();
                    LoadPlaylistsIntoComboBox();
                }
                else
                {
                    MessageBox.Show("Selecciona un ID de tiempo válido.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private async void btnActualizarP_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtPlaylist.Text) || cbxTiempo.SelectedValue == null)
            {
                MessageBox.Show("Por favor, complete todos los campos antes de actualizar.");
                return;
            }

            var playlist = new PlaylistDTO
            {
                Id = int.Parse(lblIDP.Text),
                Name = txtPlaylist.Text,
                WeatherId = int.Parse(cbxTiempo.SelectedValue.ToString())
            };

            if (!string.IsNullOrEmpty(oldPlaylistName) && oldPlaylistName != txtPlaylist.Text)
            {
                PlaylistFolderHelper.RenamePlaylistFolder(oldPlaylistName, txtPlaylist.Text);
            }

            await _playlistApplication.UpdatePlaylistAsync(playlist);
            LoadPlaylistsIntoComboBox();
            await LimpiarFormulario2();
            btnEliminarP.Enabled = false;
            btnActualizarP.Enabled = false;
            btnGuardarP.Enabled = true;
            
            MessageBox.Show("Playlist actualizada exitosamente.");
        }

        private async void btnEliminarP_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("¿Está seguro de que desea eliminar esta Playlist?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                string playlistName = txtPlaylist.Text;
                int id = int.Parse(lblIDP.Text);
                Reproductor reproductor = System.Windows.Forms.Application.OpenForms.OfType<Reproductor>().FirstOrDefault();
                if (reproductor != null && reproductor.CurrentPlaylistId == id)
                {
                    MessageBox.Show("No se puede eliminar la playlist porque está en uso en el reproductor.");
                    return;
                }

                await _playlistApplication.DeletePlaylistAsync(id);
                PlaylistFolderHelper.DeletePlaylistFolder(playlistName);
                LoadPlaylistsIntoComboBox();
                MessageBox.Show("Playlist eliminada exitosamente.");
                await LimpiarFormulario2();
                btnEliminarP.Enabled = false;
                btnActualizarP.Enabled = false;
                btnGuardarP.Enabled = true;
                
            }
        }

        private async void btnLimpiarP_Click(object sender, EventArgs e)
        {
            await LimpiarFormulario2();
            btnGuardarP.Enabled = true;
            btnEliminarP.Enabled = false;
            btnActualizarP.Enabled = false;
        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var selectedRow = dataGridView4.Rows[e.RowIndex];
                lblIDP.Text = selectedRow.Cells["Id"].Value.ToString();
                txtPlaylist.Text = selectedRow.Cells["Name"].Value.ToString();
                cbxTiempo.SelectedValue = selectedRow.Cells["WeatherId"].Value;
                oldPlaylistName = txtPlaylist.Text;
                btnGuardarP.Enabled = false;
                btnActualizarP.Enabled = true;
                btnEliminarP.Enabled = true;
            }
        }
        private void InitializeDataGridView2()
        {
            dataGridView4.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn idColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Id",
                HeaderText = "ID",
                Name = "Id",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                },
                HeaderCell = new DataGridViewColumnHeaderCell
                {
                    Style = new DataGridViewCellStyle
                    {
                        Alignment = DataGridViewContentAlignment.MiddleCenter
                    }
                }
            };
            dataGridView4.Columns.Add(idColumn);

            DataGridViewTextBoxColumn nameColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Name",
                HeaderText = "Nombre de la Playlist",
                Name = "Name",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleLeft
                },
                HeaderCell = new DataGridViewColumnHeaderCell
                {
                    Style = new DataGridViewCellStyle
                    {
                        Alignment = DataGridViewContentAlignment.MiddleLeft
                    }
                }
            };
            dataGridView4.Columns.Add(nameColumn);

            DataGridViewTextBoxColumn weatherIdColumn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "WeatherId",
                HeaderText = "Tiempo",
                Name = "WeatherId",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                },
                HeaderCell = new DataGridViewColumnHeaderCell
                {
                    Style = new DataGridViewCellStyle
                    {
                        Alignment = DataGridViewContentAlignment.MiddleCenter
                    }
                }
            };
            dataGridView4.Columns.Add(weatherIdColumn);

            dataGridView4.CellClick += new DataGridViewCellEventHandler(dataGridView4_CellClick);
        }
        /////////////////////////////////////////////PLAYLISTSongs///////////////////////////////////////////////////
        
        
        private async void LoadPlaylistsIntoComboBox()
        {
            var playlists = await _playlistApplication.GetAllPlaylistsAsync();
            cbxPlaylist.DataSource = playlists;
            cbxPlaylist.DisplayMember = "Name";
            cbxPlaylist.ValueMember = "Id";
        }
        private async Task LoadPlaylistSongs(int playlistId)
        {
            var songs = await _playlistSongsApplication.GetSongsByPlaylistIdAsync(playlistId);
            listBox1.DataSource = songs;
            listBox1.DisplayMember = "Name";
            listBox1.ValueMember = "Id";
        }
        private async void LoadAllSongsIntoListBox2()
        {
            var songs = await _songApplication.GetAllSongsAsync();
            listBox2.DataSource = songs;
            listBox2.DisplayMember = "Name";
            listBox2.ValueMember = "Id";
        }
        private async Task LoadPlaylistsAsync2()
        {
            var playlists = await _playlistApplication.GetAllPlaylistsAsync();
            dataGridView4.DataSource = playlists;
            LoadPlaylistsIntoComboBox();
        }

        private async void btnAgregarPl_Click(object sender, EventArgs e)
        {
            if (cbxPlaylist.SelectedValue != null && listBox2.SelectedItem != null)
            {
                int playlistId = (int)cbxPlaylist.SelectedValue;
                int songId = (int)listBox2.SelectedValue;
                var existingSongs = await _playlistSongsApplication.GetSongsByPlaylistIdAsync(playlistId);
                if (existingSongs.Any(s => s.Id == songId))
                {
                    MessageBox.Show("La canción ya está en la playlist.");
                    return;
                }

                var playlistSong = new PlaylistSongsDTO
                {
                    PlaylistId = playlistId,
                    SongId = songId
                };

                await _playlistSongsApplication.AddSongToPlaylistAsync(playlistSong);
                MessageBox.Show("Canción agregada a la playlist exitosamente.");
                await LoadPlaylistSongs(playlistId);
            }
        }

        private async void btnEliminarPl_Click(object sender, EventArgs e)
        {
            if (cbxPlaylist.SelectedValue != null && listBox1.SelectedItem != null)
            {
                int playlistId = (int)cbxPlaylist.SelectedValue;
                int songId = (int)listBox1.SelectedValue;

                var playlistSong = new PlaylistSongsDTO
                {
                    PlaylistId = playlistId,
                    SongId = songId
                };

                await _playlistSongsApplication.RemoveSongFromPlaylistAsync(playlistSong);
                MessageBox.Show("Canción eliminada de la playlist exitosamente.");
                await LoadPlaylistSongs(playlistId);
            }
        }

        private async void cbxPlaylist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxPlaylist.SelectedValue is int playlistId)
            {
                var songsInPlaylist = await _playlistSongsApplication.GetSongsByPlaylistIdAsync(playlistId);
                listBox1.DataSource = songsInPlaylist;
                listBox1.DisplayMember = "Name";
                listBox1.ValueMember = "Id";
            }
        }

        private async void btnAtualizarPl_Click(object sender, EventArgs e)
        {
            LoadAllSongsIntoListBox2();
        }

        private void btnActualizarL_Click(object sender, EventArgs e)
        {
            LoadPlaylistsIntoComboBox();
        }
    }
}

