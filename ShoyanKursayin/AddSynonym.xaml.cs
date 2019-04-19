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
	/// Interaction logic for AddSynonym.xaml
	/// </summary>
	public partial class AddSynonym : Window
	{
		public AddSynonym()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			using (SqlConnection conn = new SqlConnection(Fill.cs))
			{
				conn.Open();
				SqlCommand cmd = new SqlCommand(@"INSERT INTO Synonyms  VALUES ( @word, @syn )"
													, conn);

				cmd.Parameters.AddWithValue("@word", this.word.Text);
				cmd.Parameters.AddWithValue("@syn", this.syn.Text);

				SqlDataReader dr = cmd.ExecuteReader();
				MessageBox.Show("Successfully added");
				AdminPanel admin = Application.Current.Windows.OfType<AdminPanel>().FirstOrDefault();
				admin.Get_Synonyms.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
			}
			}
	}
}
