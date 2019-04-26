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
using Excel = Microsoft.Office.Interop.Excel;

namespace ShoyanKursayin
{
	/// <summary>
	/// Interaction logic for AdminPanel.xaml
	/// </summary>
	public partial class AdminPanel : Window
	{
		public AdminPanel()
		{
			
			InitializeComponent();

		}

		List<Synonyms> LoadSynsData()
		{
			using (SqlConnection conn = new SqlConnection(Fill.cs))
			{
				List<Synonyms> syns = new List<Synonyms>();
				conn.Open();
				SqlCommand cmd = new SqlCommand(@"select * from Synonyms"
													, conn);


				SqlDataReader dr = cmd.ExecuteReader();
				while (dr.Read())
				{

					syns.Add(new Synonyms { id = Convert.ToInt32(dr["Id"]), word = dr["Word"].ToString(), syns = dr["Synonyms"].ToString() });
				}
				return syns;
			}
		}
		List<Answers> LoadAnsData()
		{
			using (SqlConnection conn = new SqlConnection(Fill.cs))
			{
				List<Answers> answers = new List<Answers>();
				conn.Open();
				SqlCommand cmd = new SqlCommand(@"select * from Answers"
													, conn);


				SqlDataReader dr = cmd.ExecuteReader();
				while (dr.Read())
				{

					answers.Add(new Answers {
							id = Convert.ToInt32(dr["Answer_ID"]),
							ans = dr["AnswerText"].ToString(),
							altAns1 = dr["AlterAnswer1"].ToString(),
							altAns2 = dr["AlterAnswer2"].ToString(),
							fullAns = dr["FullAnswer"].ToString()
					});
				}
				return answers;
			}
		}

		List<Topics> LoadTopicsData()
		{
			using (SqlConnection conn = new SqlConnection(Fill.cs))
			{
				List<Topics> answers = new List<Topics>();
				conn.Open();
				SqlCommand cmd = new SqlCommand(@"select * from Topics"
													, conn);

				SqlDataReader dr = cmd.ExecuteReader();
				while (dr.Read())
				{

					answers.Add(new Topics
					{
						id = Convert.ToInt32(dr["ID"]),
						topic = dr["Topic"].ToString(),
					});
				}
				return answers;
			}
		}
		List<Questions> LoadQuestionsData()
		{
			using (SqlConnection conn = new SqlConnection(Fill.cs))
			{
				List<Questions> answers = new List<Questions>();
				conn.Open();
				SqlCommand cmd = new SqlCommand(@"select * from Questions"
													, conn);

				SqlDataReader dr = cmd.ExecuteReader();
				while (dr.Read())
				{
					Questions q = new Questions();
					q.id = Convert.ToInt32(dr["Question_ID"]);
					q.question = dr["QuestionText"].ToString();
					
					if (!(dr["Answer_ID"] is DBNull))
					{
						q.answer_id = Convert.ToInt32(dr["Answer_ID"]);
					}
					if (!(dr["Topic_ID"] is DBNull))
					{
						q.topic_id = Convert.ToInt32(dr["Topic_ID"]);
					}

					answers.Add(q);
				}
				return answers;
			}
		}
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			var syns = LoadSynsData();
			wrap.Children.Clear();
			foreach (var el in syns)
			{
				SynonymRow uc = new SynonymRow();
				uc.id.Text = el.id.ToString();
				uc.word.Text = el.word;
				uc.syns.Text = el.syns;
				wrap.Children.Add(uc);
			}
		}
		private void Button1_Click(object sender, RoutedEventArgs e)
		{
			Dictionary<string, int> Stats = new Dictionary<string, int>();
			using (SqlConnection conn = new SqlConnection(Fill.cs))
			{
				conn.Open();
				SqlCommand cmd = new SqlCommand(@"
													select t.Topic, s.count
													from [Statistics] s
													inner join
													Topics t
													on s.topic_id = t.ID"
													, conn);
				SqlDataReader dr = cmd.ExecuteReader();
				while (dr.Read())
				{
					Stats.Add(dr["Topic"].ToString(), Convert.ToInt32(dr["count"]));
				}
			}


			object misValue = System.Reflection.Missing.Value;
			Excel.Application xlApp;
			Excel.Workbook xlWorkBook;
			Excel.Worksheet xlWorkSheet;
			xlApp = new Excel.Application();
			xlWorkBook = xlApp.Workbooks.Add(misValue);
			xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

			//add data 
			int i = 1;
			foreach (var el in Stats)
			{
				xlWorkSheet.Cells[i, 1] = el.Key;
				xlWorkSheet.Cells[i, 2] = el.Value;
				i++;
			}

			//xlWorkSheet.Cells[5, 1] = "Term4";
			//xlWorkSheet.Cells[5, 2] = "75";


			Excel.Range chartRange;

			Excel.ChartObjects xlCharts = (Excel.ChartObjects)xlWorkSheet.ChartObjects(Type.Missing);
			Excel.ChartObject myChart = (Excel.ChartObject)xlCharts.Add(10, 80, 300, 250);
			Excel.Chart chartPage = myChart.Chart;

			chartRange = xlWorkSheet.get_Range("A1", "b" + (--i).ToString());
			chartPage.SetSourceData(chartRange, misValue);


			chartPage.ChartType = Excel.XlChartType.xlColumnClustered;
			xlWorkBook.SaveAs(@"F:\fill\fill.xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive);
			xlWorkBook.Close(true, misValue, misValue);
			xlApp.Quit();



			System.Diagnostics.Process.Start(@"F:\fill\fill.xls");



		}
		class Synonyms
		{
			public int id;
			public string word;
			public string syns;
		}
		class Answers
		{
			public int id;
			public string ans;
			public string altAns1;
			public string altAns2;
			public string fullAns;
		}
		class Topics
		{
			public int id;
			public string topic;
		}
		class Questions
		{
			public int id;
			public string question;
			public int answer_id;
			public int topic_id;
		}
		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			AddSynonym add = new AddSynonym();
			add.ShowDialog();
		}

	
		private void GetAnswers(object sender, RoutedEventArgs e)
		{
			var ans = LoadAnsData();
			wrap.Children.Clear();
			foreach (var el in ans)
			{
				AnswerRow uc = new AnswerRow();
				uc.id.Text = el.id.ToString();
				uc.answer.Text = el.ans;
				uc.alterAnswer.Text = el.altAns1;
				wrap.Children.Add(uc);
			}
		}

		private void AddAnswer(object sender, RoutedEventArgs e)
		{
			AddAnswer add = new AddAnswer();
			add.ShowDialog();
		}

		private void AddTopic(object sender, RoutedEventArgs e)
		{
			AddTopic add = new AddTopic();
			add.ShowDialog();
		}

		private void GetTopics(object sender, RoutedEventArgs e)
		{
			var topics = LoadTopicsData();
			wrap.Children.Clear();
			foreach (var el in topics)
			{
				TopicRow uc = new TopicRow();
				uc.id.Text = el.id.ToString();
				uc.topic.Text = el.topic;
				wrap.Children.Add(uc);
			}
		}

		private void AddQuestion(object sender, RoutedEventArgs e)
		{
			AddQuestion add = new AddQuestion();
			add.ShowDialog();
		}

		private void GetQuestions(object sender, RoutedEventArgs e)
		{
			var questions = LoadQuestionsData();
			wrap.Children.Clear();
			foreach (var el in questions)
			{
				QuestionRow uc = new QuestionRow();
				uc.id.Text = el.id.ToString();
				uc.question.Text = el.question;
				uc.answer_id.Text = el.answer_id.ToString();
				uc.topic_id.Text = el.topic_id.ToString();
				wrap.Children.Add(uc);
			}
		}
	}
}
