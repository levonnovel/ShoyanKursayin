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
	/// Interaction logic for AdminLogin.xaml
	/// </summary>
	public partial class AdminLogin : Window
	{
		public AdminLogin()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
			if(CheckUser(Login.Text, Password.Text.GetHashCode().ToString()))
			{
				AdminPanel admin = new AdminPanel();
				admin.ShowDialog();
			}
			else
			{
				MessageBox.Show("Wrong credentials inserted");
				
			}
		}

		private bool CheckUser(string user, string pass)
		{

			using (SqlConnection conn = new SqlConnection(Fill.cs))
			{
				conn.Open();
				SqlCommand cmd = new SqlCommand(@"
													select Count(*)
													from Admins a
													where a.login=@user and a.password=@password	
													"
													, conn);

				cmd.Parameters.AddWithValue("@user", user);
				cmd.Parameters.AddWithValue("@password", pass);

				SqlDataReader dr = cmd.ExecuteReader();
				while (dr.Read())
				{
					if(Convert.ToInt32(dr.GetValue(0)) == 1)
					{
						return true;
					}
				}
			}

			return false;

		}

	}
}
