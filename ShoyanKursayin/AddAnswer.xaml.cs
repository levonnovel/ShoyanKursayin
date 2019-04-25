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
using System.Windows.Shapes;

namespace ShoyanKursayin
{
	/// <summary>
	/// Interaction logic for AddAnswer.xaml
	/// </summary>
	public partial class AddAnswer : Window
	{
		public AddAnswer()
		{
			InitializeComponent();
		}
		private void AddAns(object sender, RoutedEventArgs e)
		{
			using (SqlConnection conn = new SqlConnection(Fill.cs))
			{
				conn.Open();
				SqlCommand cmd = new SqlCommand(@"INSERT INTO Answers(AnswerText, AlterAnswer1) VALUES ( @ans, @altans )"
													, conn);

				cmd.Parameters.AddWithValue("@ans", this.ans.Text);
				cmd.Parameters.AddWithValue("@altans", this.altans.Text);

				SqlDataReader dr = cmd.ExecuteReader();
				MessageBox.Show("Successfully added");
				AdminPanel admin = Application.Current.Windows.OfType<AdminPanel>().FirstOrDefault();
				admin.Get_Anwers.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
			}
		}
	}
}
