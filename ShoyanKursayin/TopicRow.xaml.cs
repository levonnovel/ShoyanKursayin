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
    public partial class TopicRow : UserControl
    {
        public TopicRow()
        {
            InitializeComponent();
        }

		private void Save(object sender, RoutedEventArgs e)
		{
			SaveTopic(this.id.Text, this.topic.Text);
		}
		public void SaveTopic(string id, string topic)
		{
			using (SqlConnection conn = new SqlConnection(Fill.cs))
			{
				conn.Open();
				SqlCommand cmd = new SqlCommand(@"UPDATE Topics SET Topic = @topic WHERE ID=@id"
													, conn);

				cmd.Parameters.AddWithValue("@id", Convert.ToInt32(id));
				cmd.Parameters.AddWithValue("@topic", topic);

				SqlDataReader dr = cmd.ExecuteReader();
				MessageBox.Show("Row has been successfully changed");
				AdminPanel admin = Application.Current.Windows.OfType<AdminPanel>().FirstOrDefault();
				admin.Get_Topics.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

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
				SqlCommand cmd = new SqlCommand(@"DELETE FROM Topics WHERE ID=@id"
													, conn);

				cmd.Parameters.AddWithValue("@id", Convert.ToInt32(id));

				SqlDataReader dr = cmd.ExecuteReader();
				AdminPanel admin = Application.Current.Windows.OfType<AdminPanel>().FirstOrDefault();
				admin.Get_Topics.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

			}
		}
	}
}
