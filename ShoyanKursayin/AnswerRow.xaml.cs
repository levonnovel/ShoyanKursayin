using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace ShoyanKursayin
{
    /// <summary>
    /// Interaction logic for AnswerRow.xaml
    /// </summary>
    public partial class AnswerRow : UserControl
    {
        public AnswerRow()
        {
            InitializeComponent();
        }

		private void Save(object sender, RoutedEventArgs e)
		{
			SaveAns(this.id.Text, this.answer.Text, this.alterAnswer.Text);
		}
		public void SaveAns(string id, string ans, string altans)
		{
			using (SqlConnection conn = new SqlConnection(Fill.cs))
			{
				conn.Open();
				SqlCommand cmd = new SqlCommand(@"UPDATE Answers SET AnswerText = @ans, AlterAnswer1=@altans WHERE Answer_ID=@id"
													, conn);

				cmd.Parameters.AddWithValue("@id", Convert.ToInt32(id));
				cmd.Parameters.AddWithValue("@ans", ans);
				cmd.Parameters.AddWithValue("@altans", altans);

				SqlDataReader dr = cmd.ExecuteReader();
				MessageBox.Show("Row has been successfully changed");
				AdminPanel admin = Application.Current.Windows.OfType<AdminPanel>().FirstOrDefault();
				admin.Get_Anwers.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

			}
		}

		private void Remove(object sender, RoutedEventArgs e)
		{
			try
			{
				RemoveRow(this.id.Text);
				MessageBox.Show("Row has been removed successfully");
				AdminPanel admin = Application.Current.Windows.OfType<AdminPanel>().FirstOrDefault();
				admin.wrap.Children.Remove(this);

			}
			catch
			{
				MessageBox.Show("Problem has been occured");
			}
			//admin.Get_Synonyms.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
		}
		private void RemoveRow(string id)
		{
			using (SqlConnection conn = new SqlConnection(Fill.cs))
			{
				conn.Open();
				SqlCommand cmd = new SqlCommand(@"DELETE FROM Answers WHERE Answer_ID=@id"
													, conn);

				cmd.Parameters.AddWithValue("@id", Convert.ToInt32(id));

				SqlDataReader dr = cmd.ExecuteReader();
				AdminPanel admin = Application.Current.Windows.OfType<AdminPanel>().FirstOrDefault();
				admin.Get_Synonyms.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

			}
		}
	}
}
