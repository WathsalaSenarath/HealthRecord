using HealthRecord.Domain.Models;
using HealthRecord.Infrastructure;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace HealthRecord.Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly PatientRecordService _patientRecordService;
        public MainWindow()
        {
            InitializeComponent();
            _patientRecordService = new PatientRecordService();

            // Detach event handlers if they were previously attached
            ButtonAdd.Click -= ButtonAdd_Click;
            ButtonList.Click -= ButtonList_Click;
            DataGridBrand.SelectionChanged -= DataGridBrand_SelectionChanged;
            ButtonEdit.Click -= ButtonEdit_Click;
            ButtonDelete.Click -= ButtonDelete_Click;
            ButtonSearch.Click -= ButtonSearch_Click;
            ButtonOpenImageFolder.Click -= ButtonOpenImageFolder_Click;

            // Attach event handlers
            ButtonAdd.Click += ButtonAdd_Click;
            ButtonList.Click += ButtonList_Click;
            DataGridBrand.SelectionChanged += DataGridBrand_SelectionChanged;
            ButtonEdit.Click += ButtonEdit_Click;
            ButtonDelete.Click += ButtonDelete_Click;
            ButtonSearch.Click += ButtonSearch_Click;

            ButtonOpenImageFolder.Click += ButtonOpenImageFolder_Click;
        }

        private async void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtPatientID.Text != string.Empty && txtPatient.Text != string.Empty)
                {
                    await _patientRecordService.UpdateBrand(int.Parse(txtPatientID.Text), txtPatient.Text, txtDetails.Text);
                    throw new Exception("Patient data Successfully Updated");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                await ListBrands();
            }
        }

        private async void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtPatientID.Text != string.Empty && txtPatient.Text != string.Empty && txtDetails.Text != string.Empty)
                {
                    await _patientRecordService.DeleteBrand(int.Parse(txtPatientID.Text));
                    throw new Exception("Patient data Successfully Deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                await ListBrands();
            }
        }

        private async void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            var SearchName = await _patientRecordService.SearchBrandByName(txtPatient.Text);
            DataGridBrand.ItemsSource = SearchName.ToList();
        }

        private void DataGridBrand_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var activelist = (Patient)DataGridBrand.CurrentItem;

                if (activelist != null)
                {
                    txtPatientID.Text = activelist.Id.ToString();
                    txtPatient.Text = activelist.Name;
                    txtDetails.Text = activelist.Details;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void ButtonList_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await ListBrands();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async Task ListBrands()
        {
            var brandList = await _patientRecordService.ListBrands();
            DataGridBrand.ItemsSource = brandList.ToList();
        }

        private async void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var addedBrand = await _patientRecordService.AddBrand(txtPatient.Text, txtDetails.Text);

                // Clear the textboxes and focus on Student ID textbox after successful addition
                txtPatientID.Clear();
                txtPatient.Clear();
                txtDetails.Clear();
                txtPatientID.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // Refresh the grid after the addition
                await ListBrands();
            }
        }

        private void ButtonOpenImageFolder_Click(object sender, RoutedEventArgs e)
        {
            string selectedFolder = @""; // Set the folder path

            try
            {
                // Check if the folder exists
                if (Directory.Exists(selectedFolder))
                {
                    // Open the folder in Windows Explorer
                    Process.Start("explorer.exe", $"\"{selectedFolder}\"");
                }
                else
                {
                    System.Windows.MessageBox.Show("The specified folder does not exist.", "Folder Not Found", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}