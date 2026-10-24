using System.Net.Http.Json;
using System.Security.Cryptography.Xml;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;

namespace HomeWorkWinForms
{
    public partial class MainForm : Form
    {
        private int currentPage = 1;
        private int totalPages = 1;
        private int currentGenreId = 0;
        private Movies shownMovies;

        public MainForm()
        {
            InitializeComponent();

            this.Load += MainForm_Load;

            SearchButton.Click += async (s, e) => SearchByName();
            GenreComboBox.SelectedIndexChanged += async (s, e) => await GenreChanged();
            NextButton.Click += async (s, e) => await ChagePage(currentPage + 1);
            PreviousButton.Click += async (s, e) => await ChagePage(currentPage - 1);
            MovieDataGridView.CellDoubleClick += MovieDataGridView_CellDoubleClick;
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            await LoadGenres();
            await LoadPopularMovies(1);
        }

        private async Task LoadGenres()
        {
            GenreComboBox.Items.Clear();
            GenreComboBox.Items.Add(new GenreComboBoxItem { Text = "All Genres", Value = 0 });

            var retrivedGenres = await MovieApi.GetGenreList();

            foreach (var item in retrivedGenres.genres)
            {
                GenreComboBox.Items.Add(new GenreComboBoxItem { Text = item.name, Value = item.id });
            }

            GenreComboBox.DisplayMember = "Text";
            GenreComboBox.ValueMember = "Value";
            GenreComboBox.SelectedIndex = 0;

        }

        private async Task LoadPopularMovies(int page)
        {
            shownMovies = await MovieApi.GetPopularMovies();



            currentPage = shownMovies.Page;
            totalPages = shownMovies.Total_Pages;

            DisplayMovies(shownMovies);
        }

        private async Task DisplayMovies(Movies movies)
        {
            MovieDataGridView.AutoGenerateColumns = false;
            MovieDataGridView.DataSource = movies.Results;

            PageInfoLabel.Text = $"Page {currentPage} from {totalPages}";
            NextButton.Enabled = currentPage < totalPages;
            PreviousButton.Enabled = currentPage > 1;
        }

        private async Task ChagePage(int page)
        {

            UIToggle(false);
            if (!string.IsNullOrWhiteSpace(SearchTextBox.Text))
            {
                shownMovies = await MovieApi.GetMoviesByName(SearchTextBox.Text.Trim(), page);
            }
            else if (currentGenreId != 0)
            {
                shownMovies = await MovieApi.GetMoviesByGenre(currentGenreId, page);
            }
            else
            {
                shownMovies = await MovieApi.GetPopularMovies(page);
            }
            currentPage = shownMovies.Page;
            totalPages = shownMovies.Total_Pages;
            DisplayMovies(shownMovies);
            UIToggle(true);
        }

        private async Task SearchByName()
        {
            UIToggle(false);
            if (!string.IsNullOrWhiteSpace(SearchTextBox.Text.Trim()))
            {
                shownMovies = await MovieApi.GetMoviesByName(SearchTextBox.Text.Trim(), 1);
            }
            else
            {
                shownMovies = await MovieApi.GetPopularMovies(1);
            }

            currentPage = shownMovies.Page;
            totalPages = shownMovies.Total_Pages;
            DisplayMovies(shownMovies);
            UIToggle(true);
        }

        private async Task GenreChanged()
        {
            var selected = GenreComboBox.SelectedItem as GenreComboBoxItem;

            if (selected == null)
            {
                return;
            }
            currentGenreId = selected.Value;
            if (currentGenreId == 0)
            {
                await LoadPopularMovies(1);
            }
            else
            {
                UIToggle(false);
                shownMovies = await MovieApi.GetMoviesByGenre(currentGenreId, 1);
                currentPage = shownMovies.Page;
                totalPages = shownMovies.Total_Pages;
                DisplayMovies(shownMovies);
            }

            UIToggle(true);
        }
        private void UIToggle(bool enable)
        {
            SearchButton.Enabled = enable;
            GenreComboBox.Enabled = enable;
            NextButton.Enabled = enable && currentPage < totalPages;
            PreviousButton.Enabled = enable && currentPage > 1;
            MovieDataGridView.Enabled = enable;
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
    }
}
