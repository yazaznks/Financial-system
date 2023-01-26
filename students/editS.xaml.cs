using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace nedaasqlite.students
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class editS : Page
    {
        public editS()
        {
            this.InitializeComponent();
            List<StudentLib.studentDetail> hi = StudentLib.GetStudentInfo(studentInfo.nama);
            foreach (var type in hi){
                Names.Text = type.name;
                ID.Text = type.ID;
                DOB.Date = new DateTimeOffset(new DateTime(int.Parse(type.dob.Substring(6, 4)), int.Parse(type.dob.Substring(3, 2)), int.Parse(type.dob.Substring(0, 2))));
                Mom.Text = type.momNumber;
                Dad.Text = type.dadNumber;
                disability.Text = type.condition;
                tuition.Text = type.fees.ToString();
                nationality.Text = type.nationality;
                
                busF.Text = type.BusF.ToString();
                notes.Text = type.notes;
            }
        }


        //edit data

        private void BtnS_Click(object sender, RoutedEventArgs e)
        {
            if (busF.Text.Equals(""))
                busF.Text = "0";
            int b = int.Parse(busF.Text);
            if (tuition.Text.Equals(""))
                tuition.Text = "0";
            int t = int.Parse(tuition.Text);
            StudentLib.UpdateStudent(studentInfo.nama, Names.Text, ID.Text, DOB.Date.ToString().Substring(0,10), Mom.Text, Dad.Text,
                disability.Text, nationality.Text,t, b, notes.Text );
            Frame.Navigate(typeof(studentInfo));
        }



        
    }
}
