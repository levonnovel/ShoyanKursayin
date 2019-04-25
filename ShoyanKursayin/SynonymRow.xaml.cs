using System;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ShoyanKursayin
{
	/// <summary>
	/// Interaction logic for DataRow1.xaml
	/// </summary>
	public partial class SynonymRow : UserControl
	{
		public SynonymRow()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			SaveSyns(this.id.Text, this.word.Text, this.syns.Text);
		}
		public void SaveSyns(string id, string word, string syns)
		{
			using (SqlConnection conn = new SqlConnection(Fill.cs))
			{
				conn.Open();
				SqlCommand cmd = new SqlCommand(@"UPDATE Synonyms SET Word = @word, Synonyms=@syn WHERE Id=@id"
													, conn);

				cmd.Parameters.AddWithValue("@id", Convert.ToInt32(id));
				cmd.Parameters.AddWithValue("@word", word);
				cmd.Parameters.AddWithValue("@syn", syns);

				SqlDataReader dr = cmd.ExecuteReader();
				MessageBox.Show("Row has been successfully changed");
				AdminPanel admin = Application.Current.Windows.OfType<AdminPanel>().FirstOrDefault();
				admin.Get_Synonyms.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

			}
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{

			RemoveRow(this.id.Text);
			MessageBox.Show("Row has been removed successfully");
			AdminPanel admin = Application.Current.Windows.OfType<AdminPanel>().FirstOrDefault();
			//admin.Get_Synonyms.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
			admin.wrap.Children.Remove(this);
		}
		private void RemoveRow(string id)
		{
			using (SqlConnection conn = new SqlConnection(Fill.cs))
			{
				conn.Open();
				SqlCommand cmd = new SqlCommand(@"DELETE FROM Synonyms WHERE Id=@id"
													, conn);

				cmd.Parameters.AddWithValue("@id", Convert.ToInt32(id));

				SqlDataReader dr = cmd.ExecuteReader();
				AdminPanel admin = Application.Current.Windows.OfType<AdminPanel>().FirstOrDefault();
				admin.Get_Synonyms.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

			}
		}
	}
}
