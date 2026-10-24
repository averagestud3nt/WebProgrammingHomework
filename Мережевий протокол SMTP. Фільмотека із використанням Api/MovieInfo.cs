using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HomeWorkWinForms
{
    public partial class MovieInfo : Form
    {
        private Movie movie;
        public MovieInfo(Movie movie)
        {
            this.movie = movie;
            InitializeComponent();
            LoadData();
            MovieDataGridView.CellDoubleClick += MovieDataGridView_CellDoubleClick;
        }

        private async void LoadData()
        {
            TitleLabel.Text = movie.title;

            DateDurationLabel.Text = $"Date {movie.release_date} | Duration {movie.runtime}";

            string genres = movie.genres != null ? $"Genres: " + string.Join(", ", movie.genres.Select(g => g.name)) + Environment.NewLine : "";

            DescriptionTextBox.Text = genres + movie.overview;

            if (!string.IsNullOrWhiteSpace(movie.poster_path))
            {
                try
                {
                    pictureBox1.Image = await LoadPoster(movie.poster_path);
                }
                catch
                {
                    pictureBox1.Image = null;
                }
            }

            await DisplayMovies(await MovieApi.GetSimilarMovies(movie.id, 1));

        }

        private async Task<System.Drawing.Image> LoadPoster(string posterPath)
        {
            string baseUrl = "https://image.tmdb.org/t/p/w500";

            using (var http = new HttpClient())
            {
                var bytes = await http.GetByteArrayAsync(baseUrl + posterPath);
                using (var ms = new MemoryStream(bytes))
                {
                    return System.Drawing.Image.FromStream(ms);
                }
            }
        }
        private async void MovieDataGridView_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            var movie = MovieDataGridView.Rows[e.RowIndex].DataBoundItem as Movie;
            if (movie == null)
            {
                return;
            }

            var retrivedMovie = await MovieApi.GetMovieById(movie.id);
            var informationForm = new MovieInfo(retrivedMovie);
            informationForm.ShowDialog();
        }

        private async Task DisplayMovies(Movies movies)
        {
            MovieDataGridView.AutoGenerateColumns = false;
            var selectedMovies = movies.Results.Take(5).ToList();
            MovieDataGridView.DataSource = selectedMovies;
        }
    }
}
