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
	public partial class AddQuestion : Window
	{
		public AddQuestion()
		{
			InitializeComponent();
		}
		private void AddAns(object sender, RoutedEventArgs e)
		{
			using (SqlConnection conn = new SqlConnection(Fill.cs))
			{
				conn.Open();
				SqlCommand cmd = new SqlCommand(@"INSERT INTO Questions(QuestionText, Answer_ID, Topic_ID) VALUES ( @quest, @ans_id, @q_id)"
													, conn);

				cmd.Parameters.AddWithValue("@quest", this.question.Text);
				cmd.Parameters.AddWithValue("@ans_id", Convert.ToInt32(this.answer_id.Text));
				cmd.Parameters.AddWithValue("@q_id", Convert.ToInt32(this.topic_id.Text));

				SqlDataReader dr = cmd.ExecuteReader();
				MessageBox.Show("Successfully added");
				AdminPanel admin = Application.Current.Windows.OfType<AdminPanel>().FirstOrDefault();
				admin.Get_Questions.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
			}
		}
	}
}
