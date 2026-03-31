using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Opaño_WPF_Project
{
    public partial class MainWindow : Window
 {
     // declare the array storage
     string[] names = new string[100];
     string[] phones = new string[100];
     string[] emails = new string[100];

     char status = 'A';

     int index = 0;
     int updatedIndex = -1;

     public MainWindow()
     {
         InitializeComponent();
     }

     private void btnSave_Click(object sender, RoutedEventArgs e)
     {
         string name = txtName.Text;
         string phone = txtPhone.Text;
         string email = txtEmail.Text;

         if (name == "" || phone == "" || email == "")
         {
             MessageBox.Show("Please fill all fields", "Contact Manager", MessageBoxButton.OK);
             return;
         }

         SaveData(name, phone, email);
         ClearData();
     }

     private void ClearData()
     {
         txtName.Clear();
         txtPhone.Clear();
         txtEmail.Clear();
     }

     private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
     {
         int index = dataGrid.SelectedIndex;

         if (index >= 0)
         {
             txtName.Text = names[index];
             txtPhone.Text = phones[index];
             txtEmail.Text = emails[index];

             btnDeleteData.IsEnabled = true;

             status = 'E';
             updatedIndex = index;
         }
     }

     private void SaveData(string n, string p, string e)
     {
         if (status == 'A')
         {
             names[index] = n;
             phones[index] = p;
             emails[index] = e;

             dataGrid.Items.Add(new
             {
                 Name = names[index],
                 Phone = phones[index],
                 Email = emails[index]
             });

             index++;

             MessageBox.Show("New contact added!", "Contact Manager", MessageBoxButton.OK);
         }
         else if (status == 'E' && updatedIndex >= 0)
         {
             names[updatedIndex] = n;
             phones[updatedIndex] = p;
             emails[updatedIndex] = e;

             RefreshGrid();

             status = 'A';
             updatedIndex = -1;

             MessageBox.Show("Contact updated!", "Contact Manager", MessageBoxButton.OK);
         }
     }

     private void btnDeleteData_Click(object sender, RoutedEventArgs e)
     {
         int deleteIndex = dataGrid.SelectedIndex;

         if (deleteIndex == -1)
         {
             MessageBox.Show("Please select a contact to delete.");
             return;
         }

         ShiftElements(deleteIndex);
         index--;

         RefreshGrid();

         btnDeleteData.IsEnabled = false;
         ClearData();

         MessageBox.Show("Contact deleted!", "Contact Manager", MessageBoxButton.OK);
     }

     private void RefreshGrid()
     {
         dataGrid.Items.Clear();

         for (int i = 0; i < index; i++)
         {
             dataGrid.Items.Add(new
             {
                 Name = names[i],
                 Phone = phones[i],
                 Email = emails[i]
             });
         }
     }

     private void ShiftElements(int deletedIndex)
     {
         for (int i = deletedIndex; i < index; i++)
         {
             names[i] = names[i + 1];
             phones[i] = phones[i + 1];
             emails[i] = emails[i + 1];
         }
     }
 }
}
