using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
namespace WPF {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        string path = "..\\..\\DB\\xml\\tailieu.xml";
        int i = 0;
        string Sex = "";
        public MainWindow() {
            InitializeComponent();
            XDocument doc = XDocument.Load(path);
            var resource = doc.Descendants("Student").Select(x => new {
                id = x.Element("ID").Value,
                fullName = x.Element("FullName").Value,
                dateBirth = x.Element("DateBirth").Value,
                className = x.Element("Class").Value,
                sex = x.Element("Sex").Value,
                faculty = x.Element("Faculty").Value
            }) ;
            XElement lastStudent = doc.Descendants("Student").Last();
            string lastID = lastStudent.Element("ID").Value;
            char soThu9 = lastID[8];
            char soThu8 = lastID[7];
            char soThu7 = lastID[6];
            int lastIDToNumber = (int)char.GetNumericValue(soThu9) * 100 + (int)char.GetNumericValue(soThu8) * 10 + (int)char.GetNumericValue(soThu7);
            i = lastIDToNumber;
            dataGridView.ItemsSource = resource;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e) {
            i++;
            XDocument doc = XDocument.Load(path);
            if (txtFullName.Text != null && txtClassName.Text != null && txtDateBirth != null && txtFaculty != null && Sex != "") {
                doc.Root.Add(new XElement("Student",
                            new XElement("ID", txtClassName.Text + ((i < 10) ? "00" + i.ToString() : "0" + i.ToString())),
                            new XElement("DateBirth", txtDateBirth.Text),
                            new XElement("FullName", txtFullName.Text),
                            new XElement("Class", txtClassName.Text),
                            new XElement("Sex", Sex),
                            new XElement("Faculty", txtFaculty.Text)

                )) ;
                doc.Save(path);
                var resource = doc.Descendants("Student").Select(x => new {
                    id = x.Element("ID").Value,
                    fullName = x.Element("FullName").Value,
                    dateBirth = x.Element("DateBirth").Value,
                    className = x.Element("Class").Value,
                    sex = x.Element("Sex").Value,
                    faculty = x.Element("Faculty").Value
                });
                dataGridView.ItemsSource = resource;
            }
            else {
                MessageBox.Show("Please fill all field!!!");
            }

        }

        private void Male_Checked(object sender, RoutedEventArgs e) {
            Sex = "Nam";
        }

        private void Famale_Checked(object sender, RoutedEventArgs e) {
            Sex = "Nu";
        }
        string selectedID = "";
        private void DataGridView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
                    DataGrid dataGrid = sender as DataGrid;
            if ((DataGridRow)dataGridView.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex) != null) {
                DataGridRow row = (DataGridRow)dataGridView.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex);
                DataGridCell RowColumn = dataGrid.Columns[0].GetCellContent(row).Parent as DataGridCell;
                selectedID = ((TextBlock)RowColumn.Content).Text;
                labelCurrentID.Content = selectedID;
            }
        }

        private void BtnRemove_Click(object sender, RoutedEventArgs e) {
            if(selectedID != "") {
                XDocument doc = XDocument.Load(path);
                XElement cStudent = doc.Descendants("Student").Where(c => c.Element("ID").Value.Equals(selectedID)).FirstOrDefault();
                cStudent.Remove();
                doc.Save(path);
                var resource = doc.Descendants("Student").Select(x => new {
                    id = x.Element("ID").Value,
                    fullName = x.Element("FullName").Value,
                    dateBirth = x.Element("DateBirth").Value,
                    className = x.Element("Class").Value,
                    sex = x.Element("Sex").Value,
                    faculty = x.Element("Faculty").Value
                });
                dataGridView.ItemsSource = resource;
                selectedID = "";
            }
            else {
                MessageBox.Show("Please choose Student you want to remove");
            }
        }

        private void BtnModify_Click(object sender, RoutedEventArgs e) {
            if(selectedID != "") {
                Window1 win1 = new Window1(selectedID);
                win1.ShowDialog();
                if (!win1.IsActive) {
                    XDocument doc = XDocument.Load(path);
                    var resource = doc.Descendants("Student").Select(x => new {
                        id = x.Element("ID").Value,
                        fullName = x.Element("FullName").Value,
                        dateBirth = x.Element("DateBirth").Value,
                        className = x.Element("Class").Value,
                        sex = x.Element("Sex").Value,
                        faculty = x.Element("Faculty").Value
                    });
                    dataGridView.ItemsSource = resource;
                }
            }
            else {
                MessageBox.Show("Please choose a student!!!");
            }
        }
    }
}
