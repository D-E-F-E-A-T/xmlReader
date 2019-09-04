using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.Xml.Linq;
namespace WPF {
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window {
        string path = "..\\..\\DB\\xml\\tailieu.xml";
        string selectedID;
        public Window1(string sltID) {
            InitializeComponent();
            this.selectedID = sltID;
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e) {
            XDocument doc = XDocument.Load(path);
            if (selectedID != "") {
                XElement currentStudent = doc.Descendants("Student").Where(y => y.Element("ID").Value == selectedID).FirstOrDefault();
                if (txtNewFullName.Text != "") currentStudent.Element("FullName").Value = txtNewFullName.Text;
                if (txtNewClass.Text != "") currentStudent.Element("Class").Value = txtNewClass.Text;
                if (txtNewFaculty.Text != "") currentStudent.Element("Faculty").Value = txtNewFaculty.Text;
                if (txtDateBirth.Text != "") currentStudent.Element("DateBirth").Value = txtDateBirth.Text;
                doc.Save(path);        
                this.Close();
            }
        }
    }
}
