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
	/// Interaction logic for DataRow1.xaml
	/// </summary>
	public partial class DataRow1 : UserControl
	{
		public DataRow1()
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
	}
}
