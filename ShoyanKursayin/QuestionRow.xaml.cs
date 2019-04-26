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
    public partial class QuestionRow : UserControl
    {
        public QuestionRow()
        {
            InitializeComponent();
        }

		private void Save(object sender, RoutedEventArgs e)
		{
			SaveQuestion(this.id.Text, this.question.Text, this.answer_id.Text, this.topic_id.Text);
		}
		public void SaveQuestion(string id, string quest, string answer_id, string topic_id)
		{
			using (SqlConnection conn = new SqlConnection(Fill.cs))
			{
				conn.Open();
				SqlCommand cmd = new SqlCommand(@"UPDATE Questions SET QuestionText = @quest, Answer_ID=@ans_id, Topic_ID=@t_id WHERE Question_ID=@id"
													, conn);

				cmd.Parameters.AddWithValue("@id", Convert.ToInt32(id));
				cmd.Parameters.AddWithValue("@quest", quest);
				cmd.Parameters.AddWithValue("@ans_id", Convert.ToInt32(answer_id));
				cmd.Parameters.AddWithValue("@t_id", Convert.ToInt32(topic_id));

				SqlDataReader dr = cmd.ExecuteReader();
				MessageBox.Show("Row has been successfully changed");
				AdminPanel admin = Application.Current.Windows.OfType<AdminPanel>().FirstOrDefault();
				admin.Get_Questions.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

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
				SqlCommand cmd = new SqlCommand(@"DELETE FROM Questions WHERE Answer_ID=@id"
													, conn);

				cmd.Parameters.AddWithValue("@id", Convert.ToInt32(id));

				SqlDataReader dr = cmd.ExecuteReader();
				AdminPanel admin = Application.Current.Windows.OfType<AdminPanel>().FirstOrDefault();
				//admin.Get_Questions.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

			}
		}
	}
}
